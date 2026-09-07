# scripts/Serve-Site.ps1
param()

$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition
$contextFile = Join-Path -Path $scriptDir -ChildPath ".docfx-context.json"

Write-Host "========================================" -ForegroundColor Magenta
Write-Host "[*] LANCEMENT DU SERVEUR DOCFX" -ForegroundColor Magenta
Write-Host "========================================" -ForegroundColor Magenta

if (Test-Path $contextFile) {
    $context = Get-Content $contextFile | ConvertFrom-Json
    $docDir = $context.DocDir
} else {
    $baseDir = [System.IO.Path]::GetFullPath((Join-Path -Path $scriptDir -ChildPath ".."))
    $docDir = Join-Path -Path $baseDir -ChildPath "doc"
}

$wwwrootPath = Join-Path -Path $docDir -ChildPath "wwwroot"

if (!(Test-Path $wwwrootPath)) {
    Write-Host "[ERREUR] Le site n'a pas encore ete genere dans : $wwwrootPath" -ForegroundColor Red
    Write-Host "Veuillez executer Build-DocFx.ps1 au moins une fois." -ForegroundColor Yellow
    exit 1
}

Write-Host "Demarrage du serveur web local sur http://localhost:8080 ..." -ForegroundColor Cyan
Start-Process -FilePath "docfx" -ArgumentList "serve `"$wwwrootPath`"" -NoNewWindow -Wait