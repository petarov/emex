@echo off
REM #******************************************************************************
REM # File: 		install-required-perl-modules.bat
REM # Author:       Petar Petrov <pro.xex@gmail.com>
REM # Description:  Batch install of all required CPAN modules
REM #               NOTE: These are required for a "Strawberry Perl" installation !
REM #
REM #******************************************************************************

echo Installing required perl modules for EmEx ...

perl -MCPAN -e "fforce install Config::Tiny"
perl -MCPAN -e "fforce install Mail::POP3Client"
perl -MCPAN -e "fforce install Mail::IMAPClient"
perl -MCPAN -e "fforce install IO::Socket::SSL"
perl -MCPAN -e "fforce install GMail::IMAPD"
perl -MCPAN -e "fforce install MIME::Parser"
perl -MCPAN -e "fforce install JSON::XS"
perl -MCPAN -e "fforce install Log::Log4perl"
perl -MCPAN -e "fforce install Date::Parse"
perl -MCPAN -e "fforce install DateTime"
perl -MCPAN -e "fforce install DateTime::Format::DateParse"

echo !!Done !!

@echo on