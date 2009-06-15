#!/bin/perl

use warnings;
use strict;

sub wsdl_generator {
	
	print "Using WSDL::Generator \n";
  	use WSDL::Generator;
  
  	my $init = {   'schema_namesp' => 'http://localhost:9002/Employee',
              'services'      => 'Employee',
              'service_name'  => 'Employee',
              'target_namesp' => 'http://localhost:9002/Employee',
              'documentation' => 'Emex services online',
              'location'      => 'http://localhost:9002/Employee' };
					              
  	my $wsdl = WSDL::Generator->new($init);
  	#ok(ref $wsdl eq 'WSDL::Generator', 'Init from Class::Hook');
  	my $obj = Employee->new('Stoqn');
  	#print CORE::dump( $obj );
  	$obj->getName( );
  	$obj->getType();
  
  	return $wsdl->get('Employee');
}

sub podwsdl_generator {
	
	print "Using POD::WSDL \n";
  	use Pod::WSDL;
  	
  	my $pod = new Pod::WSDL(source => 'Employee', 
    	location => 'http://localhost:9002',
    	pretty => 1,
    	withDocumentation => 0);
  
	return $pod->WSDL;	
}

sub spit_content( $ ) {
	
  my $content = $_[0];;
  
  open OUTFILE, ">test.wsdl";
  print OUTFILE $content;
  close OUTFILE;
  	
}

# -- MAIN -- 

print "Generator starting ... \n";

my $ret;

#$ret = wsdl_generator();
$ret = podwsdl_generator();

spit_content( $ret );

print "Done. \n"


   