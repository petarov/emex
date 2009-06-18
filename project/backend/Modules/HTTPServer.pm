package Modules::HTTPServer;
use strict;
use warnings;

use HTTP::Daemon;
use HTTP::Status;
use JSON::XS;
use MIME::Base64;
use Logger;

### globals
my $logger = Modules::Logger::create(__PACKAGE__);	
my $stop = 0;

sub new {
	my ($class, $host, $port) = @_;
	$class = ref($class) || $class;
	my $self = bless {
		'host' => $host,
		'port' => $port,
	}, $class;
	return $self;
}

sub test_server {
	my $self = shift;
	$logger->trace("TEST");
}

sub start {
	my ($self,$host,$port) = @_;
	
	$logger->info(qq/Starting server on $self->{host}:$self->{port} ... /);
	
  	my $d = HTTP::Daemon->new(
           LocalAddr => $self->{host},
           LocalPort => $self->{port},
  	) || die('FAILD');
  	
  	$logger->debug('Server URL is ' . $d->url);
  	
  	## start listening
  	
  	while(my $client = $d->accept  ) {
  		while(my $req = $client->get_request) {
  			if ( $req->method eq 'GET' ) {
  				print 'Recieved request ' . $req->url->path;
  			}
  		}
  		$client->close;
  		undef($client);
  	}
  	  	
  	$logger->info('Server stopped.');
}

sub stop {
	my $self = shift;
	$stop = 1;
	$logger->info('Server Stopping ... ');
}


1;