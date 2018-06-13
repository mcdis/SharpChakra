cd src/SharpChakra
dotnet pack -c Release -o %~dp0
cd %~dp0

cd src/SharpChakra.Json
dotnet pack -c Release -o %~dp0
cd %~dp0

cd src/SharpChakra.Extensions
dotnet pack -c Release -o %~dp0
cd %~dp0