###	Deploy.ps1:	Handles deployments of the Beano project to azure, with 
###				support for staging slots, db migrations and git integration


Param(	[Parameter(Mandatory=$True)][string]$env,
		[Parameter(Mandatory=$True)][string]$action)

$action = $action.ToLower()
$env = $env.ToLower()

# set script directory to location of deploy.ps1 file to allow execution from any directory
$scriptDirectory = Split-Path -parent $MyInvocation.MyCommand.Path

# load in the collection of modules required for script execution
foreach ($module in @("utils","sql", "nuget", "msbuild", "migrator", "git")) {
	Import-Module $scriptDirectory\Modules\$module\$module.psm1 -force -disableNameChecking
}

# set some global vars
$projectName = Get-ProjectSetting "name"
$workingDirectory = Get-WorkingDirectory "$scriptDirectory"
$solutionFile = "$workingDirectory\$projectName.sln"
$projectFile = "$workingDirectory\src\$projectName\$projectName.csproj"
$migrationsFile = "$workingDirectory\src\$projectName.Management\bin\debug\$projectName.Management.dll"
$dbName = Get-SqlSetting "$env" "db-name"
$requiresBackup = Get-SqlSetting "$env" "requires-backup"

# deploy website to staging slot
if ($action -eq 'toStaging' -or $action -eq 'ts')
{
	Write-Step "preping deployment repo"
	Clean-DeploymentRepo "$env" "$workingDirectory"

	Write-Step "compiling project with staging configuration"
	Publish-Project "staging" "$projectFile"

	Write-Step "creating staging database as a copy of production database"
	Copy-Db "$env" "$dbName" "${dbName}_Staging"

	Write-Step "running migrations against staging database"
	Migrate-Up "$env" "${dbName}_Staging" "$migrationsFile"

	Write-Step "deployinng website to staging slot"
	Git-Deploy "staging" "$workingDirectory"
}

# deploy website directly to production slot
elseif ($action -eq 'toProduction' -or $action -eq 'tp')
{
	if ($requiresBackup)
	{
		Write-Step "creating backup of production database"
		Backup-Db "$env" "$dbName"
	}

	Write-Step "preping deployment repo"
	Clean-DeploymentRepo "$env" "$workingDirectory"

	Write-Step "compiling project with $env configuration"
	Publish-Project "$env" "$projectFile"
	
	Write-Step "running migrations against production database"
	Migrate-Up "$env" "$dbName" "$migrationsFile"

	Write-Step "deployinng website to production slot"
	Git-Deploy "$env" "$workingDirectory"
}

# move a website from the staging slot into production slot
elseif ($action -eq 'fromStaging' -or $action -eq 'fs')
{
	Write-Step "removing staging database"
	Remove-Db "$env" "${dbName}_Staging"

	if ($requiresBackup)
	{
		Write-Step "creating backup of production database"
		Backup-Db "$env" "$dbName"
	}

	# (should be doing a role swap here, but just replicating 'toProduction')
	Write-Step "preping deployment repo"
	Clean-DeploymentRepo "$env" "$workingDirectory"

	Write-Step "compiling project with $env configuration"
	Publish-Project "$env" "$projectFile"
	
	Write-Step "running migrations against production database"
	Migrate-Up "$env" "$dbName" "$migrationsFile"

	Write-Step "deployinng website to production slot"
	Git-Deploy "$env" "$workingDirectory"

	Write-Step "tagging deployment branch"
	# Git-Tag "1.0.0.2202"
}