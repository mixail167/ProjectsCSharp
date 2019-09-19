@echo off

set Dir=%cd%
set Maska=*.sln
set Devenv=E:\VisualStudio\VisualStudioIDE\Common7\IDE\devenv.exe
set File=tmp.bat

if exist "%cd%\%File%" del /f "%cd%\%File%"

FOR /R %Dir% %%i IN (%Maska%) DO echo %Devenv% %%i /Rebuild>>"%cd%\%File%"

@echo on
call "%cd%\%File%"
@echo off
del /f "%cd%\%File%"
