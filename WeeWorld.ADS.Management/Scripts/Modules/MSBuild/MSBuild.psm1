#$scriptDirectory = Split-Path -parent $MyInvocation.MyCommand.Path
#Import-Module $scriptDirectory\..\Settings\Settings.psm1 -force

Function Build-Solution 
{
	param ( [string]$config,
			[string]$filepath)

	msbuild $filepath /t:rebuild /p:Configuration=$config
}


Function Publish-Project
{
	param ( [string]$config,
			[string]$filepath)

	msbuild $filepath /t:rebuild /p:Configuration=$config /p:PublishProfile=$config /p:DeployOnBuild=True /p:VisualStudioVersion=12.0
}