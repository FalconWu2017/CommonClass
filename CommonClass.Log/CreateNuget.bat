@echo off
for %%i in (*.csproj) do nuget pack %%i -Prop Configuration=Release
@echo ����nuget����C:\Documentation\NuGet\Packages\Ŀ¼
copy *.nupkg C:\Documentation\NuGet\Packages\*.nupkg
pause