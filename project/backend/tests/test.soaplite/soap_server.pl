#!/bin/perl

use warnings;
use strict;
use SOAP::Transport::HTTP;


print "server is starting ...";

# Create a simple server object
my $daemon = SOAP::Transport::HTTP::Daemon
     ->new(LocalAddr => localhost => LocalPort => 9002)
     ->dispatch_to('Employee')
     ->handle;
     
print "server started.";

package Employee;

sub new {
	
	print "new is called.";
	
    my $self = shift;
    my $class = ref($self) || $self;
    
   my $obj_ptr = {
    	"Name" => $_[0]
    };
    
    bless $obj_ptr => $class;

}

sub getName {
	return shift->{"Name"};
}
