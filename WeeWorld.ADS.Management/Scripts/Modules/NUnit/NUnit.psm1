$scriptDirectory = Split-Path -parent $MyInvocation.MyCommand.Path
Import-Module $scriptDirectory\..\Settings\Settings.psm1 -force

Function Run-UnitTests
{
	param ( [string]$filepath)

	nunit-console $filepath
}
