#******************************************************************************
# File: Mailbox.pm
# Description:
# Author:       Petar Petrov <pro.xex@gmail.com>
#
# Copyright (c) 2009 Petar Petrov.  All rights reserved.
# This module is free software; you can redistribute it and/or modify
# it under the same terms as Perl itself.
#
#******************************************************************************

package Modules::Mailbox;

use strict;
use warnings;

use Mail::IMAPClient;
use IO::Socket::SSL;
use MIME::Parser;
use Net::SMTP;
use DateTime::Format::DateParse;
use File::Spec::Functions;
use File::Copy;
use Cwd;
use Log::Log4perl;
use DBI qw(:sql_types);
use Time::gmtime;
use Logger;
use ResponseHandler;
use List::Util 'shuffle';

use constant DB_PATH => File::Spec->rel2abs( catfile( $ENV{'EMEX_PATH'}, '/data/db' ) );
use constant DB_PROTOTYP_PATH => File::Spec->rel2abs( catfile( DB_PATH, 'prototype-acc.s3db' ) );
use constant TEMP_PATH => File::Spec->rel2abs( catfile( $ENV{'EMEX_PATH'}, '/data/tmp' ) );

### globals
my $logger = Modules::Logger::create(__PACKAGE__);

sub new {
	my ( $class, $email ) = @_;
	$class = ref($class) || $class;
	my $self = bless { 'email' => $email }, $class;

	$logger->trace( 'New Mailbox instance for email => ' . $email );
	$self->_create_or_open();

	return $self;
}

# try to open existing database
sub _create_or_open {
	my $self = shift;

	# get db name
	my $userdb = $self->_get_user_dbfile();

	# create OR open
	if ( !-e $userdb ) {
		$logger->info("Creating $userdb from prototype database ...");
		copy( DB_PROTOTYP_PATH, $userdb )
		  or $logger->logdie(
			"Failed copying " . DB_PROTOTYP_PATH . " to $userdb !" );
	}
	else {
		$logger->debug("Database ($userdb) for user $self->{'email'} found !");
	}

	my $db_handle = $self->_open_database($userdb);

	# TODO: nothing needed, right now !
	$db_handle->disconnect() if ($db_handle);
}

sub _get_user_dbfile {
	my $self = shift;

	my $userdb_name = $self->{'email'};
	$userdb_name =~ s/@/__/;
	$userdb_name .= '.s3db';

	return catfile( DB_PATH, $userdb_name );
}

sub _open_database {
	my ( $self, $db_file, $transactions ) = @_;
	
	# if no user database defined, just get from current context
	if ( ! $db_file ) {
		$db_file = $self->_get_user_dbfile();
	}

	# disable transactions by default
	$transactions = 1 if not defined $transactions;

	my $db_handle =
	  DBI->connect( "dbi:SQLite:dbname=$db_file", "", "",
		{ RaiseError => 0, AutoCommit => $transactions ? 0 : 1 } );

	if ($db_handle) {

		# set error handler callback
		$db_handle->{HandleError} = sub {
			$logger->error("DBI => $_[0]");
		};
	}
	else {
		$logger->error("Error connecting DBI ($db_file) : $DBI::errstr !");
	}

	return $db_handle;
}


sub _open_imapbox {
	my ( $self, $password, $settings ) = @_;
	
	# load settings 
	if ( ! $settings ) {
		my $db_handle = $self->_open_database();
		$settings = $self->get_settings( $db_handle );
		if ( ! $settings ) {
			$logger->error('User settings are missing or could not be loaded !');
			return 0;
		}
		$db_handle->disconnect();
	}
			 	
	$logger->info("IMAP Opening connection to $settings->{'incoming_server'}:$settings->{'incoming_port'} [Security=$settings->{'incoming_security'}] ...");
	
	my $imap;
	if ( $settings->{'incoming_security'} eq "SSL" ) {
		$logger->debug("Connecting trough SSL socket ...");
		
		$imap = Mail::IMAPClient->new(
			Socket	 => IO::Socket::SSL->new(
		   		PeerAddr => $settings->{'incoming_server'},
		   		PeerPort => $settings->{'incoming_port'},
		  	),
			User     => $settings->{'username'},
			Password => $password,
			#Uid      => 1,       #Problems !?
			Debug => 1,
		  );
		  
		$imap or $logger->warn("IMAP connect failed ($@)!");
		$imap->State(Mail::IMAPClient::Connected())
			if $imap;
	}
	else {
		$imap = Mail::IMAPClient->new(
			Server	 => $settings->{'incoming_server'},
			Port	 => $settings->{'incoming_port'},
			User     => $settings->{'username'},
			Password => $password,
			#Uid      => 1,       #Problems !?
		  ); 
		$imap or $logger->warn("IMAP connect failed ($@)!");
	}
	
	if ( $imap ) {
	   $imap->login() or $logger->warn("Failed to login to IMAP server ($@)!");
	} 
	
	return $imap;
}

