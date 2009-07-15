#!/bin/perl

use strict;
use warnings;

BEGIN {
	# specify modules path
	push @INC, "./Modules"; 
}

use HTTPServer;
use Config::Tiny; 

print "Loading server ....\n";

my $Config = Config::Tiny->new();
$Config = Config::Tiny->read( File::Spec->catfile( $ENV{'EMEX_PATH'}, 'data/conf/backend.conf') );
my $host = $Config->{_}->{'server.name'};
my $port = $Config->{_}->{'server.port'};

print "Starting server ($host:$port) ....\n";

my $srv = Modules::HTTPServer->new( $host, $port );
$srv->start();

print "Server stopped.\n";

