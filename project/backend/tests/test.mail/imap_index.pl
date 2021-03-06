#! /bin/perl -w

use strict;
use warnings;

use Mail::IMAPClient;
use MIME::Parser; 
use DateTime::Format::DateParse;
use File::Spec::Functions;
use File::Copy;
use Cwd;
use Time::gmtime;

{    # open user mailbox

	my $host = "127.0.0.1";
	my $id   = 'deyan.imap';
	my $pass = "123";

	my $imap = Mail::IMAPClient->new(
		Server   => $host,
		User     => $id,
		Password => $pass,
		Uid      => 0, # problem ?
		Clear    => 5,
		#Debug   	=> 1,
	  )
	  or die('Failed to connect to IMAP server!');

	#my $Authenticated = $imap->Authenticated();
	#my $Connected = $imap->Connected();

	my @folders = $imap->folders
	  or die("List folders error: $imap->LastError !");

	foreach my $folder (@folders) {
		print "MAP : opening folder $folder with "
		  . $imap->message_count($folder)
		  . " messages .";

		# open mailbox
		$imap->select($folder)
		  or die('Failed to open mailbox !');

		# print messages info
		my $msg_count = $imap->message_count($folder);
		if ( !$msg_count ) {

			# no messages !
			print "Folder $folder is empty !";
			next;
		}

		print "\n-------------------------------------\n";
		for ( my $i = 1 ; $i < $msg_count ; $i++ ) {
			
			print "\n---------MAIL--------------\n";
			my $hashref = {};
			$imap->fetch_hash( "UID", "INTERNALDATE", "RFC822.SIZE", $hashref );

			# get IMAP header
			my $uid = $imap->message_uid($i);
			if ( !$uid ) {
				print "Invalid UID for E-mail with ID=$i in Folder => $folder !";
				next;
			}

			# get Email sender
			$imap->get_header( $i, "From" ) =~ /(.*)<(.*)>/;
			my $email = "$2";
			$email = $imap->get_header( $i, "From" )
			  if ( not $email );

			print "UID:\t $uid \n";
			print "FROM:\t"
			  . $imap->get_header( $i, "From" )
			  . " | REAL => $email \n";
			print "TO:\t" . $imap->get_header( $i, "To" ) . "\n";
			print "CC:\t" . $imap->get_header( $i, "Cc" ) . "\n"
			  if defined $imap->get_header( $i, "Cc" );
			print "SUBJECT:\t" . $imap->get_header( $i, "Subject" ) . "\n";
			print "DATE:\t" . $hashref->{$uid}{'INTERNALDATE'} . "\n";
			print "SIZE:\t" . $hashref->{$uid}{'RFC822.SIZE'} . "\n";
			
			# get BODY words !
            #my @words = split(/\W/, $imap->body_string($uid) );
            #print "BODY:\t";
            #foreach my $word (@words) {
            #	print "$word,"
            #		if ( length($word) >= 3 );
            #}
            #print "\n";
			#print "BODY:\t" . $imap->body_string($uid) . "\n";
			#print "BODY:\t" . $imap->body_string( $uid ) . "\n";

			# get Message-ID
			my $msgID = $imap->get_header( $i, "Message-ID" );

			# get priority
			my $prio;
			if ( $imap->get_header( $i, "X-Priority" ) ) {
				$imap->get_header( $i, "X-Priority" ) =~ /(.*)(\d)(.*)/;
				$prio = "$2";
			}

			# get datetime
			my $dt =
			  DateTime::Format::DateParse->parse_datetime(
				$hashref->{$uid}{'INTERNALDATE'} );

			# attachments !?

			my $mp = new MIME::Parser;
			$mp->ignore_errors(1);
			$mp->extract_uuencode(1);
			$mp->decode_headers(1);
			$mp->output_under("../../../data/tmp");

			my $entity = $mp->parse_data( $imap->bodypart_string($uid) );

			my $num_parts = $entity->parts;
			my @parts     = $entity->parts;

			print "PARTS:\t $num_parts \n";

			if ($num_parts > 0) {
				my $j = 0;
				for my $part ( $entity->parts ) {
					if ( 0 == $j++ ) {
						print "\nPrinting body ....\n";
						# print body
						my $bodydata = $part->bodyhandle->as_string;
			            #if (my $io = $part->open("r")) {
			            #    while (defined($_ = $io->getline)) {
			            #        $bodydata .= $_;
			            #    }
		                #$io->close;
			            #}			
			            my @words = split(/\W/, $bodydata);
			            print "--BODY --2--\t";
			            foreach my $word (@words) {
			            	print "$word,"
			            		if ( length($word) >= 3 );
			            }			            	
						next;
					}
					
					# attachment
					my $type = $part->mime_type;
					my $enc = $part->head->mime_encoding;
					my $file_name = $part->head->recommended_filename;
					print "ATTACHMENT:\t $file_name | $enc | $type \n";
				}
			}
			else {
				# just body
				my $bodydata;
	            if (my $io = $entity->open("r")) {
	                while (defined($_ = $io->getline)) {
	                    $bodydata .= $_;
	                }
                $io->close;
	            }
	            #print "BODY:\t $bodydata \n";
	            my @words = split(/\W/, $bodydata);
	            print "--BODY\t";
	            foreach my $word (@words) {
	            	print "$word,"
	            		if ( length($word) >= 3 );
	            }
			}
			
			# cleanup created files
			$mp->filer->purge;
			
			#----
		}

		$imap->close;
	}

	# close IMAP
	$imap->logout
	  or die('disconnect() failed for IMAP client !');

}

