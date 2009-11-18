#!/bin/perl

use warnings;
use strict;

use HTTP::Daemon;
use HTTP::Status;
use JSON::XS;
use MIME::Base64;

sub res {
    HTTP::Response->new(
        RC_OK, OK => [ 'Content-Type' => 'text/plain' ], shift
    )
}


# ---------------


$|++;

  my $d = HTTP::Daemon->new(
           LocalAddr => '127.0.0.1',
           LocalPort => 8080,
  	) || die;
  	
  print "Please contact me at: <URL:", $d->url, ">\n";
  while (my $c = $d->accept) {
      while (my $r = $c->get_request) {
          if ($r->method eq 'GET' and $r->url->path eq "/json-test-1") {
              
			#my $data = { var1 => 'val1',
			#     var2 => [ 'first_element',
			#              { sub_element => 'sub_val', sub_element2 => 'sub_val2' },
			#           ],
            #  var3 => 'val3',
            #};
            
              my $data = { name => 'Petar',
              			   age => 26,
              			   likes => ['popcorn','movies','perl']
              };
            
              my $json_obj = JSON::XS->new->allow_nonref(1);
    		  my $json_text = $json_obj->encode($data);              
              ##my $json_text = $json_obj->utf8->encode( $data );
              my $r = res( $json_text );
			  $c->send_response( $r );              
          }
          elsif ($r->method eq 'GET' and $r->url->path eq "/json-test-2") {
			 #  return some bin data
			
			  my $data_file="SpaceTrader.exe";
			  open(DAT, $data_file) || die("Could not open file!");
			  my $raw_data=join("", <DAT>); ; 		
			  close(DAT);			
			  my $data_base64 = encode_base64( $raw_data );
			  print "Sending base64 data with len=" . length($data_base64) . "\n";
			  
			  open(TS,">my.exe") || die("Could not open file 2! ");
			  print TS $raw_data;
			  close(TS);
			
              my $data = { filename => $data_file,
              			   size => -s $data_file,
              			   data => $data_base64
              };
            
              my $json_obj = JSON::XS->new->allow_nonref(1);
    		  my $json_text = $json_obj->encode($data);              
              ##my $json_text = $json_obj->utf8->encode( $data );
              my $r = res( $json_text );
			  $c->send_response( $r );   
			  
		  }			  
          else {
              $c->send_error(RC_FORBIDDEN)
          }
      }
      $c->close;
      undef($c);
  }