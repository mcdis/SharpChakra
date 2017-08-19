cd SharpChakra
dotnet pack -c Release -o %~dp0
cd %~dp0

cd SharpChakra.Json
dotnet pack -c Release -o %~dp0
cd %~dp0

cd SharpChakra.Extensions
dotnet pack -c Release -o %~dp0
cd %~dp0
