<#
.SYNOPSIS
Exécute la suite de tests d'intégration en garantissant un environnement Outlook sain, isolé et nettoyé en fin de cycle.
#>
$ErrorActionPreference = "Stop"

Write-Host "=== DÉMARRAGE DE L'ENVIRONNEMENT DE TEST ===" -ForegroundColor Yellow
& "$PSScriptRoot\Invoke-TestEnvSetup.ps1"

Write-Host "`n=== EXÉCUTION DES TESTS D'INTÉGRATION ===" -ForegroundColor Yellow
$testExitCode = 0
try {
    dotnet test "$PSScriptRoot\..\tests\Catamailer.Infrastructure.IntegrationTests\Catamailer.Infrastructure.IntegrationTests.csproj"
    $testExitCode = $LASTEXITCODE
}
catch {
    Write-Error "Une erreur critique est survenue lors de l'exécution des tests : $_"
    $testExitCode = 1
}
finally {
    Write-Host "`n=== NETTOYAGE DE L'ENVIRONNEMENT ===" -ForegroundColor Yellow
    & "$PSScriptRoot\Invoke-TestEnvTeardown.ps1"
}

if ($testExitCode -ne 0) {
    Write-Error "Des tests d'intégration ont échoué (Code: $testExitCode)."
} else {
    Write-Host "Tous les tests d'intégration sont passés avec succès." -ForegroundColor Green
}
exit $testExitCode