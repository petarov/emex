@echo off

echo Killing processes perl.exe ...

REM taskkill /F /PID 6180 /T
taskkill /F /IM perl.exe /T

@echo on