<#
.SYNOPSIS
Prépare l'environnement de test : nettoyage complet du profil précédent, recréation via PIM, et injection du PST de base (v1.2).
#>
param (
    [string]$TestProfile = "Catamailer",
    [string]$BasePstPath = "$PSScriptRoot\..\tests\Data\base_test.pst"
)

$TargetPstPath = "$env:USERPROFILE\Documents\Fichiers Outlook\Fichier de données Outlook - $TestProfile.pst"
$RegistryPath = "HKCU:\Software\Microsoft\Office\16.0\Outlook\Profiles\$TestProfile"

Write-Host "[Setup v1.2] 1/4 Fermeture de l'instance Outlook..." -ForegroundColor Cyan
Stop-Process -Name "outlook" -Force -ErrorAction SilentlyContinue
Start-Sleep -Seconds 2

Write-Host "[Setup v1.2] 2/4 Nettoyage du profil existant (Registre et PST)..." -ForegroundColor Cyan
if (Test-Path $RegistryPath) { Remove-Item -Path $RegistryPath -Recurse -Force }
if (Test-Path $TargetPstPath) { Remove-Item -Path $TargetPstPath -Force }

Write-Host "[Setup v1.2] 3/4 Création du nouveau profil PIM..." -ForegroundColor Cyan
Start-Process "outlook.exe" -ArgumentList "/pim `"$TestProfile`""
Start-Sleep -Seconds 10 # Attente nécessaire pour qu'Outlook génère les clés de registre et le PST vide

if (Test-Path $BasePstPath) {
    Write-Host "[Setup v1.2] 4/4 Injection du PST de base..." -ForegroundColor Cyan
    Stop-Process -Name "outlook" -Force -ErrorAction SilentlyContinue
    Start-Sleep -Seconds 2
    
    Copy-Item -Path $BasePstPath -Destination $TargetPstPath -Force
    Write-Host "[Setup v1.2] Fichier PST restauré avec succès dans $TargetPstPath. Réouverture d'Outlook..." -ForegroundColor Green
    
    Start-Process "outlook.exe" -ArgumentList "/profile `"$TestProfile`""
    Start-Sleep -Seconds 5
} else {
    Write-Warning "[Setup v1.2] Fichier de base introuvable ($BasePstPath). Le profil utilisera un PST vierge."
}