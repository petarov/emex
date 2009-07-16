REM *************************************
REM * EmEx Startup Script
REM *
REM *************************************
@echo off

echo Starting Backend ...

cd backend
REM cmd /C "perl bootstrap.pl"
start "EmEx Backend" /NORMAL /I perl "bootstrap.pl"

cd ..
call frontend_3_5.exe

@echo on