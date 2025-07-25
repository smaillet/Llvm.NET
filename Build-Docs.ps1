using module "PSModules/CommonBuild/CommonBuild.psd1"
using module "PSModules/RepoBuild/RepoBuild.psd1"

<#
.SYNOPSIS
    Builds the docs for this repository

.PARAMETER Configuration
    This sets the build configuration to use, default is "Release" though for inner loop development this may be set to "Debug"

.PARAMETER FullInit
    Performs a full initialization. A full initialization includes forcing a re-capture of the time stamp for local builds
    as well as writes details of the initialization to the information and verbose streams.

.PARAMETER NoClone
    Skip cloning of the docs repository. Useful for inner loop development where you only do the clone once when iterating on
    doc updates.

.PARAMETER ShowDocs
    Show the docs via `docfx serve --open-browser ...`. This option is useful for inner loop development of the docs as it allows
    opening a browser on the newly created docs for testing/checking the contents are produced correctly.
#>
Param(
    [string]$Configuration="Release",
    [switch]$FullInit,
    [switch]$NoClone,
    [switch]$ShowDocs
)

$docFXToolVersion = '2.78.3'

$InformationPreference = 'Continue'
$ErrorInformationPreference = 'Stop'

Push-Location $PSScriptRoot
$oldPath = $env:Path
try
{
    $buildInfo = Initialize-BuildEnvironment -FullInit:$FullInit

    # make sure the supported tool is installed.
    Invoke-External dotnet tool install --global docfx --version $docFXToolVersion | Out-Null

    $docsOutputPath = $buildInfo['DocsOutputPath']
    Write-Information "Docs OutputPath: $docsOutputPath"

    # Clone docs output location so it is available as a destination for the Generated docs content
    # and the versioned docs links can function correctly for locally generated docs
    if(!$NoClone -and !(Test-Path (Join-Path $docsOutputPath '.git') -PathType Container))
    {
        # clean out existing docs content so that clone can work
        if(Test-Path -PathType Container $docsOutputPath)
        {
            Write-Information "Cleaning $docsOutputPath"
            Remove-Item -Path $docsOutputPath -Recurse -Force -ProgressAction SilentlyContinue
        }

        Write-Information "Cloning Docs repository"
        Invoke-External git clone $buildInfo['OfficialGitRemoteUrl'] -b gh-pages $docsOutputPath -q
    }

    # Delete everything in the docs output except the git folder so the result of applying changes
    # is ONLY what is generated by this script.
    Write-Information "Cleaning $docsOutputPath"
    Get-ChildItem -Path $docsOutputPath -Exclude '.git' | remove-item -Recurse -Force -ProgressAction SilentlyContinue

    # Create a file to disable the default GitHub Pages use of JEKYLL as this uses docfx to generate the final
    # HTML. [It is at least theoretically plausible that JEKYLL could handle some/all of the metadata files produced
    # by DOCFX but it is not clear how much of the "build" stage would be lost if ONLY the metadata phase was used.
    # Thus, for now, this uses the docfx build phase.]
    "$([DateTime]::UtcNow.ToString('o'))" | Out-File -Path (Join-Path $docsOutputPath '.nojekyll')

    push-location './docfx'
    try
    {
        Write-Information "Generating Version JSON"
        Invoke-External dotnet msbuild -restore '-target:GenerateVersionJson' documentation.msbuildproj
        if(!(Test-Path -PathType Leaf 'CurrentVersionInfo.json'))
        {
            throw "CurrentVersionInfo.json - missing/not created!"
        }

        $versionInfo = Get-Content ./CurrentVersionInfo.json | ConvertFrom-Json -AsHashTable
        $fullBuildNumber = $versionInfo['FullBuildNumber']
        Write-Information "Building docs [FullBuildNumber=$fullBuildNumber]"
        Invoke-External docfx '-m' _buildVersion=$fullBuildNumber '-o' $docsOutputPath
    }
    finally
    {
        Pop-Location
    }

    if($ShowDocs)
    {
        .\Show-Docs.ps1 $buildInfo
    }
}
catch
{
    # Everything from the official docs to the various articles in the blog-sphere says this isn't needed
    # and in fact it is redundant - They're all WRONG! By re-throwing the exception the original location
    # information is retained and the error reported will include the correct source file and line number
    # data for the error. Without this, only the error message is retained and the location information is
    # Line 1, Column 1, of the outer most script file, which is, of course, completely useless.
    throw
}
finally
{
    Pop-Location
    $env:Path = $oldPath
}
