@del *.ncb
@del *.wrn
@del *.log
@del *.err

rd bin /s /q
rd TestResults /s /q

cd driver
call clean.bat
cd ..

cd Driverlib
call clean.bat
cd ..

cd Test.Driver
call clean.bat
cd .. 

pause