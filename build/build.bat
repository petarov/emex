REM *************************************
REM * EmEx Build Batch Script
REM *
REM *************************************
@echo off

SET PROJECT_PATH=c:\prj\emex
SET DIST_PATH=dist
SET DIST_EMEX=%DIST_PATH%\emex

mkdir dist 

echo Deleting old files ...
DEL /Q /F /S dist\*.*
rmdir /S /Q dist\backend
rmdir /S /Q dist\data
rmdir /S /Q dist\scripts
REM goto end

echo Copying Folder structure ...
XCOPY %PROJECT_PATH%\data\conf\*.conf %DIST_EMEX%%\data\conf\ /E /C /F /R /Y
XCOPY %PROJECT_PATH%\data\conf\*.xml %DIST_EMEX%%\data\conf\ /E /C /F /R /Y
XCOPY %PROJECT_PATH%\data\db\prototype-acc.s3db %DIST_EMEX%%\data\db\ /E /C /F /R /Y
mkdir %DIST_EMEX%%\data\logs 
mkdir %DIST_EMEX%%\data\tmp 
XCOPY %PROJECT_PATH%\scripts\*.bat %DIST_EMEX%%\scripts\ /E /C /F /R /Y

echo Copying Backend files ...
XCOPY %PROJECT_PATH%\backend\*.pl %DIST_EMEX%%\backend\ /E /C /F /R /Y
XCOPY %PROJECT_PATH%\backend\Modules\*.pm %DIST_EMEX%%\backend\Modules\ /E /C /F /R /Y

echo Copying Frontend files ...
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\biztalk.dll %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\biztalk_appender.xml %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\emex-options.xml.template %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\frontend_3_5.exe %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\frontend_3_5.exe.config%DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\frontend_3_5.exe.config %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\log4net.dll %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\log4net.xml %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\Newtonsoft.Json.dll %DIST_EMEX%%\ /C /F /R /Y

echo Copying Additional files ...
XCOPY %PROJECT_PATH%\scripts\emex.bat %DIST_EMEX%%\ /C /F /R /Y
XCOPY %PROJECT_PATH%\scripts\kill-perl-proc.bat %DIST_EMEX%%\ /C /F /R /Y

echo Creating archive ...
cd %DIST_PATH%
"..\7za" a "emex_dist.zip" emex\* -mx5 -xr!*\.svn\

:end
pause
@echo on
