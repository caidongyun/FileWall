del *.ncb
del *.wrn
del *.log
rd bin /s /q
rd TestResults /s /q

cd Benchmark
rd bin /s /q
rd obj /s /q
cd ..

cd RemotingTests\RemotingClient
rd bin /s /q
rd obj /s /q
cd ..\..

cd RemotingTests\RemotingServer
rd bin /s /q
rd obj /s /q
cd ..\..

cd Cleany2Ap
rd bin /s /q
rd obj /s /q
cd ..

cd Client
rd bin /s /q
rd obj /s /q
cd ..

cd Install
rd bin /s /q
rd obj /s /q
cd ..

cd Service
rd bin /s /q
rd obj /s /q
cd ..

cd Setup
rd Debug /s /q
rd Release /s /q
cd ..

cd Shared
rd bin /s /q
rd obj /s /q
cd ..

cd Test.Client
rd bin /s /q
rd obj /s /q
cd ..

cd Test.Service
rd bin /s /q
rd obj /s /q
cd ..

cd Test.Shared
rd bin /s /q
rd obj /s /q
cd ..

cd Test.TypesMarshaling
rd bin /s /q
rd Debug /s /q
cd ..

cd Driver
call clean.bat
cd ..

cd DriverLib
call clean.bat
cd ..

cd Test.Driver
call clean.bat
cd ..

pause