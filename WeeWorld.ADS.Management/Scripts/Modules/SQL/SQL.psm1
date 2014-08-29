$scriptDirectory = Split-Path -parent $MyInvocation.MyCommand.Path
Import-Module $scriptDirectory\..\Settings\Settings.psm1 -force

Function Get-ConnectionString
{
	param ( [string]$env,
			[string]$db)

	$sqlServer = Get-SqlSetting $env "server"
	$sqlLogin = Get-SqlSetting $env "login"
	$sqlPassword = Get-SqlSetting $env "password"
	
	return "server=$sqlServer;Database=$db;User ID=$sqlLogin;Password=$sqlPassword";
}

Function Invoke-Query
{
	param ( [string]$env,
			[string]$query)

	$sqlServer = Get-SqlSetting $env "server"
	$sqlLogin = Get-SqlSetting $env "login"
	$sqlPassword = Get-SqlSetting $env "password"

	return sqlcmd -S $sqlServer -U $sqlLogin -P $sqlPassword -Q $query
}

Function Get-Db
{
	param ( [string]$env)

	Invoke-Query $env "SELECT name FROM master..sysdatabases WHERE name <> 'master'"
}

Function Add-Db
{
	param (	[string]$env,
			[string]$db)
	 
	Invoke-Query $env "CREATE DATABASE [$db]"
	Write-Host "db $db created"
}

Function Remove-Db
{
	param ( [string]$env,
			[string]$db)

	Invoke-Query $env "DROP DATABASE [$db]"
	Write-Host "db $db deleted"
}

Function Copy-Db
{
	param (	[string]$env,
			[string]$sourceDb,
			[string]$targetDb)

	Invoke-Query $env "CREATE DATABASE [$targetDb] AS COPY OF [$sourceDb]"
	$state = Invoke-Query $env "SELECT state FROM sys.databases WHERE name = '$targetDb'"
	
	Write-Host -NoNewline 'Waiting For db replication to complete.'
	while($state[2].trim() -eq 7) 
	{
		Write-Host -NoNewline ' .'
		Start-Sleep -s 5;
		$state = Invoke-Query $env "SELECT state FROM sys.databases WHERE name = '$targetDb'"
	}

	Write-Host "db $targetDb created from $sourceDb"
}

Function Backup-Db
{
	param ( [string]$env,
			[string]$db)
	 
	$date = Get-Date -format yyyyMMddhhmmss
	$targetDb = "$db" + "_$date"
	Copy-Db $env $db $targetDb
}
