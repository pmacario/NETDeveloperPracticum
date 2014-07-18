@echo off

set X86MachineMSBuildPath="C:\Program Files\MSBuild\12.0\bin\MSBuild.exe"
set X64Machine32BitMSBuildPath="C:\Program Files (x86)\MSBuild\12.0\bin\MSBuild.exe"
set X64Machine64BitMSBuildPath="C:\Program Files (x86)\MSBuild\12.0\bin\amd64\MSBuild.exe"

rem Find the MSBuild.exe file path.
if exist %X86MachineMSBuildPath% (
	set MSBuildPath=%X86MachineMSBuildPath%
) else if exist %X64Machine32BitMSBuildPath% (
	set MSBuildPath=%X64Machine32BitMSBuildPath%
) else if exist %X64Machine64BitMSBuildPath% (
	set MSBuildPath=%X64Machine64BitMSBuildPath%
) else (
	echo MSBuild.exe path not found
)

if defined MSBuildPath (
	rem If the MSBuild.exe file path has been located, use it to 
	rem clean and build the solution.
	call %MSBuildPath% NETDeveloperPracticum.sln /p:Configuration=Release /t:Clean;Build
	cls
	rem Launch the console application to allow for testing.
	start "Order Food" OrderFood\bin\Release\OrderFood.exe
)