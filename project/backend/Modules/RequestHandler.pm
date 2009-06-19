package Modules::RequestHandler;

use strict;
use warnings;
use JSON::XS;
use MIME::Base64;

use constant {
	CMD_STOPSERVER => 1
};

### globals
my $logger = Modules::Logger::create(__PACKAGE__);	
my $stop = 0;

sub new {
	my ($class, $url) = @_;
	$class = ref($class) || $class;
	my $self = bless {
		'url' => $url
	}, $class;
	return $self;
}

sub handle_method_get {
	my ($self) = @_;
	$logger->trace('handle_method_get(): begin');
	
	return CMD_STOPSERVER;
}

sub handle_method_post {
	my ($self) = @_;
	$logger->trace('handle_method_post): begin');
		
}


1;