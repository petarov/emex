package Modules::HTTPServer;
use strict;
use warnings;

use HTTP::Daemon;
use HTTP::Status qw(:constants );
use Logger;
use RequestHandler;

### globals
my $logger = Modules::Logger::create(__PACKAGE__);	
my $stopit = 0;

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
	$logger->trace("TEST HTTP");
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
  				my $req = Modules::RequestHandler->new( $req->uri, $req->url->path );
  				my $resp = $req->handle_method_get();
  				
  				# is there a QUIT/EXIT signal ?
  				$stopit = $req->is_quit_signal();
  				
  				$client->send_response( $resp );
  				$logger->debug("Response is sent!");
  			}
  			elsif( $req->method eq 'POST' ) {
  				my $req = Modules::RequestHandler->new( $req->url->path );
  				$req->handle_method_post();

				my $txt = "DIR";
    			my $resp = HTTP::Response->new( HTTP_OK, OK => [ 'Content-Type' => 'text/plain', 'Connection' => 'close' ], $txt );
  				$client->send_response( $resp );
  			}
  			else {
  				$logger->warn('Unhandled HTTP method passed -> ' . $req->method );
  				$client->send_response( HTTP_NOT_FOUND );
  			}
  			$logger->debug("REQ END!");
  		}
  		
  		$logger->debug("Closing client ...");
  		$client->close;
  		undef($client);
  		
  		last if ( $stopit eq 1 );
  	}
  	  	
  	$logger->info('Server stopped.');
}

sub stop {
	my $self = shift;
	$stopit = 1;
	$logger->info('Server Stopping ... ');
}


1;