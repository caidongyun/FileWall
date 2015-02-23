echo Compiling driver
@Set WLHBASE=C:\WinDDK\7600.16385.1\

echo Compiling for XP
SET AP_PLATFORM=XP
call ..\ddkbuild -WLHXP checked .

echo Compiling for Vista
SET AP_PLATFORM=Vista
call ..\ddkbuild -WLH checked .

exit /b %error_code%