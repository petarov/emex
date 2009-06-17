package Logger::Base;

use strict;
use warnings;

use Log::Log4perl;
#use Log::Log4perl qw(get_logger);
use File::Spec;

use constant LOGGER_CONF => File::Spec->rel2abs( '../lib/Logger/logger-default.conf' );

sub init {
	my ($package) = @_;
	
	Log::Log4perl::init( LOGGER_CONF );
	my $logger = Log::Log4perl->get_logger($package);
	#$logger->error( "I've got something to say!" );
	
	return $logger;
}

sub log_message {
	my ($msg, $type) = @_;
	my $logger = Log::Log4perl->get_logger("");
	$logger->log( $type, $msg );
}

sub trace {
	my ($msg) = @_;
	my $logger = Log::Log4perl->get_logger("");
	$logger->trace($msg);
}

sub debug {
	my ($msg) = @_;
	my $logger = Log::Log4perl->get_logger("");
	$logger->debug($msg);
}

sub info {
	my ($msg) = @_;
	my $logger = Log::Log4perl->get_logger("");
	$logger->info($msg);
}

sub warn {
	my ($msg) = @_;
	my $logger = Log::Log4perl->get_logger("");
	$logger->warn($msg);
}

sub error {
	my ($msg) = @_;
	my $logger = Log::Log4perl->get_logger("");
	$logger->error($msg);
}

1;