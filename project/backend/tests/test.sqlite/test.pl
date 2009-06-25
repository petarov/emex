#! /bin/perl -w

  			use DBI qw(:sql_types);
        use Mail::IMAPClient;
        
        $host = "127.0.0.1";
        $id = 'deyan.imap';
        $pass = "123";


				print "Connecting to $host ... \n";        

        # returns an unconnected Mail::IMAPClient object:
        my $imap = Mail::IMAPClient->new;  
        #       ...                             
        # intervening code using the 1st object, then:
        # (returns a new, authenticated Mail::IMAPClient object)
        $imap = Mail::IMAPClient->new(  
                        Server => $host,
                        User    => $id,
                        Password=> $pass,
                        Clear   => 5,
                        #Debug		=> 1,
                        
        )       or die "Cannot connect to $host as $id: $@";
        
        
       my $Authenticated = $imap->Authenticated();
       my $Connected = $imap->Connected();
       
       print "Authenticated: $Authenticated \t Connected: $Connected \n";
       
       # list folders
       
       print "Listing mailboxes ... \n";
		       
		    my @folders = $imap->folders;
				#	push(@folders, "INBOX");
		
		    foreach my $folder (@folders) {
					my $message_count;       
					
					print "--- Listing Folder: $folder ---\n";
					print "Seen: " . $imap->message_count($folder) . "\tUnseen: " . $imap->message_count($folder) . " ! \n";
					
								
					# open mailbox								
			    $imap->select($folder)
			        or die "Could not examine $folder: $!";
			        
					# open db
					my $dbh = DBI->connect("dbi:SQLite:dbname=test.s3db","","");			        
			        
			    # print messages info
			    for( my $i = 1; $i < $imap->message_count($folder) + 1; $i++ ) {
			    	
			    	print "FROM:\t" . $imap->get_header($i, "From") . "\n";
			    	print "TO:\t" . $imap->get_header($i, "To") . "\n";
			    	print "CC:\t" . $imap->get_header($i, "Cc") . "\n" if defined $imap->get_header($i, "Cc");
			    	print "SUBJECT:\t" . $imap->get_header($i, "Subject") . "\n";
			    	print "DATE:\t" . $imap->get_header($i, "Message-Id") . "\n";
			    	
						# write into the DB
		  			my $sth = $dbh->prepare("INSERT INTO mails VALUES (?,?,?)");
		  			$sth->bind_param(2, $imap->get_header($i, "Message-Id"), SQL_VARCHAR);
		  			$sth->bind_param(3, $imap->get_header($i, "Subject"), SQL_VARCHAR);
		  			$sth->execute();				
			    }
			    
			    $imap->close;		
				}
       
				$imap->disconnect 
					or warn "Could not disconnect: $@\n";
       
       

       
       