sub clear {
	my ( $self ) = @_;
	
	my $db_handle = $self->_open_database( $self->_get_user_dbfile() )
	  or return 0;
	  
	my $sth = $db_handle->prepare("DELETE FROM Attachment");
	$sth->execute;	  
	$sth = $db_handle->prepare("DELETE FROM Conversant");
	$sth->execute;	  
	$sth = $db_handle->prepare("DELETE FROM Conversation");
	$sth->execute;	  	
	$sth = $db_handle->prepare("DELETE FROM EMail");
	$sth->execute;	  	
	$sth = $db_handle->prepare("DELETE FROM EMailTags");
	$sth->execute;	  	
	$sth = $db_handle->prepare("DELETE FROM Folder");
	$sth->execute;	
	$sth = $db_handle->prepare("DELETE FROM RelConversantToEMail");
	$sth->execute;	
	$sth = $db_handle->prepare("DELETE FROM RelMailTagToEMail");
	$sth->execute;	
	$sth = $db_handle->prepare("DELETE FROM RelSearchTagToEMail");
	$sth->execute;	
	$sth = $db_handle->prepare("DELETE FROM SearchTags");
	$sth->execute;	
		
	# close DB
	$db_handle->commit
		or return 0;
	$db_handle->disconnect();	
	
	return 1;
}

sub register {
	my ( $self, $username, $full_name, $incoming_server, $incoming_port, $incoming_security, $server_type, $smtp_server, $smtp_port, $smtp_username, $smtp_security ) = @_;
	
	my $db_handle = $self->_open_database( $self->_get_user_dbfile() )
	  || return Modules::ResponseHandler->new()
	  ->response_fail('Failed to open database!');

	my $sth = $db_handle->prepare("DELETE FROM settings");    # nasty hack, I admit ;)
	$sth->execute;

	#print "TIME:" . DateTime::Format::DateParse->parse_datetime( DateTime->now() );;
	$sth = $db_handle->prepare( "INSERT INTO settings (key,value,last_update) VALUES (?,?,CURRENT_TIMESTAMP)" );
	$sth->bind_param_array( 1,
		[ qw(email username full_name incoming_server incoming_port incoming_security server_type smtp_server smtp_port smtp_username smtp_security) ],
		SQL_VARCHAR );
	$sth->bind_param_array( 2,
		[ $self->{'email'}, $username, $full_name, $incoming_server, $incoming_port, $incoming_security, $server_type, $smtp_server, $smtp_port, $smtp_username, $smtp_security ],
		SQL_VARCHAR );
	#$sth->bind_param_array(3, DateTime::Format::DateParse->parse_datetime( DateTime->now() ), SQL_TIMESTAMP ); # scalar will be reused for each row
	$sth->execute_array( { ArrayTupleStatus => \my @tuple_status } );

	$db_handle->commit;
	$db_handle->disconnect();

	return Modules::ResponseHandler->new()->response_ok();
}

sub get_settings {
	my ($self, $db_handle) = @_;
	
	my $hash_result;	
	my $sth = $db_handle->prepare( qq/SELECT key,value FROM Settings/ );
	$sth->execute;
	
	while( my @row = $sth->fetchrow_array ) {
		$hash_result->{"$row[0]"} = $row[1];
	}
	#use Data::Dumper;
	#print Dumper( $hash_result );
	return $hash_result;
}


