#!/bin/perl

use warnings;
use strict;

  use SOAP::Lite;

  my $soap = SOAP::Lite
    -> uri('urn:Employee')
    -> proxy('http://localhost:9002');

  my $obj = $soap->call( new => "Stoqn")->result;

  print "Result= " . 
  	$soap-> getName($obj)-> result;