# Set-AssemblyVersion.ps1: Update the version number

Param (	[string]$Version)

$scriptDirectory = Split-Path -parent $MyInvocation.MyCommand.Path
$workingDirectory = Get-WorkingDirectory "$scriptDirectory"

$NewVersion = 'AssemblyFileVersion("' + $Version + '")';

foreach ($file in "AssemblyInfo.cs", "AssemblyInfo.vb" ) 
{
	get-childitem -recurse |? {$_.Name -eq $file} | Update-SourceVersion $version ;
}

function Update-VersionNumber
{
  foreach ($o in $input) 
  {
    $TmpFile = $o.FullName + ".tmp"

     get-content $o.FullName | 
        %{$_ -replace 'AssemblyVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $NewVersion } |
        %{$_ -replace 'AssemblyFileVersion\("[0-9]+(\.([0-9]+|\*)){1,3}"\)', $NewVersion }  > $TmpFile

     move-item $TmpFile $o.FullName -force
  }
}