sub build_mbox {
	my ($self, $pass) = @_;
	
	# just clean up everything from the current DB
	$self->clear();

	my $db_handle = $self->_open_database()
	  or return Modules::ResponseHandler->new()->response_fail('Failed to open database!');

	# open user mailbox
	my $imap = $self->_open_imapbox( $pass );
	$imap or return Modules::ResponseHandler->new()->response_fail('Failed to connect to IMAP server!');

	my @folders = $imap->folders
	  or return Modules::ResponseHandler->new()->response_fail("List folders error: $imap->LastError !");

	$logger->debug("Folders found: @folders")
	  if ( $logger->is_debug() );

	foreach my $folder (@folders) {
		$logger->debug( "IMAP : opening folder $folder ..." );
		
		#skip (SENT) folder  
		if ( $folder eq 'Sent' ) {
			$logger->debug('Skipping SENT folder ...');
			next;
		}

		# open mailbox
		$imap->select($folder)
		  or return Modules::ResponseHandler->new()->response_fail('Failed to open mailbox !');

		# get messages
		my @msgs = $imap->messages;
		my $msg_count = @msgs; 
		if ( !$msg_count ) {
			$logger->info("Folder $folder is empty !");
			next;
		}
		
		$logger->debug( "$msg_count messages found !" );

		print "\n--------- $folder ----------\n";
		foreach my $uid  ( @msgs ) {

			print "---\n";
			my $hashref = $imap->fetch_hash( "UID", "INTERNALDATE", "RFC822.SIZE" );

			# get IMAP header
			print "\n $folder => Message $uid \n";
			if ( !$uid ) {
				$logger->warn("Invalid message UID => $uid in Folder => $folder !");
				next;
			}

			# get Email sender
			$imap->get_header( $uid, "From" ) =~ /"?(.[^"]*)"?\s?<(.*)>/;
			my ($fullname,$email);
			$fullname = $1 if $1;
			$fullname or $fullname = '';
			$email = $2 if $2;
			$email or $email = $imap->get_header( $uid, "From" );
			  
			my $subject = $imap->subject( $uid );
			$subject = '(No Subject)'
				if ( ! $subject );
				
			#---DEBUG---
			#$logger->debug("Working on Email with subject => [$subject]");
			print "UID:\t $uid \n";
			print "FROM:\t" . $imap->get_header( $uid, "From" ) . " | REAL => $email \n";
			print "TO:\t" . $imap->get_header( $uid, "To" ) . "\n";
			print "CC:\t" . $imap->get_header( $uid, "Cc" ) . "\n"
			  if defined $imap->get_header( $uid, "Cc" );
			print "SUBJECT:\t" . $subject . "\n";
			print "FULNAME:\t" . $fullname . "\n";
			#---DEBUG---

			# get Message-ID
			my $msgID = $imap->get_header( $uid, "Message-ID" );
			# get priority
			my $prio = 0;
			if ( $imap->get_header( $uid, "X-Priority" ) ) {
				$imap->get_header( $uid, "X-Priority" ) =~ /(.*)(\d)(.*)/;
				$prio = $2 if $2;
			}
			# get datetime
			my $dt = DateTime::Format::DateParse->parse_datetime( $hashref->{$uid}{'INTERNALDATE'} );
			# get size
			my $size = $hashref->{$uid}{'RFC822.SIZE'};
			
			my (@row, $sth);

			#--- insert/get [Conversant]
			my $conversant_id;
			
			$sth = $db_handle->prepare(qq/SELECT id FROM Conversant WHERE email='$email'/);
			$sth->execute;
			if ( @row = $sth->fetchrow_array ) {
				$conversant_id = $row[0];
				$sth = $db_handle->prepare(qq/UPDATE Conversant SET messages = messages + 1 WHERE id=?/);
				$sth->bind_param( 1, $conversant_id, SQL_INTEGER );
				$sth->execute;
			}
			else {
				# insert new
				$sth = $db_handle->prepare(qq/INSERT INTO Conversant(id,email,fullname,messages) VALUES(NULL,?,?,?)/);
				$sth->bind_param( 1, $email, SQL_VARCHAR );
				$sth->bind_param( 2, $fullname, SQL_VARCHAR );
				$sth->bind_param( 3, 1, SQL_INTEGER );
				$sth->execute;
	
				$sth = $db_handle->prepare(qq/SELECT id FROM Conversant WHERE email='$email'/);
				$sth->execute;
				if ( @row = $sth->fetchrow_array ) {
					$conversant_id = $row[0];
				}
			}
			
			#--- insert [Email]
			$sth = $db_handle->prepare(
				qq/INSERT INTO 
						EMail(id,conversation_id,messageID,uid,inReplyTo,dateRecieved,priority,subject,folder,size,sender_id) 
						VALUES(NULL,?,?,?,?,?,?,?,?,?,?)/
			);

			$sth->bind_param( 1, -1, SQL_INTEGER );
			$sth->bind_param( 2, $msgID ? $msgID : '', SQL_VARCHAR );
			$sth->bind_param( 3, $uid, SQL_INTEGER );
			$sth->bind_param( 4, $imap->get_header( $uid, "In-Reply-To" ), SQL_VARCHAR );
			$sth->bind_param( 5, $dt, SQL_TIMESTAMP );
			$sth->bind_param( 6, $prio, SQL_INTEGER );
			$sth->bind_param( 7, $subject, SQL_VARCHAR );
			$sth->bind_param( 8, $folder, SQL_VARCHAR );
			$sth->bind_param( 9, $size, SQL_INTEGER );
			$sth->bind_param( 10, $conversant_id, SQL_INTEGER );
			$sth->execute;

			$sth = $db_handle->prepare(qq/SELECT MAX(id) FROM EMail/);
			$sth->execute;
			@row      = $sth->fetchrow_array;
			my $email_id = $row[0];			
			
			#--- insert [RelConversantToEMail]
			$sth = $db_handle->prepare(
				qq/INSERT INTO RelConversantToEMail(id,conversant_id,email_id) VALUES(NULL,?,?)/
			);
			$sth->bind_param( 1, $conversant_id, SQL_INTEGER );
			$sth->bind_param( 2, $email_id, SQL_INTEGER );
			$sth->execute;			

			#--- insert [Attachment]
			my $mp = new MIME::Parser;
			$mp->ignore_errors(1);
			$mp->extract_uuencode(1);
			$mp->decode_headers(1);
			$mp->output_under(TEMP_PATH);

			my $entity    = $mp->parse_data( $imap->bodypart_string($uid, '') );
			my $num_parts = $entity->parts;
			my @parts     = $entity->parts;
			#print "PARTS:\t $num_parts \n";

			my $j = 0;
			my $bodydata;
			
			if ($num_parts > 0) {
				for my $part ( $entity->parts ) {
	
					# first part is (always??) a body
					if ( $j++ == 0 ) {
						$bodydata = $part->bodyhandle->as_string;
						#print $bodydata;
						#print "BOUNDAT:" . $part->head->multipart_boundary;
						next;
					}
	
					# attachment
					my $type      = $part->mime_type;
					my $enc       = $part->head->mime_encoding;
					my $file_name = $part->head->recommended_filename;
					$file_name or $file_name = $part->head->mime_attr('content-disposition.filename');
					$file_name or $file_name = "unknown";
					my $boundary  = $part->head->multipart_boundary;
					#$boundary or $boundary = '';
					print "ATTACHMENT INFO:\t $file_name | $enc | $type | $boundary \n";
	
					$sth = $db_handle->prepare(
						qq/INSERT INTO 
									Attachment(id,folder_id,email_id,conversant_id,name,type,boundary) 
									VALUES(NULL,NULL,?,?,?,?,?)/
					);
	
					$sth->bind_param( 1, $email_id, SQL_INTEGER );
					$sth->bind_param( 2, $conversant_id, SQL_INTEGER );
					$sth->bind_param( 3, $file_name, SQL_VARCHAR );
					$sth->bind_param( 4, $type, SQL_VARCHAR );
					$sth->bind_param( 5, $boundary, SQL_VARCHAR );
					$sth->execute;
	
					# clean up extracted attachment
					$mp->filer->purge;
				}
			}
			else {
				# NO ATTACHMENTS ! Just get the body
	            if (my $io = $entity->open("r")) {
	                while (defined($_ = $io->getline)) {
	                    $bodydata .= $_;
	                }
                	$io->close;
	            }				
			}
			
			#--- insert [SearchTags]
			print "Adding keywords ...\n";
			if ( $bodydata ) {
				
	            my @words = split(/\W/, $bodydata, 5000 );
	            my @shuffled_words = shuffle(@words);
	            my $cnt = 0;
	            
	            foreach my $word (@shuffled_words) {
		            if ( length($word) >= 3 ) { # Insert at least 3-letter words
		            
		            	next if ( $cnt++ > 1200 );
		            	
		            	my $tag_id;
		            
						$sth = $db_handle->prepare( qq/SELECT id FROM SearchTags WHERE data=?/ );
						$sth->bind_param( 1, $word, SQL_VARCHAR );
						$sth->execute;	
						my @row = $sth->fetchrow_array;
						if ( @row ) {
							$tag_id = $row[0];							
							# increase depth
							$sth = $db_handle->prepare( qq/UPDATE SearchTags SET depth=depth + 1 WHERE id=?/ );
							$sth->bind_param( 1, $tag_id, SQL_INTEGER );
							$sth->execute;				
						}
						else {
							# tag DOES NOT EXIST
							$sth = $db_handle->prepare( qq/INSERT INTO SearchTags(id,depth,data) VALUES(NULL,1,?)/ );
							$sth->bind_param( 1, $word, SQL_VARCHAR );
							$sth->execute;
							
							# create EMail->Tag relation
							$sth = $db_handle->prepare( qq/SELECT MAX(id) FROM SearchTags/ );
							$sth->execute;
							$tag_id = $row[0]
								if (@row = $sth->fetchrow_array);
						}
							
						# insert relation
						$sth = $db_handle->prepare( qq/SELECT id FROM RelSearchTagToEMail WHERE tag_id=? AND email_id=?/ );
						$sth->bind_param( 1, $tag_id, SQL_INTEGER );
						$sth->bind_param( 2, $email_id, SQL_INTEGER );						
						$sth->execute;
						if ( ! (@row = $sth->fetchrow_array) ) {
							#print "Adding new => $word\n";
							$sth = $db_handle->prepare(qq/INSERT INTO RelSearchTagToEMail(id,tag_id,email_id) VALUES(NULL,?,?)/);
							$sth->bind_param( 1, $tag_id, SQL_INTEGER );
							$sth->bind_param( 2, $email_id, SQL_INTEGER );
							$sth->execute;	
						}
						#else {
						#	print " $word added already for this mail !\n";
						#}
	            	}            	
	            }
			}
			print "Done.\n";
			$db_handle->commit;
			
			# TO BE CONTINUED ...
		}

		$imap->close;
	}

	# close IMAP
	$imap->logout
	  or $logger->warn('disconnect() failed for IMAP client !');

	# close DB
	$db_handle->commit;
	$db_handle->disconnect();

	return Modules::ResponseHandler->new()->response_ok();
}

