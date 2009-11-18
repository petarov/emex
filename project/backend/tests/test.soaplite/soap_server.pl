#!/bin/perl

use warnings;
use strict;
use SOAP::Transport::HTTP;
use Employee;

print "server is starting ...";

# Create a simple server object
my $daemon = SOAP::Transport::HTTP::Daemon
     ->new(LocalAddr => localhost => LocalPort => 9002)
     ->dispatch_to('Employee')
     ->handle;
     
print "server started.";

