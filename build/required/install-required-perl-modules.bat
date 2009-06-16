@echo off

echo Installing Perl Modules ...

perl -MCPAN -e "install Mail::IMAPClient"
perl -MCPAN -e "install JSON::XS"
perl -MCPAN -e "install Log::Log4perl"

echo !!Done !!

@echo on