sub list_users {
	my ($self) = @_;
	
	my $db_handle = $self->_open_database()
	  or return Modules::ResponseHandler->new()->response_fail('Failed to open database!');
	  
	my $sth = $db_handle->prepare(qq/SELECT id, email, fullname, messages FROM Conversant/);
	$sth->execute;
	my $hash_ref = $sth->fetchall_arrayref({});
	
	$db_handle->disconnect();	  
	return Modules::ResponseHandler->new()->response_ok( $hash_ref );	  
}

sub edit_contact {
	my ($self, $params) = @_;

	my $db_handle = $self->_open_database()
	  or return Modules::ResponseHandler->new()->response_fail('Failed to open database!');
	  
	$logger->info("Updating user " . $$params{'who'} . " ...");
	  
	my $sth = $db_handle->prepare(qq/UPDATE Conversant 
			SET other_email=?,fullname=?,nickname=?,birthday=?,tel_work=?,tel_home=?,tel_fax=?,tel_pager=?,tel_mobile=?,AIM=?  
			WHERE id=? /);
	
	$sth->bind_param( 1, $$params{'other_email'}, SQL_VARCHAR );
	$sth->bind_param( 2, $$params{'fullname'}, SQL_VARCHAR );
	$sth->bind_param( 3, $$params{'nickname'}, SQL_VARCHAR );
	$sth->bind_param( 4, $$params{'birthday'}, SQL_TIMESTAMP );
	$sth->bind_param( 5, $$params{'tel_work'}, SQL_VARCHAR );
	$sth->bind_param( 6, $$params{'tel_home'}, SQL_VARCHAR );
	$sth->bind_param( 7, $$params{'tel_fax'}, SQL_VARCHAR );
	$sth->bind_param( 8, $$params{'tel_pager'}, SQL_VARCHAR );
	$sth->bind_param( 9, $$params{'tel_mobile'}, SQL_VARCHAR );
	$sth->bind_param( 10, $$params{'AIM'}, SQL_VARCHAR );
	$sth->bind_param( 11, $$params{'id'}, SQL_VARCHAR );
	$sth->execute;	
	
	$db_handle->commit;
	$db_handle->disconnect();	  
	return Modules::ResponseHandler->new()->response_ok();	  
}

