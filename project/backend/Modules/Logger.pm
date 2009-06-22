package Modules::Logger;

use strict;
use warnings;

use File::Spec;
use Env qw(EMEX_PATH);
use Cwd;
use Log::Log4perl;

use constant LOGGER_CONF => File::Spec->catfile( 
								File::Spec->rel2abs( 
									File::Spec->catfile( cwd(), '../../conf' ) ), 'logger-default.conf');
#print LOGGER_CONF;

sub create {
	my ($package) = @_;
	
	$| = 1;
	Log::Log4perl::init( LOGGER_CONF );
	my $logger = Log::Log4perl->get_logger($package);
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