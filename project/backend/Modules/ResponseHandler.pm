#******************************************************************************
# File: ResponseHandler.pm
# Description:  
# Author:       Petar Petrov <pro.xex@gmail.com>
#
# Copyright (c) 2009 Petar Petrov.  All rights reserved.
# This module is free software; you can redistribute it and/or modify
# it under the same terms as Perl itself.
#
#******************************************************************************

package Modules::ResponseHandler;

use base qw(Exporter);
our @EXPORT_OK = qw( RESP_OK, RESP_FAILED, RESP_ERR_DB_OPEN );

use constant RESP_OK => 0;
use constant RESP_FAILED => -1;
use constant RESP_ERR_DB_OPEN => -100;

my %responses = (
	RESP_OK 			=> q/Operation successful!/,
	RESP_FAILED 		=> q/Operation failed! Unknown reason !/,
	RESP_ERR_DB_OPEN 	=> q/Failed to open database !/
);

use strict;
use warnings;

use JSON::XS;
use MIME::Base64;
use Data::Dumper;
use Logger;

### globals
my $logger = Modules::Logger::create(__PACKAGE__);

sub new {
	my ($class) = @_;
	$class = ref($class) || $class;
	my $self = bless {
	}, $class;
	
	$logger->trace( 'created ResponseHandler' );	
	return $self;
}


sub response {
	my ($self, $data, $code, $desc ) = @_;
	
	# create JSON encoded response
	my $json_data = {
		'code' => $code,
		'desc' => $desc,
		'return' => $data
	};
	
    my $json_obj = JSON::XS->new->allow_nonref(1);
    my $json_enc = $json_obj->encode($json_data);              
    #my $json_enc = $json_obj->utf8->encode( $data );
	
	return $json_enc;  	
}

sub response_ok  {
	my ($self, $data) = @_;
	return $self->response( $data, RESP_OK, $responses{RESP_OK} );
}

sub response_fail {
	my ($self, $msg) = @_;
	
	$msg = defined $msg ? $msg : $responses{RESP_FAILED};
	$logger->trace("response_fail() : $msg");
	return $self->response( my $data, RESP_FAILED, $msg );	
}

sub response_error {
	my ($self, $code) = @_;
	return $self->response( my $data, $code, $responses{$code} );	
}

1