sub list_mails {
	my ($self) = @_;

	my $db_handle = $self->_open_database() 
		or return Modules::ResponseHandler->new()->response_fail('Failed to open database!');
	  
	my $sth = $db_handle->prepare(qq/SELECT EMail.id, dateRecieved, priority, subject, size, Conversant.email  
								FROM EMail 
								LEFT OUTER JOIN Conversant ON Conversant.id = EMail.sender_id  /);
	
	$sth->execute;
	my $hash_ref = $sth->fetchall_arrayref({});
	
	$db_handle->disconnect();	  
	return Modules::ResponseHandler->new()->response_ok( $hash_ref );
}

sub list_mails_by_searchtag {
	my ($self, $tagid ) = @_;

	my $db_handle = $self->_open_database()
	  or return Modules::ResponseHandler->new()->response_fail('Failed to open database!');
	  
	my $sth = $db_handle->prepare(qq/SELECT email_id FROM RelSearchTagToEMail WHERE tag_id=?/);
	$sth->bind_param( 1, $tagid, SQL_INTEGER );
	$sth->execute;	
		  
	my $hash_ref = [];
	
	while( my @row = $sth->fetchrow_array ) {
		my $sth2 = $db_handle->prepare(qq/SELECT id, dateRecieved, priority, subject, size FROM EMail WHERE id=? /);
		$sth2->bind_param( 1, $row[0], SQL_INTEGER );
		$sth2->execute;
		push( @$hash_ref, $sth2->fetchrow_hashref );
	}
	
	$db_handle->disconnect();	  
	return Modules::ResponseHandler->new()->response_ok( $hash_ref );	
}

