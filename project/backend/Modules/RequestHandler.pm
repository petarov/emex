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
use ResponseHandler;

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

    my $uri = URI->new( $self->{uri} );
	my $response;
	my $valid_params = 1;
	
	$_ = $self->{resource};
	### SYSTEM
	if ( /^$resources{R_QUIT}$/ ) {
		$quit_signal = 1;
	}
	elsif ( /^$resources{R_TEST}$/ ) {
		$response = http_response_ok( "This is a test request !" );
	}
	### APP
	elsif ( /^$resources{R_REGISTER_USER}$/ ) {
		# register new user database
		if ( http_validate_params( $uri, qw(email user server port ssl server_type) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->register( $uri->query_param('server'), 
										$uri->query_param('user'),
										$uri->query_param('port'),
										$uri->query_param('ssl'),
										$uri->query_param('server_type')
										); 
			$response = http_response_ok( $json );
		}
		else {
			$valid_params = 0;
		}
	}
	elsif ( /^$resources{R_UPDATE_USER_SETTINGS}$/ ) {
		# update user settings in user's database
	}
	elsif ( /^$resources{R_BUILD_MBOX}$/ ) {
		# build database from user mailbox
		if ( http_validate_params( $uri, qw(email) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->build_mbox( $uri->query_param('server') ); 
			$response = http_response_ok( $json );
		}
		else {
			$valid_params = 0;
		}		
	}
	elsif ( /^$resources{R_LIST_USERS}$/ ) {
		# get list of contacts
		if ( http_validate_params( $uri, qw(email) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->list_users(); 
			$response = http_response_ok( $json );
		}
		else {
			$valid_params = 0;
		}		
	}		
	### NOT FOUND
	else {
		$response = http_response_not_found();
	}
	
	if ( ! $valid_params ) {
		$response = http_response_ok( 
			Modules::ResponseHandler->new()->response_fail('Insufficient or incorrect params!') 
		);		
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

sub http_response_ok {
	my ($content) = @_;
	
	my $response = HTTP::Response->new( HTTP_OK );
	$response->header( 'Server' => $SERVER_INFO );
	$response->header( 'Content-Type' => 'text/plain' );
	$response->header( 'Connection' => 'close' );
	$response->header( 'Cache-Control' => 'max-age=86400' );	
	$response->content( $content );		
	return $response;
}

sub http_response_not_found {
	my $response = HTTP::Response->new( HTTP_NOT_FOUND );
	return $response;	
}

sub http_validate_params {
	my ($uri, @params_expected) = @_;
	
	foreach my $elem (@params_expected) {
		if ( ! $uri->query_param($elem) ) {
			#print 'elem:' . $elem . ' keys:' . $uri->query_param($elem) . '\n';
			$logger->trace('http_validate_params() : Element (' . $elem . ') was not found !');
			return 0;
		}
	}
	return 1;
}

1