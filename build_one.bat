call "C:\WinDDK\7600.16385.1\bin\setenv.bat" C:\WinDDK\7600.16385.1\ %1 %2 %3 no_oacr
echo %4
cd /d %4
build.exe -cef