@echo off
for %%i in (*.csproj) do nuget pack %%i -Prop Configuration=Release
@echo 复制nuget包到C:\Documentation\NuGet\Packages\目录
copy *.nupkg C:\Documentation\NuGet\Packages\*.nupkg
pause