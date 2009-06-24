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
use ResponseHandler;

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
	$self->_create_or_open();
		
	return $self;
}

# try to open existing database
sub _create_or_open {
	my $self = shift;
	
	# get db name
	my $userdb = $self->_get_user_dbfile();
	
	# create OR open 
	if ( ! -e $userdb ) {
		$logger->info("Creating $userdb from prototype database ...");
		copy( DB_PROTOTYP_PATH, $userdb ) 
			or $logger->logdie("Failed copying " . DB_PROTOTYP_PATH . " to $userdb !");
	}
	else {
		$logger->debug("Database ($userdb) for user $self->{'email'} found !");
	}
	
	my $db_handle = $self->_open_database( $userdb );
	# TODO: nothing needed, right now !
	$db_handle->disconnect() if ( $db_handle );
}

sub _get_user_dbfile {
	my $self = shift;

	my $userdb_name = $self->{'email'};
	$userdb_name =~ s/@/__/;
	$userdb_name .= '.s3db';
	
	return catfile( DB_PATH, $userdb_name );
}

sub _open_database {
	my ($self, $db_file, $transactions) = @_;
	
	my $db_handle = DBI->connect( "dbi:SQLite:dbname=$db_file", "", "", 
		{ RaiseError => 0, AutoCommit => $transactions ? 0 : 1 } );
		
	if ( $db_handle ) {
		# set error handler callback
		$db_handle->{HandleError} = sub {
			$logger->error("DBI => $_[0]");
		};		
	} 
	else {
		$logger->error("Error connecting DBI ($db_file) : $DBI::errstr !");
	}
	
	return $db_handle;	
}

sub register {
	my ($self, $server, $user, $port, $ssl) = @_;

	my $db_handle = $self->_open_database( $self->_get_user_dbfile() )
		|| return Modules::ResponseHandler->new()->response_failed('Failed to open database!');

	my $sth; 
	
	$sth = $db_handle->prepare("DELETE FROM settings"); # nasty hack, I admit ;)
	$sth->execute;
	
	$sth = $db_handle->prepare("INSERT INTO settings VALUES (?,?,?)");
  	$sth->bind_param_array(1, [ 'email', 'server', 'user', 'port', 'ssl' ], SQL_VARCHAR);
  	$sth->bind_param_array(2, [$self->{'email'}, $server, $user, $port, $ssl ], SQL_VARCHAR);
  	$sth->bind_param_array(3, time(), SQL_DATETIME ); # scalar will be reused for each row
	$sth->execute_array( { ArrayTupleStatus => \my @tuple_status } );
	
	$db_handle->commit;
	$db_handle->disconnect();
	
	return Modules::ResponseHandler->new()->response_ok();
}



1;