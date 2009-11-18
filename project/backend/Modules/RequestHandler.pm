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
	R_EDIT_CONTACT => '/edit_contact',
	R_LIST_MAILS => '/list_mails',
	R_LIST_CONTACT_MAILS => '/list_contact_mails',
	R_LIST_MAILS_BY_SEARCHTAG => '/list_mails_by_searchtag',
	R_LIST_CONTACT_ATTACHMENTS => '/list_contact_attachments',
	R_LIST_ATTACHMENTS => '/list_attachments',
	R_GET_ATTACHMENT => '/get_attachment',
	R_GET_EMAIL => '/get_email',
	R_SEND_EMAIL => '/send_email',
	R_SEARCH_EMAIL => '/search_email',
	R_LIST_TAGS => '/list_tags',
	R_CREATE_TAG => '/create_tag',
	R_REMOVE_TAG => '/remove_tag',
	R_PING => '/ping'
	
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
		if ( http_validate_params( $uri, qw(email username full_name incoming_server incoming_port incoming_security server_type smtp_server smtp_port smtp_security) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->register( $uri->query_param('username'), 
										$uri->query_param('full_name'),
										$uri->query_param('incoming_server'),
										$uri->query_param('incoming_port'),
										$uri->query_param('incoming_security'),
										$uri->query_param('server_type'),
										$uri->query_param('smtp_server'),
										$uri->query_param('smtp_port'),
										$uri->query_param('smtp_username'),
										$uri->query_param('smtp_security')
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
		if ( http_validate_params( $uri, qw(email pass) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->build_mbox( $uri->query_param('pass') ); 
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
	elsif ( /^$resources{R_EDIT_CONTACT}$/ ) {
		# edit contact info
		if ( http_validate_params( $uri, qw(email id) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->edit_contact( $uri->query_form_hash ); 
			$response = http_response_ok( $json );
		}
		else {
			$valid_params = 0;
		}		
	}	
	elsif ( /^$resources{R_LIST_MAILS}$/ ) {
		# get list of all e-mails
		if ( http_validate_params( $uri, qw(email) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->list_mails(); 
			$response = http_response_ok( $json );
		}
		else {
			$valid_params = 0;
		}		
	}	
	elsif ( /^$resources{R_LIST_TAGS}$/ ) {
		# get list of all tags
		if ( http_validate_params( $uri, qw(email) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->list_tags(); 
			$response = http_response_ok( $json );
		}
		else {
			$valid_params = 0;
		}		
	}		
	elsif ( /^$resources{R_LIST_CONTACT_MAILS}$/ ) {
		# get list of e-mails for given contact
		if ( http_validate_params( $uri, qw(email id) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->list_contact_mails( $uri->query_param('id') ); 
			$response = http_response_ok( $json );
		}
		else {
			$valid_params = 0;
		}		
	}		
	elsif ( /^$resources{R_LIST_ATTACHMENTS}$/ ) {
		# get list of attachments
		if ( http_validate_params( $uri, qw(email) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->list_attachments(); 
			$response = http_response_ok( $json );
		}
		else {
			$valid_params = 0;
		}		
	}
	elsif ( /^$resources{R_LIST_CONTACT_ATTACHMENTS}$/ ) {
		# get list of attachments
		if ( http_validate_params( $uri, qw(email id) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->list_contact_attachments( $uri->query_param('id') ); 
			$response = http_response_ok( $json );
		}
		else {
			$valid_params = 0;
		}		
	}	
	elsif ( /^$resources{R_GET_ATTACHMENT}$/ ) {
		# download attachment
		if ( http_validate_params( $uri, qw(email id pass) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->get_attachment( $uri->query_param('id'), $uri->query_param('pass') ); 
			$response = http_response_ok( $json );
		}
		else {
			$valid_params = 0;
		}		
	}	
	elsif ( /^$resources{R_LIST_MAILS_BY_SEARCHTAG}$/ ) {
		# get list of e-mails that match tag
		if ( http_validate_params( $uri, qw(email tagid) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->list_mails_by_searchtag( $uri->query_param('tagid') ); 
			$response = http_response_ok( $json );
		}
		else {
			$valid_params = 0;
		}		
	}	
	elsif ( /^$resources{R_GET_EMAIL}$/ ) {
		# get specific e-mail
		if ( http_validate_params( $uri, qw(email id pass) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->get_email( $uri->query_param('id'), $uri->query_param('pass') ); 
			$response = http_response_ok( $json );
		}
		else {
			$valid_params = 0;
		}		
	}		
	elsif ( /^$resources{R_SEARCH_EMAIL}$/ ) {
		# search e-mail by tag 
		if ( http_validate_params( $uri, qw(email word) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->search( $uri->query_param('word') ); 
			$response = http_response_ok( $json );
		}
		else {
			$valid_params = 0;
		}		
	}		
	elsif ( /^$resources{R_PING}$/ ) {
		$response = http_response_ok( 
			Modules::ResponseHandler->new()->response_ok() );
	}	
	### 404 NOT FOUND
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
	
    my $uri = URI->new( $self->{uri} );
	my $response;
	my $valid_params = 1;
	
	$_ = $self->{resource};

	if ( /^$resources{R_SEND_EMAIL}$/ ) {
		if ( http_validate_params( $uri, qw(email to subject body) ) ) {
			my $mbox = Modules::Mailbox->new( $uri->query_param('email') );
			my $json = $mbox->send_mail( 
				'TO' => $uri->query_param('to'),
				'CC' => $uri->query_param('cc'),
				'BCC' => $uri->query_param('bcc'),
				'SUBJECT' => $uri->query_param('subject'),
				'BODY' => $uri->query_param('body'),
				'PASS' => $uri->query_param('pass')
				 ); 
			$response = http_response_ok( $json );
		}
		else {
			$valid_params = 0;
		}
	}	
	### 404 NOT FOUND
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