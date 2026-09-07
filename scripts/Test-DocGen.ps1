# scripts/Test-DocGen.ps1
param()

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
$rootDir = Resolve-Path "$scriptDir\.."
$testProject = "$rootDir\tests\Tools.AiDocGenerator.Tests\Tools.AiDocGenerator.Tests.csproj"

Write-Host "========================================" -ForegroundColor Magenta
Write-Host "[*] TESTS UNITAIRES : AiDocGenerator" -ForegroundColor Magenta
Write-Host "========================================" -ForegroundColor Magenta

if (-Not (Test-Path $testProject)) {
    Write-Host "[ATTENTION] Le projet de tests AiDocGenerator est introuvable à l'emplacement : $testProject" -ForegroundColor Yellow
    Write-Host "Si l'outil n'a pas encore été créé pour Catamailer, vous pouvez ignorer ce message." -ForegroundColor Yellow
    exit 0
}

Write-Host "Lancement de dotnet test sur Tools.AiDocGenerator.Tests..." -ForegroundColor Cyan
dotnet test $testProject

if ($LASTEXITCODE -eq 0) {
    Write-Host "========================================" -ForegroundColor Green
    Write-Host "[SUCCES] Tous les tests DocGen sont au vert !" -ForegroundColor Green
    Write-Host "========================================" -ForegroundColor Green
} else {
    Write-Host "========================================" -ForegroundColor Red
    Write-Host "[ECHEC] Un ou plusieurs tests DocGen ont échoué." -ForegroundColor Red
    Write-Host "========================================" -ForegroundColor Red
    exit $LASTEXITCODE
}