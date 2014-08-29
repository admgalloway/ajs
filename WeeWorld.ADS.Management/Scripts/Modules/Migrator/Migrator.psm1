$scriptDirectory = Split-Path -parent $MyInvocation.MyCommand.Path
Import-Module $scriptDirectory\..\Settings\Settings.psm1 -force
Import-Module $scriptDirectory\..\SQL\SQL.psm1 -force

Function Migrate-Up
{
	param (	[string]$env, 
			[string]$db,
			[string]$filepath)

	$connString = Get-ConnectionString "$env" "$db"

	"connstring: $connString"

	migrate -c "$connString" -db "sqlserver2008" -a "$filepath" -t "migrate:up"
	Write-Host "migrations applied"
}

Function Migrate-Down
{
	param (	[string]$env, 
			[string]$db,
			[string]$filepath)
	
	$connString = Get-ConnectionString "$env" "$db"

	migrate -c "$connString" -db "sqlserver2008" -a "$filepath" -t "rollback"
	Write-Host "migrations rolled back"
}