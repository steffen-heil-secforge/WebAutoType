@echo off
set version=6.9.2
set output=%~dp0v%version%\
set zipfile="%output%WebAutoType-v%version%.zip"
set buildoutputs="%~dp0Build Outputs"

rd /s /q "%output%"

copy "%~dp0..\Readme.txt" %buildoutputs%
rem copy "%~dp0..\COPYING" %buildoutputs%
:retry
copy "%~dp0..\WebAutoType\bin\Release\*.dll" %buildoutputs%
IF %ERRORLEVEL% NEQ 0 (
    echo.
    echo [31m Keepass still running, exit it first [0m
    echo.
    pause
    goto :retry
)

rem don't include pdb files
rem del %buildoutputs%\*.pdb

pushd %buildoutputs%
"%ProgramFiles%\7-Zip\7z.exe" a -tzip -mx9 -bd %zipfile% *
popd
copy "%~dp0..\Readme.txt" "%output%"

set version=
set output=
set zipfile=
set buildoutputs=