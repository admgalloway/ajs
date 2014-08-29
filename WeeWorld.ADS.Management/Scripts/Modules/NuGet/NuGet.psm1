$scriptDirectory = Split-Path -parent $MyInvocation.MyCommand.Path
Import-Module $scriptDirectory\..\Settings\Settings.psm1 -force

Function Install-NuGetPackages
{
	param ( [string]$filepath)

	nuget restore $filepath -NoCache
}
