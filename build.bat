@echo off

set Dir=%cd%
set Maska=*.sln
set Devenv=E:\VisualStudio\IDE\Common7\IDE\devenv.exe
set File=tmp.bat
set Log=log.txt

if exist "%cd%\%File%" del /f "%cd%\%File%"

FOR /R %Dir% %%i IN (%Maska%) DO (
	echo "%Devenv%" "%%i" /rebuild Debug /Out "%cd%\%Log%">>"%cd%\%File%"
	echo "%Devenv%" "%%i" /rebuild Release /Out "%cd%\%Log%">>"%cd%\%File%"
)

@echo on
call "%cd%\%File%"
@echo off
del /f "%cd%\%File%"
start "%cd%\%Log%"