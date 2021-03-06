@echo off
goto menu

:menu
echo Build Project Generator:
echo.
echo [1] Visual C++ 2012
echo [2] Visual C++ 2013
echo [3] GNU Make
echo.

:choice
set /P C="Choice: "
if "%C%"=="3" goto gmake
if "%C%"=="2" goto vs2013
if "%C%"=="1" goto vs2012
if "%C%"=="0" goto clean

:clean
"premake5" --file=premake4.lua clean
goto quit

:vs2012
"premake5" --file=premake4-gmake.lua --outdir=vs2012 gmake x86_64-pc-windows-gnu
"premake5" --file=premake4-gmake.lua --outdir=vs2012 gmake i686-pc-windows-gnu
"premake5" --file=premake4.lua vs2012
goto quit

:vs2013
"premake5" --file=premake4-gmake.lua --outdir=vs2013 gmake x86_64-pc-windows-gnu
"premake5" --file=premake4-gmake.lua --outdir=vs2013 gmake i686-pc-windows-gnu
"premake5" --file=premake4.lua vs2013
goto quit

:gmake
"premake5" --file=premake4-gmake.lua --outdir=gmake gmake x86_64-pc-windows-gnu
"premake5" --file=premake4-gmake.lua --outdir=gmake gmake i686-pc-windows-gnu
"premake5" --file=premake4.lua gmake
goto quit

:quit
pause
:end