sub list_tags {
	my ($self) = @_;

	my $db_handle = $self->_open_database()
	  or return Modules::ResponseHandler->new()->response_fail('Failed to open database!');
	  
	# get 200 of the most tagged
	my $sth = $db_handle->prepare(qq/SELECT id, depth, data FROM SearchTags ORDER BY depth DESC LIMIT 200/);
	$sth->execute;
	my $hash_ref = $sth->fetchall_arrayref({});
	$db_handle->disconnect();	  
	
	return Modules::ResponseHandler->new()->response_ok( $hash_ref );
}

sub list_contact_mails {
	my ($self, $userid) = @_;

	my $db_handle = $self->_open_database()
	  or return Modules::ResponseHandler->new()->response_fail('Failed to open database!');
	  
	my $sth = $db_handle->prepare(qq/SELECT email_id FROM RelConversantToEMail WHERE conversant_id = ?/);
	$sth->bind_param( 1, $userid, SQL_INTEGER );
	$sth->execute;	  
	
	my $hash_ref = [];
	
	while( my @row = $sth->fetchrow_array ) {
		my $sth2 = $db_handle->prepare(qq/SELECT id, dateRecieved, priority, subject, size FROM EMail WHERE id=? /);
		$sth2->bind_param( 1, $row[0], SQL_INTEGER );
		$sth2->execute;
		push( @$hash_ref, $sth2->fetchrow_hashref );
	}
		
	$db_handle->disconnect();	  
	return Modules::ResponseHandler->new()->response_ok( $hash_ref );
}

