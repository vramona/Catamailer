<#
.SYNOPSIS
Prépare l'environnement de test : nettoyage complet du profil précédent, recréation via PIM, et injection du PST de base (v1.3.1).
#>
param (
    [string]$TestProfile = "Catamailer",
    [string]$BasePstPath = "$PSScriptRoot\..\tests\Data\base_test.pst"
)

function Stop-OutlookGracefully {
    Write-Host "  -> Demande de fermeture COM..." -ForegroundColor Gray
    try {
        $app = New-Object -ComObject Outlook.Application -ErrorAction Stop;
        $app.Quit();
        [System.Runtime.InteropServices.Marshal]::ReleaseComObject($app) | Out-Null;
        [System.GC]::Collect();
        [System.GC]::WaitForPendingFinalizers();
        
        $timeout = 10;
        while ((Get-Process -Name "outlook" -ErrorAction SilentlyContinue) -and $timeout -gt 0) {
            Start-Sleep -Seconds 1;
            $timeout--;
        }
    } catch { 
        # Ignorer si COM n'est pas accessible
    }

    if (Get-Process -Name "outlook" -ErrorAction SilentlyContinue) {
        Write-Warning "  -> Fermeture forcée (Timeout ou échec COM).";
        Stop-Process -Name "outlook" -Force -ErrorAction SilentlyContinue;
        Start-Sleep -Seconds 2;
    }
}

$TargetPstPath = "$env:USERPROFILE\Documents\Fichiers Outlook\Fichier de données Outlook - $TestProfile.pst"
$RegistryPath = "HKCU:\Software\Microsoft\Office\16.0\Outlook\Profiles\$TestProfile"

Write-Host "[Setup v1.3.1] 1/4 Fermeture de l'instance Outlook..." -ForegroundColor Cyan
Stop-OutlookGracefully

Write-Host "[Setup v1.3.1] 2/4 Nettoyage du profil existant (Registre et PST)..." -ForegroundColor Cyan
if (Test-Path $RegistryPath) { Remove-Item -Path $RegistryPath -Recurse -Force }
if (Test-Path $TargetPstPath) { Remove-Item -Path $TargetPstPath -Force }

Write-Host "[Setup v1.3.1] 3/4 Création du nouveau profil PIM..." -ForegroundColor Cyan
Start-Process "outlook.exe" -ArgumentList "/pim `"$TestProfile`""
Start-Sleep -Seconds 10 # Attente nécessaire pour qu'Outlook génère les clés de registre et le PST vide

if (Test-Path $BasePstPath) {
    Write-Host "[Setup v1.3.1] 4/4 Injection du PST de base..." -ForegroundColor Cyan
    Stop-OutlookGracefully
    
    Copy-Item -Path $BasePstPath -Destination $TargetPstPath -Force
    Write-Host "[Setup v1.3.1] Fichier PST restauré avec succès dans $TargetPstPath. Réouverture d'Outlook..." -ForegroundColor Green
    
    Start-Process "outlook.exe" -ArgumentList "/profile `"$TestProfile`""
    Start-Sleep -Seconds 5
} else {
    Write-Warning "[Setup v1.3.1] Fichier de base introuvable ($BasePstPath). Le profil utilisera un PST vierge."
}