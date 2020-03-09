$iosProject = ".\Xamarin.Forms.Nuke\Xamarin.Forms.Nuke.csproj"

echo "  Xamarin.Forms.Nuke project"
msbuild "$iosProject" /t:Clean,Restore,Build /p:Configuration=Release > build.txt

$version = (Get-Item Xamarin.Forms.Nuke\bin\Release\Xamarin.Forms.Nuke.dll).VersionInfo.FileVersion

echo "  packaging Xamarin.Forms.Nuke.nuspec (v$version)"
nuget pack .\Xamarin.Forms.Nuke.nuspec -Version $version > $null
