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
use MIME::Parser;
use DateTime::Format::DateParse;
use File::Spec::Functions;
use File::Copy;
use Cwd;
use Log::Log4perl;
use DBI qw(:sql_types);
use Time::gmtime;
use Logger;
use ResponseHandler;

use constant DB_PATH =>
  File::Spec->rel2abs( catfile( cwd(), '../../data/db' ) );
use constant DB_PROTOTYP_PATH =>
  File::Spec->rel2abs( catfile( DB_PATH, 'prototype-acc.s3db' ) );

### globals
my $logger = Modules::Logger::create(__PACKAGE__);

sub new {
	my ( $class, $email ) = @_;
	$class = ref($class) || $class;
	my $self = bless { 'email' => $email }, $class;

	$logger->trace( 'created Mailbox : email => ' . $email );
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

sub register {
	my ( $self, $server, $user, $port, $ssl, $server_type ) = @_;

	my $db_handle = $self->_open_database( $self->_get_user_dbfile() )
	  || return Modules::ResponseHandler->new()
	  ->response_fail('Failed to open database!');

	my $sth = $db_handle->prepare("DELETE FROM settings");    # nasty hack, I admit ;)
	$sth->execute;

	#print "TIME:" . DateTime::Format::DateParse->parse_datetime( DateTime->now() );;
	$sth = $db_handle->prepare( "INSERT INTO settings (key,value,last_update) VALUES (?,?,CURRENT_TIMESTAMP)" );
	$sth->bind_param_array( 1,
		[ 'email', 'server', 'user', 'port', 'ssl', 'server_type' ],
		SQL_VARCHAR );
	$sth->bind_param_array( 2,
		[ $self->{'email'}, $server, $user, $port, $ssl, $server_type ],
		SQL_VARCHAR );
	#$sth->bind_param_array(3, DateTime::Format::DateParse->parse_datetime( DateTime->now() ), SQL_TIMESTAMP ); # scalar will be reused for each row
	$sth->execute_array( { ArrayTupleStatus => \my @tuple_status } );

	$db_handle->commit;
	$db_handle->disconnect();

	return Modules::ResponseHandler->new()->response_ok();
}

sub build_mbox {
	my ($self) = @_;

	my $db_handle = $self->_open_database( $self->_get_user_dbfile() )
	  or return Modules::ResponseHandler->new()
	  ->response_fail('Failed to open database!');

	#TODO: check SERVER TYPE ?

	# open user mailbox

	my $host = "127.0.0.1";
	my $id   = 'deyan.imap';
	my $pass = "123";

	my $imap = Mail::IMAPClient->new(
		Server   => $host,
		User     => $id,
		Password => $pass,
		Uid      => 0,       #Problems !?
		Clear    => 5,
		#Debug   	=> 1,
	  )
	  or return Modules::ResponseHandler->new()
	  ->response_fail('Failed to connect to IMAP server!');

	#my $Authenticated = $imap->Authenticated();
	#my $Connected = $imap->Connected();

	my @folders = $imap->folders
	  or return Modules::ResponseHandler->new()
	  ->response_fail("List folders error: $imap->LastError !");

	$logger->debug("Folders found Folders: @folders")
	  if ( $logger->is_debug() );

	foreach my $folder (@folders) {
		$logger->debug( "MAP : opening folder $folder with "
			  . $imap->message_count($folder)
			  . " messages ." )
		  if ( $logger->is_debug() );

		# open mailbox
		$imap->select($folder)
		  or return Modules::ResponseHandler->new()
		  ->response_fail('Failed to open mailbox !');

		# print messages info
		my $msg_count = $imap->message_count($folder);
		if ( !$msg_count ) {
			# no messages !
			$logger->info("Folder $folder is empty !");
			next;
		}

		print "\n--------- $folder ----------\n";
		for ( my $i = 1 ; $i < $msg_count ; $i++ ) {

			print "\n---\n";
			my $hashref =
			  $imap->fetch_hash( "UID", "INTERNALDATE", "RFC822.SIZE" );

			# get IMAP header
			my $uid = $imap->message_uid($i);
			if ( !$uid ) {
				$logger->warn(
					"Invalid UID for E-mail with ID=$i in Folder => $folder !");
				next;
			}

			# get Email sender
			$imap->get_header( $i, "From" ) =~ /(.*)<(.*)>/;
			my $email = "$2";
			$email = $imap->get_header( $i, "From" )
			  if ( not $email );

			print "UID:\t $uid \n";
			print "FROM:\t" . $imap->get_header( $i, "From" ) . " | REAL => $email \n";
			print "TO:\t" . $imap->get_header( $i, "To" ) . "\n";
			print "CC:\t" . $imap->get_header( $i, "Cc" ) . "\n"
			  if defined $imap->get_header( $i, "Cc" );
			print "SUBJECT:\t" . $imap->get_header( $i, "Subject" ) . "\n";
			print "BODY:\t" . $imap->body_string( $uid ) . "\n";
			#print "DATE:\t" . $hashref->{$uid}{'INTERNALDATE'} . "\n";
			#print "SIZE:\t" . $hashref->{$uid}{'RFC822.SIZE'} . "\n";
			#print "BODY:\t" . $imap->body_string( $uid ) . "\n";

			# get Message-ID
			my $msgID = $imap->get_header( $i, "Message-ID" );

			# get priority
			my $prio;
			if ( $imap->get_header( $i, "X-Priority" ) ) {
				$imap->get_header( $i, "X-Priority" ) =~ /(.*)(\d)(.*)/;
				$prio = "$2";
			}

			#print "PRIO:\t" . $prio . "\n";

			# get datetime
			my $dt = DateTime::Format::DateParse->parse_datetime(
				$hashref->{$uid}{'INTERNALDATE'} );

			#--- insert [Email]
			my $sth = $db_handle->prepare(
				qq/INSERT INTO 
						EMail(id,conversation_id,messageID,uid,inReplyTo,dateRecieved,priority,type,subject) 
						VALUES(NULL,?,?,?,?,?,?,?,?)/
			);

			$sth->bind_param( 1, -1, SQL_INTEGER );
			$sth->bind_param( 2, $msgID ? $msgID : '', SQL_VARCHAR );
			$sth->bind_param( 3, $uid, SQL_INTEGER );
			$sth->bind_param( 4, $imap->get_header( $i, "In-Reply-To" ), SQL_VARCHAR );
			$sth->bind_param( 5, $dt, SQL_TIMESTAMP );
			$sth->bind_param( 6, $prio ? $prio : 0, SQL_INTEGER );
			$sth->bind_param( 7, 1.0, SQL_FLOAT );
			$sth->bind_param( 8, $imap->get_header( $i, "Subject" ), SQL_VARCHAR );
			$sth->execute;

			$sth = $db_handle->prepare(qq/SELECT MAX(id) FROM EMail/);
			$sth->execute;
			my @row      = $sth->fetchrow_array;
			my $email_id = $row[0];

			#--- insert [Conversant]
			$sth = $db_handle->prepare(
				qq/INSERT INTO Conversant(id,email) VALUES(NULL,?)/
			);
			$sth->bind_param( 1, $email ? $email : '', SQL_VARCHAR );
			$sth->execute;

			$sth = $db_handle->prepare(
				qq/SELECT id FROM Conversant WHERE email='$email'/);
			$sth->execute;
			@row = $sth->fetchrow_array;
			my $conversant_id = $row[0];
			
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
			$mp->output_under("../../data/tmp");

			my $entity    = $mp->parse_data( $imap->bodypart_string($uid) );
			my $num_parts = $entity->parts;
			my @parts     = $entity->parts;

			#print "PARTS:\t $num_parts \n";

			my $j = 0;
			for my $part ( $entity->parts ) {

				# first part is (always??) a body
				next if $j++ == 0;

				# attachment
				my $type      = $part->mime_type;
				my $enc       = $part->head->mime_encoding;
				my $file_name = $part->head->recommended_filename;
				print "ATTACHMENT:\t $file_name | $enc | $type \n";

				#print "TYPE:\t $part->mime_type \n";
				#print "ATTACHMENT:\t $file_name \n";

				$sth = $db_handle->prepare(
					qq/INSERT INTO 
								Attachment(id,folder_id,email_id,conversant_id,name,type) 
								VALUES(NULL,NULL,?,?,?,?)/
				);

				$sth->bind_param( 1, $email_id,      SQL_INTEGER );
				$sth->bind_param( 2, $conversant_id, SQL_INTEGER );
				$sth->bind_param( 3, $file_name ? $file_name : 'unknown',
					SQL_VARCHAR );
				$sth->bind_param( 4, $type, SQL_VARCHAR );
				$sth->execute;

				# clean up extracted attachment
				$mp->filer->purge;
			}
			
			#--- insert [SearchTags]
			
			
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

	my $db_handle = $self->_open_database( $self->_get_user_dbfile() )
	  or return Modules::ResponseHandler->new()
	  ->response_fail('Failed to open database!');
	  
	my $sth = $db_handle->prepare(qq/SELECT * FROM Conversant;/);
	$sth->execute;
	
	my $hash_ref = $sth->fetchall_hashref('id');
	#use Data::Dumper;
	#print Dumper( $hash_ref );
	$db_handle->disconnect();	  

	return Modules::ResponseHandler->new()->response_ok( $hash_ref );	  
}


1;
