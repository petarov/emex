#!/bin/perl

use warnings;
use strict;

print "Generator starting ...";

  #use Pod::WSDL;
  #my $pod = new Pod::WSDL(source => 'Employee', 
  #  location => 'http://localhost:9002',
  #  pretty => 1,
  #  withDocumentation => 0);
  #print $pod->WSDL;


  use WSDL::Generator;
  my $init = {   'schema_namesp' => 'http://www.emex.org/Employee.xsd',
              'services'      => 'EmEx',
              'service_name'  => 'Employee',
              'target_namesp' => 'http://www.emex.org/SOAP/',
              'documentation' => 'Emex services online',
              'location'      => 'http://www.emex.org/SOAP/test.wsdl' };
					              
  my $wsdl = WSDL::Generator->new($init);
  #ok(ref $wsdl eq 'WSDL::Generator', 'Init from Class::Hook');
  my $obj = Employee->new('Stoqn');
  #print CORE::dump( $obj );
  $obj->getName( );
  $obj->getType();
  
  my $content = $wsdl->get('Employee');
  
  open OUTFILE, ">test.wsdl";
  print OUTFILE $content;
  close OUTFILE;

   