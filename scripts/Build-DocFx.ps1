# scripts/Build-DocFx.ps1
Write-Host "Vérification de l'outil global DocFX..." -ForegroundColor Cyan

if (!(Get-Command docfx -ErrorAction SilentlyContinue)) {
    Write-Host "Installation de DocFX (.NET Global Tool)..." -ForegroundColor Yellow
    dotnet tool install -g docfx
}

$baseDir = [System.IO.Path]::GetFullPath((Join-Path -Path $PSScriptRoot -ChildPath ".."))
$docDir = Join-Path -Path $baseDir -ChildPath "docs"
$wwwrootPath = Join-Path -Path $docDir -ChildPath "wwwroot"
$logsDir = Join-Path -Path $baseDir -ChildPath "logs"

if (-not (Test-Path $logsDir)) { New-Item -ItemType Directory -Path $logsDir | Out-Null }

$contextObj = @{ DocDir = $docDir }
$contextObj | ConvertTo-Json | Set-Content (Join-Path -Path $PSScriptRoot -ChildPath ".docfx-context.json")

Write-Host "Purge du répertoire généré (wwwroot)..." -ForegroundColor Yellow
if (Test-Path $wwwrootPath) { Remove-Item -Path $wwwrootPath -Recurse -Force }

Write-Host "Nettoyage des anciens fichiers API générés..." -ForegroundColor Yellow
$apiSubDirs = @("Domain", "Application", "Infrastructure", "UI", "Migrator")
foreach ($subDir in $apiSubDirs) {
    $targetPath = Join-Path -Path $docDir -ChildPath "api\$subDir"
    if (Test-Path $targetPath) { Remove-Item -Path $targetPath -Recurse -Force }
}

Write-Host "Exécution des tests unitaires (Génération du rapport TRX)..." -ForegroundColor Magenta
$testDir = Join-Path -Path $baseDir -ChildPath "tests\Catamailer.Domain.Tests"
if (Test-Path $testDir) {
    Push-Location $testDir
    dotnet test --logger "trx;LogFileName=TestResults.trx" --results-directory "..\..\logs" --nologo --verbosity quiet
    Pop-Location
} else {
    Write-Host "ATTENTION : Dossier de tests introuvable ($testDir)" -ForegroundColor Red
}

$trxPath = Join-Path -Path $logsDir -ChildPath "TestResults.trx"
if (!(Test-Path $trxPath)) {
    Write-Host "ATTENTION : Le fichier TestResults.trx n'a pas été généré par dotnet test !" -ForegroundColor Red
}

Write-Host "Génération des métadonnées et du site statique DocFX..." -ForegroundColor Green
if (Test-Path (Join-Path -Path $docDir -ChildPath "docfx.json")) {
    Start-Process -FilePath "docfx" -ArgumentList "docfx.json" -WorkingDirectory "$docDir" -NoNewWindow -Wait
} else {
    Write-Host "ATTENTION : docfx.json introuvable dans le dossier $docDir" -ForegroundColor Red
}