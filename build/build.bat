REM *************************************
REM * EmEx Build Batch Script
REM *
REM *************************************
@echo off

SET PROJECT_PATH=c:\prj\emex

echo Deleting old files ...
DEL /Q /F /S dist\*.*
rmdir /S /Q dist\backend
rmdir /S /Q dist\data
rmdir /S /Q dist\scripts
REM goto end

echo Copying Folder structure ...
XCOPY %PROJECT_PATH%\data\conf\*.conf dist\data\conf\ /E /C /F /R /Y
XCOPY %PROJECT_PATH%\data\conf\*.xml dist\data\conf\ /E /C /F /R /Y
XCOPY %PROJECT_PATH%\data\db\prototype-acc.s3db dist\data\db\ /E /C /F /R /Y
mkdir dist\data\logs 
mkdir dist\data\tmp 
XCOPY %PROJECT_PATH%\scripts\*.bat dist\scripts\ /E /C /F /R /Y

echo Copying Backend files ...
XCOPY %PROJECT_PATH%\backend\*.pl dist\backend\ /E /C /F /R /Y
XCOPY %PROJECT_PATH%\backend\Modules\*.pm dist\backend\Modules\ /E /C /F /R /Y

echo Copying Frontend files ...
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\biztalk.dll dist\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\biztalk_appender.xml dist\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\emex-options.xml.template dist\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\frontend_3_5.exe dist\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\frontend_3_5.exe.config dist\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\frontend_3_5.exe.config dist\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\log4net.dll dist\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\log4net.xml dist\ /C /F /R /Y
XCOPY %PROJECT_PATH%\frontend\dotnet\bin\Newtonsoft.Json.dll dist\ /C /F /R /Y

:end
pause
@echo on