sub get_email {
	my ($self, $id, $pass) = @_;
	
	if ( ! $pass ) {
		$logger->error('Password for IMAP mailbox not specified !');
		return Modules::ResponseHandler->new()->response_fail( 'Password for mailbox was not specified !' );
	}

	my $db_handle = $self->_open_database()
	  or return Modules::ResponseHandler->new()->response_fail('Failed to open database!');
	  
	my $hash_ref;
	
	# get mail info
	my $sth = $db_handle->prepare(qq/SELECT uid, folder FROM EMail WHERE id=? /);
	$sth->bind_param( 1, $id, SQL_INTEGER );
	$sth->execute;
	my @row = $sth->fetchrow_array;
	if ( @row ) {
		my $uid 	= $row[0];
		my $folder 	= $row[1];
	
		# open user mailbox
		my $imap = $self->_open_imapbox( $pass );
		$imap or return Modules::ResponseHandler->new()->response_fail('Failed to connect to IMAP server!');
		
		$imap->select($folder) or return Modules::ResponseHandler->new()->response_fail('Failed to open mailbox !');
	
		my $hashref = $imap->fetch_hash( "UID", "INTERNALDATE", "RFC822.SIZE" );
		
		$uid or return Modules::ResponseHandler->new()->response_fail("Invalid UID for E-mail in Folder => $folder !");
	
		# get Email sender
		$imap->get_header( $uid, "From" ) =~ /(.*)<(.*)>/;
		my ($fullname,$email);
		$fullname = $1 if $1;
		$fullname or $fullname = '';
		$email = $2 if $2;
		$email or $email = $imap->get_header( $uid, "From" );
				  
		my $subject = $imap->get_header( $uid, "Subject" ) ? $imap->get_header( $uid, "Subject" ) : '(No Subject)';
	
		my $to = $imap->get_header( $uid, "To" );
		my $cc = $imap->get_header( $uid, "Cc" );
		my $bcc = $imap->get_header( $uid, "Bcc" );
	
		#--- read Attachments
		my $mp = new MIME::Parser;
		$mp->ignore_errors(1);
		$mp->extract_uuencode(1);
		$mp->decode_headers(1);
		$mp->output_under(TEMP_PATH);
	
		my $entity    = $mp->parse_data( $imap->bodypart_string($uid, '') );
		my $num_parts = $entity->parts;
		my @parts     = $entity->parts;
	
		my $j = 0;
		my $bodydata;
				
		if ($num_parts > 0) {
			for my $part ( $entity->parts ) {
				# first part is (always??) a body
				if ( $j++ == 0 ) {
					$bodydata = $part->bodyhandle->as_string;
					next;
				}
				# clean up extracted attachment
				$mp->filer->purge;
			}
		}
		else {
			# NO ATTACHMENTS ! Just get the body
	           if (my $io = $entity->open("r")) {
	               while (defined($_ = $io->getline)) {
	                   $bodydata .= $_;
	               }
	           $io->close;
	           }				
		}
		# clean up extracted attachment
		$mp->filer->purge;
						
		$imap->close;
	
		# close IMAP
		$imap->logout 
			or $logger->warn('disconnect() failed for IMAP client !');	
		  
		$hash_ref = [ 
			{'FROM' => $email, 'TO' => $to, 'CC' => $cc, 'BCC' => $bcc, 'SUBJECT' => $subject, 'BODY' => $bodydata} 
		]	
	}
	$db_handle->commit;
	$db_handle->disconnect();
	return Modules::ResponseHandler->new()->response_ok( $hash_ref );	  
}

sub list_attachments {
	my ($self) = @_;

	my $db_handle = $self->_open_database()
	  or return Modules::ResponseHandler->new()->response_fail('Failed to open database!');
	  
	my $sth = $db_handle->prepare(qq/SELECT Attachment.id, email_id, name, type, Conversant.email  
		FROM Attachment 
		LEFT OUTER JOIN Conversant ON Conversant.id = Attachment.conversant_id/);
	$sth->execute;
	
	my $hash_ref = $sth->fetchall_arrayref({});
	$db_handle->disconnect();	  
	
	return Modules::ResponseHandler->new()->response_ok( $hash_ref );	  
}

