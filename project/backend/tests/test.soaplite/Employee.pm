#!/bin/perl

use warnings;
use strict;

package Employee;

=begin WSDL

_IN in $string
_RETURN $anyType

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

_RETURN $string

=cut

sub getName {
	return shift->{"Name"};
}

=begin WSDL

_RETURN $string

=cut

sub getTypeARE {
	my $self = shift;
	
	return 'whats done is done';
}

1
