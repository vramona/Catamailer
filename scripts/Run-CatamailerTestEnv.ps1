<#
.SYNOPSIS
Lance l'IHM Catamailer en encapsulant l'exécution avec la préparation et le nettoyage de l'environnement Outlook via les scripts partagés.
#>
$ErrorActionPreference = "Stop"

Write-Host "=== DÉMARRAGE DE L'ENVIRONNEMENT DE TEST ===" -ForegroundColor Yellow
& "$PSScriptRoot\Invoke-TestEnvSetup.ps1"

$exitCode = 0
try {
    Write-Host "`n=== COMPILATION DE LA SOLUTION ===" -ForegroundColor Yellow
    $solutionPath = "$PSScriptRoot\..\Catamailer.slnx"
    if (Test-Path $solutionPath) {
        dotnet build $solutionPath
    } else {
        dotnet build "$PSScriptRoot\..\src\Catamailer.UI\Catamailer.UI.csproj"
    }

    if ($LASTEXITCODE -ne 0) {
        $exitCode = $LASTEXITCODE
        throw "La compilation a échoué. Arrêt du processus."
    }

    Write-Host "`n=== LANCEMENT DE CATAMAILER ===" -ForegroundColor Yellow
    $catamailerPath = "$PSScriptRoot\..\src\Catamailer.UI\bin\Debug\net10.0-windows10.0.19041.0\win-x64\Catamailer.UI.exe"

    if (Test-Path $catamailerPath) {
        Write-Host "Application Catamailer en cours d'exécution. (Fermez l'application pour continuer...)" -ForegroundColor Cyan
        $process = Start-Process -FilePath $catamailerPath -PassThru
        $process.WaitForExit()
    } else {
        throw "Exécutable introuvable : $catamailerPath."
    }
}
catch {
    Write-Error "Erreur : $_"
    if ($exitCode -eq 0) { $exitCode = 1 }
}
finally {
    Write-Host "`n=== NETTOYAGE DE L'ENVIRONNEMENT ===" -ForegroundColor Yellow
    & "$PSScriptRoot\Invoke-TestEnvTeardown.ps1"
}

if ($exitCode -eq 0) {
    Write-Host "`nTerminé avec succès." -ForegroundColor Green
}
exit $exitCode