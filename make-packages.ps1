$iosProject = ".\FFImageLoading.ImageSourceHandler\FFImageLoading.ImageSourceHandler.csproj"

echo "  FFImageLoading.ImageSourceHandler project"
msbuild "$iosProject" /t:Clean,Restore,Build /p:Configuration=Release > build.txt

$version = (Get-Item FFImageLoading.ImageSourceHandler\bin\Release\FFImageLoading.ImageSourceHandler.dll).VersionInfo.FileVersion

echo "  packaging Sharpnado.Presentation.Forms.nuspec (v$version)"
nuget pack .\FFImageLoading.ImageSourceHandler.nuspec -Version $version > $null
