$scriptDirectory = Split-Path -parent $MyInvocation.MyCommand.Path
Import-Module $scriptDirectory\..\Settings\Settings.psm1 -force

function Write-Step
{
	param ( [string]$msg)

	write-host "`n>>>`t$msg" -foreground "Cyan"
}

function Get-WorkingDirectory
{
	param ( [string]$startDirectory)

	$workingDirectory = "$startDirectory"
	$projectName = Get-ProjectSetting "name"

	# traverse up the file tree until we find the solution file
	# for this project, and treat that directory as the root
	do 
	{
		$segment = (Split-Path $workingDirectory -Leaf)
		$workingDirectory = ($workingDirectory -replace "$segment", "").trim("\")

	}
	while ( (Test-Path "$workingDirectory\$projectName.sln") -eq $False)

	return $workingDirectory
}