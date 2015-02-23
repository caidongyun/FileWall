echo Reinstalling the driver...
@net stop FileWall
@rundll32 syssetup,SetupInfObjectInstallAction DefaultInstall 128 bin\FileWall.inf

echo Starting SoftICE...
@net start ntice
echo Reloading symbols...
@SET ldr="C:\Program Files\Compuware\DriverStudio\SoftICE\nmsym"
@%ldr% /UNLOAD:TestDriver.sys
@%ldr% /LOAD			/SOURCE:driver		bin\i386\TestDriver.sys

echo Some breakpoints before start?
@pause

cfix32 -kern bin\i386\TestDriver.sys
rem @SET PATH="C:\Program Files\Microsoft Visual Studio 9.0\Common7\IDE";%PATH%
rem mstest /testcontainer:bin\Test.VitaliiPianykh.System.dll /noresults /noisolation
pause