sub get_attachment {
	my ($self, $id, $pass) = @_;

	if ( ! $pass ) {
		$logger->error('Password for IMAP mailbox not specified !');
		return Modules::ResponseHandler->new()->response_fail( 'Password for mailbox was not specified !' );
	}

	my $db_handle = $self->_open_database()
	  or return Modules::ResponseHandler->new()->response_fail('Failed to open database!');
	  
	my $hash_ref;
	
	# get attachment info
	my $sth = $db_handle->prepare(qq/SELECT name, email_id, uid, folder FROM Attachment 
								LEFT OUTER JOIN EMail ON EMail.id=Attachment.email_id 
								WHERE Attachment.id=? /);
	$sth->bind_param( 1, $id, SQL_INTEGER );
	$sth->execute;
	my @row = $sth->fetchrow_array;
	if ( @row ) {
		my $name 		= $row[0];
		my $email_id 	= $row[1];
		my $uid 		= $row[2];
		my $folder 		= $row[3];
	
		# open user mailbox
		my $imap = $self->_open_imapbox( $pass );
		$imap or return Modules::ResponseHandler->new()->response_fail('Failed to connect to IMAP server!');
		
		$logger->debug("Opening folder $folder ...");
		$imap->select($folder) or return Modules::ResponseHandler->new()->response_fail('Failed to open mailbox !');
	
		$uid or return Modules::ResponseHandler->new()->response_fail("Invalid UID for E-mail in Folder => $folder !");

		#--- read Attachment
		my $mp = new MIME::Parser;
		$mp->ignore_errors(1);
		$mp->extract_uuencode(1);
		$mp->decode_headers(1);
		$mp->output_under(TEMP_PATH);
	
		my $entity    = $mp->parse_data( $imap->bodypart_string($uid, '') );
		my $num_parts = $entity->parts;
		my $j 		  = 0;
				
		if ($num_parts > 0) {
			for my $part ( $entity->parts ) {
	
				# first part is (always??) a body
				if ( $j++ == 0 ) {
					#$bodydata = $part->bodyhandle->as_string;
					next;
				}
	
				# attachment
				if ( $name eq $part->head->recommended_filename ) {
					my $type      = $part->mime_type;
					my $file_name = $part->head->recommended_filename;
					$file_name or $file_name = $part->head->mime_attr('content-disposition.filename');
					$file_name or $file_name = "unknown";
					 
					my $content   = MIME::Base64::encode_base64( $part->bodyhandle->as_string );
					$hash_ref = [ 
						{'name' => $file_name, 'type' => $type, 'b64_content' => $content } 
					];
							
					next;
				}
										
				# clean up extracted attachment
				#$mp->filer->purge;
			}
		}
		else {
			# No attachments at all !
			$logger->error("Attachment with ID=$id was not found ! There are no attachments for EMail=$email_id!");
		}
		$mp->filer->purge;
		$imap->close;
	
		# close IMAP
		$imap->logout
		  or $logger->warn('disconnect() failed for IMAP client !');	
	}
	
	$db_handle->disconnect();	  
	
	$hash_ref or return Modules::ResponseHandler->new()->response_fail("Attachment was not found !");
	return Modules::ResponseHandler->new()->response_ok( $hash_ref );	  
}

sub search {
	my ($self, $word) = @_;

	my $db_handle = $self->_open_database()
	  or return Modules::ResponseHandler->new()->response_fail('Failed to open database!');
	  
	my $sth = $db_handle->prepare(qq/SELECT id,data,depth 
			FROM SearchTags 
			WHERE data LIKE '%$word%' 
			ORDER BY depth DESC/);
	$sth->execute;
	my $hash_ref = $sth->fetchall_arrayref({});
	$db_handle->disconnect();	  
	
	return Modules::ResponseHandler->new()->response_ok( $hash_ref );	  
}

sub send_mail {
	my ($self, %params ) = @_;
	
	use Data::Dumper;
	print Dumper( %params );	
	
	my $db_handle = $self->_open_database()
	  or return Modules::ResponseHandler->new()->response_fail('Failed to open database!');
	  	
	# load settings
	my $settings = $self->get_settings( $db_handle );
	if ( ! $settings ) {
		return Modules::ResponseHandler->new()->response_fail('User settings are missing !');		
	}
			
	$db_handle->disconnect();	 
	
	$logger->info("Sending E-Mail to $params{'TO'} ...");
	
	my $smtp = Net::SMTP->new( $settings->{'smtp_server'},
							   Timeout => 60,
							   Debug => 1 );
								
    $smtp->mail( $self->{'email'} );
    $smtp->to( $params{'TO'} );
    $smtp->cc( $params{'CC'} );
    $smtp->bcc( $params{'BCC'} );
    $smtp->data();
    $smtp->datasend("From: $self->{'email'} \n");
    $smtp->datasend("To: $params{'TO'}\n");
    $smtp->datasend("Cc: $params{'CC'}\n")
    	if ( $params{'CC'} );
    $smtp->datasend("Subject: $params{'SUBJECT'}\n");
    $smtp->datasend("\n");
    $smtp->datasend("$params{'BODY'}\n");
    $smtp->dataend();
    $smtp->quit;    
    
    return Modules::ResponseHandler->new()->response_ok();	  
}


1;
