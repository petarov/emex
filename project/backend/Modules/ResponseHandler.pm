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

package Modules::RequestHandler;

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
	my ($self, $data) = @_;
	
	#TODO: create JSON response
}


1