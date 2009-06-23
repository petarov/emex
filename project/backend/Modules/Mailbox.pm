#******************************************************************************
# File: Mailbox.pm
# Description:  
# Author:       Petar Petrov <pro.xex@gmail.com>
#
# Copyright (c) 2009 Petar Petrov.  All rights reserved.
# This module is free software; you can redistribute it and/or modify
# it under the same terms as Perl itself.
#
#******************************************************************************

package Modules::Mailbox;

use strict;
use warnings;

use File::Spec;
use Cwd;
use Log::Log4perl;
use DBI qw(:sql_types);
use Logger;

use constant DB_PATH => File::Spec->rel2abs( File::Spec->catfile( cwd(), '../../data/db' ) );

### globals
my $logger = Modules::Logger::create(__PACKAGE__);


sub new {
	my ($class, $email) = @_;
	$class = ref($class) || $class;
	my $self = bless {
		'email' => $email
	}, $class;
	
	$logger->trace( 'created Mailbox : email => ' . $email );
	$self->_open();
		
	return $self;
}

# try to open existing database
sub _open {
	my $self = shift;
	
	# open db
	my $db_path = File::Spec->catfile( DB_PATH, 'test-acc.s3db' );
	my $dbh = DBI->connect("dbi:SQLite:dbname=$db_path","","") 
		|| $logger->logdie("Error connecting DBI : $DBI::errstr ");
				        
	# write into the DB
	my $sth = $dbh->prepare("INSERT INTO settings VALUES (?,?)");
	$sth->bind_param(1, 'email', SQL_VARCHAR);
	$sth->bind_param(2, $self->{'email'}, SQL_VARCHAR);
	$sth->execute();	
	
	$dbh->disconnect();
	
}

sub register {
	my ($self, $email, $server, $user, $port, $ssl) = @_;
	
	
}



1;