<#
.SYNOPSIS
Nettoie l'environnement de test (suppression du profil et du PST) et relance Outlook sur le profil par défaut (v1.2).
#>
param (
    [string]$TestProfile = "Catamailer",
    [string]$DefaultProfile = "Outlook"
)

$TargetPstPath = "$env:USERPROFILE\Documents\Fichiers Outlook\Fichier de données Outlook - $TestProfile.pst"
$RegistryPath = "HKCU:\Software\Microsoft\Office\16.0\Outlook\Profiles\$TestProfile"

Write-Host "[Teardown v1.2] 1/3 Fermeture d'Outlook (Fin des tests)..." -ForegroundColor Cyan
Stop-Process -Name "outlook" -Force -ErrorAction SilentlyContinue
Start-Sleep -Seconds 2

Write-Host "[Teardown v1.2] 2/3 Suppression du profil de test..." -ForegroundColor Cyan
if (Test-Path $RegistryPath) { Remove-Item -Path $RegistryPath -Recurse -Force }
if (Test-Path $TargetPstPath) { Remove-Item -Path $TargetPstPath -Force }

Write-Host "[Teardown v1.2] 3/3 Réouverture d'Outlook avec le profil par défaut : $DefaultProfile" -ForegroundColor Cyan
Start-Process "outlook.exe" -ArgumentList "/profile `"$DefaultProfile`""