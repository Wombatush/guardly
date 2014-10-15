@ECHO OFF
SET MSBUILDEXE="C:\Windows\Microsoft.NET\Framework64\v4.0.30319\MSBuild.exe"

PUSHD .nuget
FOR /F %%I IN ('CD') DO (SET NUGET_EXE=%%I\nuget.exe)
POPD

IF NOT EXIST %NUGET_EXE% GOTO:EOF_NO_NUGET

FOR /F %%I IN ('CD') DO (SET NUSPEC_SRC=%%I\Guardian.nuspec)

IF NOT EXIST %NUSPEC_SRC% GOTO:EOF_NO_NUSPEC

IF EXIST nugetpack_tmp RMDIR /S /Q nugetpack_tmp

MKDIR nugetpack_tmp
PUSHD nugetpack_tmp
MKDIR lib
MKDIR tools
MKDIR content
PUSHD lib
MKDIR net35
MKDIR net40
MKDIR net45
MKDIR net451
POPD
FOR /F %%I IN ('CD') DO (SET NUSPEC_DST=%%I)
FOR /F %%I IN ('CD') DO (SET LIB_DST=%%I\lib)
POPD

XCOPY %NUSPEC_SRC% %NUSPEC_DST% /q /i 

CALL:REBUILD Net35 %%1
CALL:REBUILD Net40 %%1
CALL:REBUILD Net45 %%1
CALL:REBUILD Net451 %%1

CALL %NUGET_EXE% pack %NUSPEC_DST%\Guardian.nuspec -noninteractive -version %1

REM MOVE /Y Guardian.%1.nupkg S:\NuGet\Repo

REM IF EXIST nugetpack_tmp RMDIR /S /Q nugetpack_tmp

GOTO:EOF

:REBUILD
PUSHD "Guardian.%1"
FOR /F %%I IN ('CD') DO (SET PROJECT_BASE=%%I)
IF EXIST bin RMDIR /S /Q bin
IF EXIST obj RMDIR /S /Q obj
%MSBUILDEXE% /p:Configuration=Debug;VersionAssembly=%2 "Guardian.%1.csproj" 
IF %ERRORLEVEL% NEQ 0 GOTO:BUILD_FAILED GOTO:EOF
%MSBUILDEXE% /p:Configuration=Release;VersionAssembly=%2 "Guardian.%1.csproj" 
IF %ERRORLEVEL% NEQ 0 GOTO:BUILD_FAILED GOTO:EOF
SET LIB_SRC=%PROJECT_BASE%\bin\Release\*
XCOPY "%LIB_SRC%" "%LIB_DST%\%1" /q /i 
POPD
GOTO:EOF

:BUILD_FAILED
POPD
ECHO BUILD FAILED
GOTO:EOF

:EOF_NO_NUGET
ECHO NuGet not found: %NUGET_EXE%
GOTO:EOF

:EOF_NO_NUSPEC
ECHO NuSpec was not found: %NUSPEC_SRC%
GOTO:EOF