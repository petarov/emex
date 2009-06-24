#******************************************************************************
# File: RequestHandler.pm
# Description:  
# Author:       Petar Petrov <pro.xex@gmail.com>
#
# Copyright (c) 2009 Petar Petrov.  All rights reserved.
# This module is free software; you can redistribute it and/or modify
# it under the same terms as Perl itself.
#
#******************************************************************************

package Modules::RequestHandler;

use strict;
use warnings;

use HTTP::Daemon;
use HTTP::Status qw(:constants );
use JSON::XS;
use MIME::Base64;
use URI;
use URI::QueryParam;
use Data::Dumper;
use Logger;
use Mailbox;

### globals
my $logger = Modules::Logger::create(__PACKAGE__);
my $SERVER_INFO = q(EmEx HTTP Deamon/1.0);
my $quit_signal = 0;
my %resources = (
    ## system actions ##
	R_TEST => '/test',
	R_QUIT => '/quit',
	## application actions ##
	R_REGISTER_USER => '/register_user',
	R_UPDATE_USER_SETTINGS => '/update_settings',
	R_BUILD_MBOX => '/build_mbox',
	R_UPDATE_MBOX => '/update_mbox',
	R_LIST_USERS => '/list_users',
	R_LIST_ATTACHMENTS => '/list_attachments',
	R_GET_EMAIL => '/get_email',
	R_SEND_EMAIL => '/send_email',
	R_SEARCH_EMAIL => '/search_email',
	R_CREATE_TAG => '/create_tag',
	R_REMOVE_TAG => '/remove_tag',
	
);


sub new {
	my ($class, $uri, $url) = @_;
	$class = ref($class) || $class;
	my $self = bless {
		'uri' => $uri,
		'resource' => $url
	}, $class;
	
	$logger->trace( 'created RequestHandler : uri => ' . $self->{uri} . ' url => ' . $self->{resource} );	
	return $self;
}


sub handle_method_get {
	my ($self,$request) = @_;
	$logger->trace('handle_method_get(): begin');

	my $response;
	
	$_ = $self->{resource};
	### SYSTEM
	if ( /^$resources{R_QUIT}$/ ) {
		$quit_signal = 1;
	}
	elsif ( /^$resources{R_TEST}$/ ) {
		$response = form_response_ok( "This is a test request !" );
	}
	### APP
	elsif ( /^$resources{R_REGISTER_USER}$/ ) {
		my $u1 = URI->new( $self->{uri} );
		#my %k = $u1->query_form  ;
		#print Dumper( %k );
  		#print $u1->query;    # prints foo=1&foo=2&foo=3
		  #for my $key ($u1->query_param) {
		  #   # print "$key: ", join(", ", $u1->query_param($key)), "\n";
		   #   print "$key: ", $u1->query_param($key), "\n"; 
		 # }
		
		my $mbox = Modules::Mailbox->new( $u1->query_param('name') );
		$mbox->register('localhost','bai_ivan','143','1'); 
		$response = form_response_ok( "This is REGISTER request !" );
		
	}
	### NOT FOUND
	else {
		$response = form_response_not_found();
	}
	
	return $response;
}


sub handle_method_post {
	my ($self) = @_;
	$logger->trace('handle_method_post(): begin');
		
	#TODO:
}

sub is_quit_signal {
	my ($self) = @_;
	#if ( $self->{resource} =~ /^$resources{R_QUIT}/ ) {
	#	return 1;
	#}
	return $quit_signal;
}


sub form_response_ok {
	my ($content) = @_;
	
	my $response = HTTP::Response->new( HTTP_OK );
	$response->header( 'Server' => $SERVER_INFO );
	$response->header( 'Content-Type' => 'text/plain' );
	$response->header( 'Connection' => 'close' );
	$response->header( 'Cache-Control' => 'max-age=86400' );	
	$response->content( $content );		
	return $response;
}

sub form_response_not_found {
	my $response = HTTP::Response->new( HTTP_NOT_FOUND );
	return $response;	
}

1;