#!/bin/perl

use warnings;
use strict;

package Employee;

=begin WSDL

_IN in $string
_RETURN $string

=cut

sub new {
	
	print "new is called.";
	
    my $self = shift;
    my $class = ref($self) || $self;
    
   my $obj_ptr = {
    	"Name" => $_[0]
    };
    
    bless $obj_ptr => $class;

}

=begin WSDL

_IN in $string
_RETURN $string

=cut

sub getName {
	return shift->{"Name"};
}

sub getType {
	return 'whats done is done';
}

1
