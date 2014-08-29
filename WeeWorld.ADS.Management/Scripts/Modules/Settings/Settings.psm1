$settings = @{};

# General Project Settings
$settings["project.name"] = "DCT.Beano"
$settings["project.repo-url"] = "https://ag-weeworld.visualstudio.com/DefaultCollection/_git/DCT.Beano"
$settings["project.repo-username"] = "weedev"
$settings["project.repo-password"] = "W33w0rld!"

# SQL Server Settings
$settings["sql.live.server"] = "psdp7wyav6.database.windows.net"
$settings["sql.live.login"] = "dctadmin@psdp7wyav6"
$settings["sql.live.password"] = "Att70qWt54"
$settings["sql.live.db-name"] = "Beano"
$settings["sql.live.requires-backup"] = $True

$settings["sql.test.server"] = "n9nv4obeuc.database.windows.net"
$settings["sql.test.login"] = "dctadmin@n9nv4obeuc"
$settings["sql.test.password"] = "bc30sAPTO1"
$settings["sql.test.db-name"] = "Beano"
$settings["sql.test.requires-backup"] = $False

$settings["sql.dev.server"] = "cvsq619ez7.database.windows.net"
$settings["sql.dev.login"] = "dctadmin@cvsq619ez7"
$settings["sql.dev.password"] = "g93bV1P67y"
$settings["sql.dev.db-name"] = "Beano"
$settings["sql.dev.requires-backup"] = $False

# Azure Settings
$settings["azure.live.deployment-url"] = "beano.scm.azurewebsites.net:443/beano.git"
$settings["azure.live.deployment-username"] = "weeworldpublisher"
$settings["azure.live.deployment-password"] = "wCdxTk3C"

$settings["azure.staging.deployment-url"] = "beano-staging.scm.azurewebsites.net:443/beano-staging.git"
$settings["azure.staging.deployment-username"] = "weeworldpublisher"
$settings["azure.staging.deployment-password"] = "wCdxTk3C"

$settings["azure.test.deployment-url"] = "beano-test.scm.azurewebsites.net:443/beano-test.git"
$settings["azure.test.deployment-username"] = "weeworldpublisher"
$settings["azure.test.deployment-password"] = "wCdxTk3C"

$settings["azure.dev.deployment-url"] = "beano-dev.scm.azurewebsites.net:443/beano-dev.git"
$settings["azure.dev.deployment-username"] = "weeworldpublisher"
$settings["azure.dev.deployment-password"] = "wCdxTk3C"

function Get-SqlSetting
{
	param ( [string]$env,
			[string]$setting)

	return $settings["sql.$env.$setting"]
}

function Get-AzureSetting
{
	param ( [string]$env,
			[string]$setting)

	return $settings["azure.$env.$setting"]
}

function Get-ProjectSetting
{
	param ( [string]$setting)

	return $settings["project.$setting"]
}