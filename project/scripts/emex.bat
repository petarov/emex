@echo off
REM #******************************************************************************
REM # File: 		emex.bat
REM # Author:       Petar Petrov <pro.xex@gmail.com>
REM # Description:  Startup script which sets paths and starts backend and frontend
REM #
REM #******************************************************************************

set EMEX_PATH=%~dp0

echo Starting Backend ...

cd backend
start "EmEx Backend" /NORMAL /I perl "bootstrap.pl"

cd ..
call frontend_3_5.exe

call kill-perl-proc.bat

@echo on