#******************************************************************************
# File: HTTPServer.pm
# Description:  
# Author:       Petar Petrov <pro.xex@gmail.com>
#
# Copyright (c) 2009 Petar Petrov.  All rights reserved.
# This module is free software; you can redistribute it and/or modify
# it under the same terms as Perl itself.
#
#******************************************************************************

package Modules::HTTPServer;
use strict;
use warnings;

use HTTP::Daemon;
use HTTP::Request;
use HTTP::Message;
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

sub start {
	my ($self, $host, $port) = @_;
	
	$logger->info(qq/Starting server on $self->{host}:$self->{port} ... /);
	
  	my $d = HTTP::Daemon->new(
           LocalAddr => $self->{host},
           LocalPort => $self->{port},
  	) || $logger->error_die("Failed to start HTTP server !");
  	
  	$logger->debug('Server URL is ' . $d->url);
  	
  	## start listening
  	
  	while(my $client = $d->accept  ) {
  		while(my $req = $client->get_request) {
  			if ( $req->method eq 'GET' ) {
  				my $req = Modules::RequestHandler->new( $req->uri, $req->url->path );
  				my $resp = $req->handle_method_get($req);
  				# is there a QUIT/EXIT signal ?
  				$stopit = $req->is_quit_signal();
  				$client->send_response( $resp );
  			}
  			elsif( $req->method eq 'POST' ) {
  				my $full_uri = $req->uri . '/?' .$req->content;  # just simulate like GET request
  				my $req = Modules::RequestHandler->new( $full_uri, $req->url->path );
  				my $resp = $req->handle_method_post();
  				$client->send_response( $resp );
  			}
  			else {
  				$logger->warn('Unhandled HTTP method passed -> ' . $req->method );
  				$client->send_response( HTTP_METHOD_NOT_ALLOWED );
  			}
  		}
  		
  		$logger->debug("Closing client ...");
  		$client->close;
  		undef($client);
  		# exit on signal ...
  		last if ( $stopit eq 1 );
  	}
  	  	
  	$logger->info('Server stopped.');
}

sub stop {
	my $self = shift;
	$stopit = 1;
	$logger->info('Server Stopping ... ');
}

sub send_basic_auth_request {
	my ($c, $realm)      = @_;
	$realm               = 'Restricted Area' if !$realm;
	my $auth_request_res = HTTP::Response->new(401, 'Unauthorized');
	$auth_request_res->header('WWW-Authenticate' => qq{Basic realm="$realm"});
	$auth_request_res->is_error(1);
    $auth_request_res->error_as_HTML(1);
	$c->send_response($auth_request_res);
}

sub decode_basic_auth {
	my ($auth) = @_;
	no warnings 'uninitialized';
    $auth = ( split /\s+/, $auth->header('Authorization') )[1] if ref $auth;
    require MIME::Base64;
    return split(/:/, MIME::Base64::decode( $auth ), 2);
}



1;