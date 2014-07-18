@echo off

set X86MachineMSBuildPath="C:\Program Files\MSBuild\12.0\bin\MSBuild.exe"
set X64Machine32BitMSBuildPath="C:\Program Files (x86)\MSBuild\12.0\bin\MSBuild.exe"
set X64Machine64BitMSBuildPath="C:\Program Files (x86)\MSBuild\12.0\bin\amd64\MSBuild.exe"

if exist %X86MachineMSBuildPath% (
 	rem echo Found in X86MachineMSBuildPath
	set MSBuildPath=%X86MachineMSBuildPath%
) else if exist %X64Machine32BitMSBuildPath% (
	rem echo Found in X64Machine32BitMSBuildPath
	set MSBuildPath=%X64Machine32BitMSBuildPath%
) else if exist %X64Machine64BitMSBuildPath% (
	rem echo Found in X64Machine64BitMSBuildPath
	set MSBuildPath=%X64Machine64BitMSBuildPath%
) else (
	echo MSBuild.exe path not found
)

rem echo %MSBuildPath%

if defined MSBuildPath (
	rem echo MSBuildPath defined
	call %MSBuildPath% NETDeveloperPracticum.sln /p:Configuration=Release /t:Clean;Build
	cls
	start "Order Food" OrderFood\bin\Release\OrderFood.exe
rem ) else (
	rem echo MSBuildPath not defined
)