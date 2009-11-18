@echo off

echo Killing all perl.exe processes ...

REM taskkill /F /PID 6180 /T

taskkill /F /IM perl.exe /T

echo Goodbye.

@echo on