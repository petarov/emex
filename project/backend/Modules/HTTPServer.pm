package Modules::HTTPServer;
use strict;
use warnings;

use HTTP::Daemon;
use HTTP::Status qw(:constants );
use Logger;
use RequestHandler qw(:constants );


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
  				my $req = Modules::RequestHandler->new( $req->url->path );
  				my $res = $req->handle_method_get();
  				$stopit = 1;
  				#if ( $res == RequestHandler::CMD_STOPSERVER ) {
  			#		$stopit = 1;
  			#	} 
  				
  				my $txt = "DIR";
    			my $resp = HTTP::Response->new( HTTP_OK, OK => [ 'Content-Type' => 'text/plain' ], $txt );			
  				$client->send_response( $resp );
  			}
  			elsif( $req->method eq 'POST' ) {
  				my $req = Modules::RequestHandler->new( $req->url->path );
  				$req->handle_method_post();

				my $txt = "DIR";
    			my $resp = HTTP::Response->new( HTTP_OK, OK => [ 'Content-Type' => 'text/plain' ], $txt );			
  				$client->send_response( $resp );
  			}
  			else {
  				$logger->warn('Unhandled HTTP method passed -> ' . $req->method );
  				$client->send_response( HTTP_NOT_FOUND );
  			}
  		}
  		$client->close;
  		undef($client);
  		$logger->debug("CLIENT END!");
  		last  ; #if ( $stopit eq 1 );
  			
  	}
  	  	
  	$logger->info('Server stopped.');
}

sub stop {
	my $self = shift;
	$stopit = 1;
	$logger->info('Server Stopping ... ');
}


1;