# Build.ps1: Handles building of the Beano project for 
# code analysis purposes (compilation, unit testing, etc)

# set script directory to location of deploy.ps1 file to allow execution from any directory
$scriptDirectory = Split-Path -parent $MyInvocation.MyCommand.Path

# load in the collection of modules required for script execution
foreach ($module in @("utils","sql", "nuget", "msbuild", "nunit")) {
	Import-Module $scriptDirectory\Modules\$module\$module.psm1 -force -DisableNameChecking
}

# set some global vars
$projectName = Get-ProjectSetting "name"
$workingDirectory = Get-WorkingDirectory "$scriptDirectory"
$solutionFile = "$workingDirectory\$projectName.sln"
$testsFile = "$workingDirectory\src\$projectName.Tests\bin\debug\$projectName.Tests.dll"


Write-Step "installing nuget packages"
Install-NuGetPackages "$solutionFile"

Write-Step "building solution for code analysis"
Build-Solution "debug" "$solutionFile" 

Write-Step "running unit tests"
Run-UnitTests "$testsFile"
