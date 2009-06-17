package HTTPServer::Core;

use strict;
use warnings;

use HTTP::Daemon;
use HTTP::Status;
use JSON::XS;
use MIME::Base64;

my $log;

BEGIN {
	# specify modules path
	push @INC, "../"; 
	use Logger::Base;
	$log = Logger::Base::init(__PACKAGE__);	
}

sub test_server {
	$log->trace("TEST");
}

sub start_server {
	my ($host,$port) = @_;
	
  	my $d = HTTP::Daemon->new(
           LocalAddr => '127.0.0.1',
           LocalPort => 8080,
  	) || die();
  		
}


1;