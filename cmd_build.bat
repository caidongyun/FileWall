rem Windows XP
cmd /c "build_one.bat fre x86 WXP %CD%"
pause

rem Windows Server 2003
cmd /c "build_one.bat fre x86 WNET %CD%"
cmd /c "build_one.bat fre x64 WNET %CD%"

rem Windows Vista and Windows Server 2008
cmd /c "build_one.bat fre x86 WLH %CD%"
cmd /c "build_one.bat fre x64 WLH %CD%"

rem Windows 7
cmd /c "build_one.bat fre x86 WIN7 %CD%"
cmd /c "build_one.bat fre x64 WIN7 %CD%"
pause