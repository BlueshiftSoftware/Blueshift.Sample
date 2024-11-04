<#
.SYNOPSIS
Compiles, tests, packs, and publishes the Blueshift.MSBuild solution.

.DESCRIPTION
Executes the steps to compile, test, and pack the solution. With the "Publish" stage, this
script can also be used to publish build artifacts to the configured package manager.
 
.PARAMETER Stages
A list of build stages to execute. Valid stages are:

- Restore
    Restores the dependency tree for the solution.
- Compile
    Compiles the solution.
- Test
    Runs tests found within the solution. Test results are placed in the .\artifacts\testResults directory.
- Package
    Ensures that all packaged build artifacts are created and placed in the .\artifacts\packages directory.
- Publish
    Pushes package artifacts to their respective package managers.

The default stage list is "Restore, Compile, Test, Package". By default, the "Publish" stage is not included in order
to reduce load on the package management system.

.PARAMETER NugetPackageSourceUsername
The username to use to authentication against the NuGet package source.

.PARAMETER NugetPackageSourcePassword
The password to use to authentication against the NuGet package source.

.PARAMETER NugetPublishSource
The NuGet package source URI to use when publishing nupkg artifacts.

.PARAMETER NugetApiKey
The API key to use when publishing nupkg files.

.PARAMETER Configuration
Optional. The cofiguration to select when building the solution. Defaults to "Debug" on developer
machines, but to "Release" on systems with the "CI" environment variable set to "true".

.PARAMETER EnableCodeCoverage
Optional. Enables code coverage. Defaults to true on systems with the "CI" environment variable set to "true"
#>
[CmdletBinding()]
Param (
    [ValidateSet("Restore", "Clean", "Compile", "Test", "Package", "Publish")]
    [Parameter(Position=0)]
    [string[]] $Stages = @("Restore", "Clean", "Compile", "Test", "Package"),

    [string] $NugetPublishSource = $env:NUGET_PUBLISH_SOURCE,

    [string] $NugetApiKey = $env:NUGET_API_KEY,

    [ValidateSet("Debug", "Release")]
    [string] $Configuration = @{$true = "Release"; $false = "Debug"}[([System.Convert]::ToBoolean($env:CI))],

    [switch] $EnableCodeCoverage = ([System.Convert]::ToBoolean($env:CI))
)

Function Invoke-Dotnet {
    param(
        [Parameter(Mandatory=$true, Position = 0, ValueFromRemainingArguments)]
        [string[]] $Arguments
    )

    dotnet $Arguments

    if ($LASTEXITCODE -ne 0) {
        throw "'dotnet' exited with code '${LASTEXITCODE}'."
    }
}

Push-Location $PSScriptRoot

try {
    $separator = $([System.IO.Path]::DirectorySeparatorChar)

    $artifactsDir = (Join-Path (PWD) "artifacts")
    $srcDir = (Join-Path (PWD) "src${separator}")
    $testDir = (Join-Path (PWD) "test${separator}")
    $packagesDir = (Join-Path $artifactsDir "packages${separator}")
    $testResultsDir = (Join-Path $artifactsDir "testResults${separator}")
    $coverageFile = (Join-Path $testResultsDir "result.json")

    if ($Stages -contains "Restore") {
        Invoke-Dotnet restore --force
    }

    if ($Stages -contains "Clean") {
        Invoke-Dotnet clean --configuration $Configuration
    }

    if ($Stages -contains "Compile") {
        Invoke-Dotnet build --configuration $Configuration --no-restore --force
    }

    if ($Stages -contains "Test") {
        Remove-Item $testResultsDir -ErrorAction SilentlyContinue -Recurse -Force

        $excludeFromCoverageByAttribute = @(
            "Obsolete"
            "GeneratedCodeAttribute"
            "CompilerGeneratedAttribute"
        ) -join '%2c'

        $coverageThresholdTypes = @(
            "line",
            "branch",
            "method"
        ) -join '%2c'

        $testParams = @(
            "test"
            "--configuration", $Configuration
            "--results-directory", $testResultsDir
            "--logger", "trx"
            "--verbosity", "normal"
            "--no-build"
            "--no-restore"
            "--collect:XPlat Code Coverage"
            "/p:MergeWith=$coverageFile"
            "/p:ExcludeByAttribute=${excludeFromCoverageByAttribute}"
        )

        if ($EnableCodeCoverage) {
            $testParams += "/p:Threshold=80"
            $testParams += "/p:ThresholdType=${coverageThresholdTypes}"
            $testParams += "/p:ThresholdStat=minimum"
        }

        Invoke-Dotnet @testParams
    }

    # Api-Doc: https://github.com/Redocly/redoc
    # Client-Gen: https://github.com/swagger-api/swagger-codegen#swagger-codegen-cli-docker-image

    if ($Stages -contains "Package") {
        Remove-Item $packagesDir -ErrorAction SilentlyContinue -Recurse -Force

        Invoke-Dotnet pack --output $packagesDir --no-build --no-restore
    }

    if ($Stages -contains "Publish") {
        Invoke-Dotnet nuget push $packagesDir --source $NugetPublishSource --api-key $NugetApiKey
    }
}
catch {
    Write-Error "Encountered an unexpected error during build: ${_}"

    Exit $LASTEXITCODE
}
finally {
    Pop-Location
}
