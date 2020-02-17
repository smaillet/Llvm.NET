Param(
    [string]$Configuration="Release",
    [switch]$AllowVsPreReleases,
    [switch]$NoClean,
    [ValidateSet('All','Source','Docs')]
    [System.String]$BuildMode = 'All'
)

. .\buildutils.ps1

# Main Script entry point -----------
pushd $PSScriptRoot
$oldPath = $env:Path
$ErrorActionPreference = "Stop"
$InformationPreference = "Continue"
$BuildSource = $false
$BuildDocs = $false;
switch($BuildMode)
{
'All' { $BuildSource = $true; $BuildDocs = $true; }
'Source' { $BuildSource = $true }
'Docs' { $BuildDocs = $true }
}

# for an automated build, get the ISO-8601 formatted time stamp of the HEAD commit
$isAutomatedBuild = $env:CI -or $env:IsAutomatedBuild
if($isAutomatedBuild -and !$env:BuildTime)
{
    $env:BuildTime = (git show -s --format=%cI)
}

try
{
    $msbuild = Find-MSBuild -AllowVsPrereleases:$AllowVsPreReleases
    if( !$msbuild )
    {
        throw "MSBuild not found"
    }

    if( !$msbuild.FoundOnPath )
    {
        $env:Path = "$env:Path;$($msbuild.BinPath)"
    }

    # setup standard MSBuild logging for this build
    $msbuildLoggerArgs = @('/clp:Verbosity=Minimal')

    if (Test-Path "C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll")
    {
        $msbuildLoggerArgs = $msbuildLoggerArgs + @("/logger:`"C:\Program Files\AppVeyor\BuildAgent\Appveyor.MSBuildLogger.dll`"")
    }

    $buildPaths = Get-BuildPaths $PSScriptRoot

    Write-Information "Build Paths:"
    Write-Information ($buildPaths | Format-Table | Out-String)

    if($BuildSource)
    {
        if( (Test-Path -PathType Container $buildPaths.BuildOutputPath) -and !$NoClean )
        {
            Write-Information "Cleaning output folder from previous builds"
            rd -Recurse -Force -Path $buildPaths.BuildOutputPath
        }

        md $buildPaths.NuGetOutputPath -ErrorAction SilentlyContinue| Out-Null

        $BuildInfo = Get-BuildInformation $buildPaths
        if($env:APPVEYOR)
        {
            Write-Information "Updating APPVEYOR version: $($BuildInfo.FullBuildNumber)"
            Update-AppVeyorBuild -Version "$($BuildInfo.FullBuildNumber) [$([DateTime]::Now)]"
        }

        $packProperties = @{ version=$($BuildInfo.PackageVersion)
                             llvmversion=$($BuildInfo.LlvmVersion)
                             buildbinoutput=(normalize-path (Join-path $($buildPaths.BuildOutputPath) 'bin'))
                             configuration=$Configuration
                           }

        $msBuildProperties = @{ Configuration = $Configuration
                                FullBuildNumber = $BuildInfo.FullBuildNumber
                                PackageVersion = $BuildInfo.PackageVersion
                                FileVersionMajor = $BuildInfo.FileVersionMajor
                                FileVersionMinor = $BuildInfo.FileVersionMinor
                                FileVersionBuild = $BuildInfo.FileVersionBuild
                                FileVersionRevision = $BuildInfo.FileVersionRevision
                                FileVersion = $BuildInfo.FileVersion
                                LlvmVersion = $BuildInfo.LlvmVersion
                              }

        Write-Information "Build Parameters:"
        Write-Information ($BuildInfo | Format-Table | Out-String)

        # Download and unpack the LLVM libs if not already present, this doesn't use NuGet as the NuGet compression
        # is insufficient to keep the size reasonable enough to support posting to public galleries. Additionally, the
        # support for native lib projects in NuGet is tenuous at best. Due to various compiler version dependencies
        # and incompatibilities libs are generally not something published in a package. However, since the build time
        # for the libraries exceeds the time allowed for most hosted build services these must be pre-built for the
        # automated builds.
        Install-LlvmLibs $buildPaths.LlvmLibsRoot "8.0.0" "msvc" "15.9"

        .\Build-Interop.ps1 -BuildInfo $BuildInfo

        Write-Information "Restoring NuGet Packages for Llvm.NET"
        Invoke-MSBuild -Targets 'Restore;Build' -Project src\Llvm.NET.sln -Properties $msBuildProperties -LoggerArgs ($msbuildLoggerArgs + @("/bl:Llvm.NET.binlog") )
    }

    if($BuildDocs)
    {
        .\Build-Docs.ps1 -BuildInfo $BuildInfo
    }

    if( $env:APPVEYOR_PULL_REQUEST_NUMBER )
    {
        foreach( $item in Get-ChildItem *.binlog )
        {
            Push-AppveyorArtifact $item.FullName
        }
    }
}
finally
{
    popd
    $env:Path = $oldPath
}
