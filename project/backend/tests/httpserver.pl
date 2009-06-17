#!/bin/perl

use strict;
use warnings;

BEGIN {
	# specify modules path
	push @INC, "../lib"; 
}

use HTTPServer::Core;
use Logger::Base;

HTTPServer::Core::test_server();

my $log = Logger::Base::init(__PACKAGE__);
$log->debug("info");
Logger::Base::trace("test");

