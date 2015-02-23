echo Removing trash files
@del *.ncb
@del *.wrn
@del *.log
@del *.err

echo Remove [Debug] dir
@rd Debug /s /q

echo Release [Release] dir
@rd Release /s /q

echo Release [bin] dir
@rd bin /s /q

echo Release [obj] dir
@rd obj /s /q

for /f %%a IN ('dir /b objchk_*') do @rd %%a /s /q

for /f %%a IN ('dir /b objfre_*') do @rd %%a /s /q