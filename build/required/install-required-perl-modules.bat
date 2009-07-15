@echo off

echo Installing Perl Modules ...

perl -MCPAN -e "install Config::Tiny"
perl -MCPAN -e "install Mail::POP3Client"
perl -MCPAN -e "install Mail::IMAPClient"
perl -MCPAN -e "install MIME::Parser"
perl -MCPAN -e "install JSON::XS"
perl -MCPAN -e "install Log::Log4perl"
perl -MCPAN -e "install Date::Parse"
perl -MCPAN -e "install DateTime"
perl -MCPAN -e "install DateTime::Format::DateParse"

echo !!Done !!

@echo on