$iosProject = ".\Xamarin.Forms.Nuke\Xamarin.Forms.Nuke.csproj"

echo "  Xamarin.Forms.Nuke project"
msbuild "$iosProject" /t:Clean,Restore,Build /p:Configuration=Release
if ($LastExitCode -gt 0)
{
    echo "  Error building Xamarin.Forms.Nuke"
    return
}

$version = (Get-Item Xamarin.Forms.Nuke\bin\Release\Xamarin.Forms.Nuke.dll).VersionInfo.FileVersion

echo "  packaging Xamarin.Forms.Nuke.nuspec (v$version)"
nuget pack .\Xamarin.Forms.Nuke.nuspec -Version $version
