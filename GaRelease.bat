@echo off
NAnt.exe -t:net-3.5 -D:project.config=release clean package

if %ERRORLEVEL%==0 (
	echo.
	echo Done!
	echo.
) else (
	echo.
	echo BUILD ERROR!
	echo.
	echo NAnt build error or NAnt not found. If you don't have NAnt,
	echo please install it from http://nant.sourceforge.net/
	echo and add it to your command line PATH.
	echo.
	echo.
	pause
)