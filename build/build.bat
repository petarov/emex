@echo off

REM *************************************
REM * EmEx Build Batch Script
REM *
REM *************************************

REM SET PROJECT_PATH=c:\prj\emex
SET DIST_PATH=dist
SET DIST_EMEX=%DIST_PATH%\emex

REM *** PREREQUISITES
IF "%PROJECT_PATH%" == "" GOTO errenv
IF NOT EXIST "%PROJECT_PATH%"\frontend\dotnet\bin\frontend_3_5.exe GOTO errfile 

mkdir dist 

echo Deleting old files ...
DEL /Q /F /S dist\*.*
rmdir /S /Q dist\backend
rmdir /S /Q dist\data
rmdir /S /Q dist\scripts
goto startbuild

REM *** ERROR ENV VARIABLES
:errenv
echo.
echo ERROR: Environment varaible PROJECT_PATH is not set! 
echo        Please set the variable to point to the checked out \project path - e.g. SET PROJECT_PATH=c:\emex\project
echo.
goto end

REM *** ERROR FILES
:errfile
echo.
echo ERROR: One or more binary files could not be found ! 
echo        If you haven't yet build the .NET frontend project, please do so and then run this script again.
echo.
goto end


:startbuild
echo .
echo Project path is %PROJECT_PATH%
echo .
echo Copying Folder structure ...
XCOPY %PROJECT_PATH%\data\conf\*.conf %DIST_EMEX%%\data\conf\ /E /C /F /R /Y
XCOPY %PROJECT_PATH%\data\conf\*.xml %DIST_EMEX%%\data\conf\ /E /C /F /R /Y
XCOPY %PROJECT_PATH%\data\db\prototype-acc.s3db %DIST_EMEX%%\data\db\ /E /C /F /R /Y
mkdir %DIST_EMEX%%\data\logs 
mkdir %DIST_EMEX%%\data\tmp 
XCOPY %PROJECT_PATH%\scripts\install-required-perl-modules.bat %DIST_EMEX%%\scripts\ /E /C /F /R /Y

echo Copying Backend files ...
XCOPY %PROJECT_PATH%\backend\*.pl %DIST_EMEX%%\backend\ /E /C /F /R /Y
XCOPY %PROJECT_PATH%\backend\Modules\*.pm %DIST_EMEX%%\backend\Modules\ /E /C /F /R /Y

echo Copying Frontend files ...
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\biztalk.dll %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\biztalk_appender.xml %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\emex-options.xml.template %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\frontend_3_5.exe %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\frontend_3_5.exe.config %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\frontend_appender.xml %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\log4net.dll %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\log4net.xml %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\Newtonsoft.Json.dll %DIST_EMEX%%\ /C /F /R /Y

echo Copying Additional files ...
XCOPY %PROJECT_PATH%\scripts\emex.bat %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\scripts\kill-perl-proc.bat %DIST_EMEX%%\ /C /F /R /Y

echo Creating archive ...
cd %DIST_PATH%
"..\7za" a "emex_dist.zip" emex\* -mx5 -xr!*\.svn\
cd ..
Echo Done.

:end
@echo on
