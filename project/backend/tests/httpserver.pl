#!/bin/perl

use strict;
use warnings;

BEGIN {
	# specify modules path
	push @INC, "../Modules"; 
}

use HTTPServer qw(new);
#use Logger::Base;

my $srv = Modules::HTTPServer->new('127.0.0.1','8080');
$srv->test_server();
$srv->start();

print "READY!";

#my $log = Logger::Base::init(__PACKAGE__);
#$log->debug("info");
#Logger::Base::trace("test");

