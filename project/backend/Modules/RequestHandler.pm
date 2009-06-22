package Modules::RequestHandler;

use strict;
use warnings;

use HTTP::Daemon;
use HTTP::Status qw(:constants );
use JSON::XS;
use MIME::Base64;
use URI::Escape;

### globals
my $logger = Modules::Logger::create(__PACKAGE__);	
my %resources = (
	R_TEST => 'test'
);

sub new {
	my ($class, $uri, $url) = @_;
	$class = ref($class) || $class;
	my $self = bless {
		'uri' => $uri,
		'resource' => $url
	}, $class;
	
	$logger->trace( 'created RequestHandler : uri => ' . $self->{uri} . ' url => ' . $self->{resource} );	
	return $self;
}

sub handle_method_get {
	my ($self) = @_;
	$logger->trace('handle_method_get(): begin');

	my $response;
	
	$_ = $self->{resource};
	if ( /^quit/ ) {
		#TODO:
	}
	elsif ( /^\/$resources{R_TEST}/ ) {
		my $txt = "TESwer";
		$response = HTTP::Response->new( HTTP_OK );
		$response->header( 'Content-Type' => 'text/plain' );
		$response->header( 'Connection' => 'close' );	
		$response->content( $txt );		
	}
	
	return $response;
}

sub handle_method_post {
	my ($self) = @_;
	$logger->trace('handle_method_post): begin');
		
}

sub is_quit_signal {
	my ($self) = @_;
	if ( $self->{resource} =~ /^\/quit/ ) {
		return 1;
	}
	return 0;
}


1;