<#
.SYNOPSIS
Orchestre l'exécution des tests d'intégration en sécurisant la restauration du profil Outlook par défaut de l'utilisateur.

.DESCRIPTION
1. Détecte le profil Outlook par défaut de l'utilisateur courant via le Registre Windows.
2. Déploie le profil Outlook de test (PIM) via Invoke-TestEnvSetup.ps1.
3. Exécute "dotnet test" uniquement sur le projet d'intégration en forçant son activation MSBuild.
4. Restaure le profil Outlook par défaut détecté via Invoke-TestEnvTeardown.ps1.
#>
param (
    [string]$IntegrationProject = "$PSScriptRoot\..\tests\Catamailer.Infrastructure.IntegrationTests\Catamailer.Infrastructure.IntegrationTests.csproj"
)

$ErrorActionPreference = "Stop"
$DefaultProfile = "Outlook" # Valeur de secours

# 1. Lecture dynamique du profil par défaut de l'utilisateur
$RegPathSetup = "HKCU:\Software\Microsoft\Office\16.0\Outlook\Setup"
if (Test-Path $RegPathSetup) {
    $profileValue = Get-ItemProperty -Path $RegPathSetup -Name "DefaultProfile" -ErrorAction SilentlyContinue
    if ($null -ne $profileValue.DefaultProfile -and -not [string]::IsNullOrWhiteSpace($profileValue.DefaultProfile)) {
        $DefaultProfile = $profileValue.DefaultProfile
    }
}

Write-Host "[Integration] Profil de restauration détecté : $DefaultProfile" -ForegroundColor Cyan

try {
    Write-Host "[Integration] Démarrage de la bascule vers l'environnement de Test..." -ForegroundColor Cyan
    & "$PSScriptRoot\Invoke-TestEnvSetup.ps1"

    Write-Host "[Integration] Exécution des tests d'intégration..." -ForegroundColor Yellow
    
    # Exécution des tests avec le flag forçant la propriété IsTestProject=true
    # Utilisation d'une verbosité minimale pour conserver l'affichage silencieux sur le succès.
    dotnet test $IntegrationProject --logger "console;verbosity=quiet" -p:ForceIntegrationTests=true 

}
catch {
    Write-Error "[Integration] Une erreur est survenue pendant l'exécution : $_"
}
finally {
    Write-Host "[Integration] Restauration de l'environnement par défaut ($DefaultProfile)..." -ForegroundColor Cyan
    & "$PSScriptRoot\Invoke-TestEnvTeardown.ps1" -DefaultProfile $DefaultProfile
    
    Write-Host "[Integration] Fin de l'orchestration." -ForegroundColor Green
}