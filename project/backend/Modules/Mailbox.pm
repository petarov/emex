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

use File::Spec::Functions;
use File::Copy;
use Cwd;
use Log::Log4perl;
use DBI qw(:sql_types);
use Logger;

use constant DB_PATH => File::Spec->rel2abs( catfile( cwd(), '../../data/db' ) );
use constant DB_PROTOTYP_PATH => File::Spec->rel2abs( catfile( DB_PATH, 'prototype-acc.s3db' ) );

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
	
	# get db name
	my $userdb = $self->_getUserDBPath();
	
	# create OR open 
	if ( ! -e $userdb ) {
		$logger->info("Creating $userdb from prototype database ...");
		copy( DB_PROTOTYP_PATH, $userdb ) 
			or $logger->logdie("Failed copying " . DB_PROTOTYP_PATH . " to $userdb !");
	}
	else {
		$logger->debug("Database ($userdb) for user $self->{'email'} found !");
	}
	
	my $db_handle = DBI->connect("dbi:SQLite:dbname=$userdb","","") 
		|| $logger->logdie("Error connecting DBI : $DBI::errstr !");
				        
	# TODO: nothing needed, right now !
	$db_handle->disconnect();
}

sub _getUserDBPath {
	my $self = shift;
	# get db name
	my $userdb_name = $self->{'email'};
	$userdb_name =~ s/@/__/;
	$userdb_name .= '.s3db';
	my $userdb_path = catfile( DB_PATH, $userdb_name );
	
	return $userdb_path;
}

sub register {
	my ($self, $server, $user, $port, $ssl) = @_;

	my $userdb = $self->_getUserDBPath();
	my $db_handle = DBI->connect("dbi:SQLite:dbname=$userdb","","", {
      RaiseError => 0, AutoCommit => 0 } ) 
      || $logger->logdie("Error connecting DBI : $DBI::errstr !");
	
	# write SOMETHING into the DB
	my $sth; 
	
	$sth = $db_handle->prepare("INSERT INTO settings VALUES (?,?)");
	$sth->bind_param(1, 'email', SQL_VARCHAR);
	$sth->bind_param(2, $self->{'email'}, SQL_VARCHAR);
	$sth->execute();	
	
	$sth->bind_param(1, 'server', SQL_VARCHAR);
	$sth->bind_param(2, $server, SQL_VARCHAR);
	$sth->execute();	
	
	$sth->bind_param(1, 'user', SQL_VARCHAR);
	$sth->bind_param(2, $user, SQL_VARCHAR);
	$sth->execute();

	$sth->bind_param(1, 'port', SQL_VARCHAR);
	$sth->bind_param(2, $port, SQL_VARCHAR);
	$sth->execute();

	$sth->bind_param(1, 'ssl', SQL_VARCHAR);
	$sth->bind_param(2, $ssl, SQL_VARCHAR);
	$sth->execute();
	
	$db_handle->commit;
	$db_handle->disconnect();
}



1;