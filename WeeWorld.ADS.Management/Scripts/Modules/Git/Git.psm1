$scriptDirectory = Split-Path -parent $MyInvocation.MyCommand.Path
Import-Module $scriptDirectory\..\Settings\Settings.psm1 -force

Function Git-ResetWorkSpace
{
	param (	[string]$branch, 
			[string]$workingDirectory)

	cd $workingDirectory

	# branch names map directly to environments, except live, which maps to the master branch
	if ($branch -eq "live")
	{
		$branch = "master"
	}

	git reset --hard
	git checkout $branch
	git pull origin $branch
}

Function Clean-DeploymentRepo
{
	param (	[string]$env,
			[string]$workingDirectory)

	Remove-Item "$workingDirectory\publish\$env\*" -recurse -force
	Write-Host "deployment repo reset and up-to-date"
}

Function Git-Deploy
{
	param (	[string]$env,
			[string]$workingDirectory)

	$deploymentUrl = Get-AzureSetting "$env" "deployment-url"
	$deploymentUsername = Get-AzureSetting "$env" "deployment-username"
	$deploymentPassword = Get-AzureSetting "$env" "deployment-password"
	
	cd "$workingDirectory\publish\$env"
	git init 
	git remote add origin "https://${deploymentUsername}:${deploymentPassword}@${deploymentUrl}"
	git add -A
	git commit -m "<commit message>" 
	git push origin master -f
	Write-Host "website deployed to $env"

	cd "$workingDirectory"
}

Function Git-Commit
{
	param (	[string]$branch,
			[string]$workingDirectory,
			[string]$commitMessage)

	$repoUrl = Get-ProjectSetting "repo-url"
	$repoUsername = Get-ProjectSetting "repo-username"
	$repoPassword = Get-ProjectSetting "repo-password"
	
	if ($env -ne "dev")
	{
		"commit denied on this branch"
		break
	}

	cd "$workingDirectory"
	git init 
	git remote add origin "https://${repoUsername}:${repoPassword}@${repoUrl}"
	git add -A
	git commit -m "$commitMessage" 
	git push origin $branch -f
	Write-Host "website deployed to $branch"
}
