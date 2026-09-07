# scripts/Commit-Task.ps1
param(
    [Parameter(Mandatory=$true)]
    [string]$Message
)

Write-Host "========================================" -ForegroundColor Magenta
Write-Host "[*] VALIDATION TACHE : $Message" -ForegroundColor Magenta
Write-Host "========================================" -ForegroundColor Magenta

Write-Host "Ajout des fichiers au perimetre Git..." -ForegroundColor Cyan
git add --all

Write-Host "Creation du commit de la tache..." -ForegroundColor Cyan
git commit -m $Message
$commitHash = git rev-parse HEAD

Write-Host "Hash du code genere : $commitHash" -ForegroundColor Yellow

Write-Host "Injection du hash dans la documentation..." -ForegroundColor Cyan
$files = @("Specs_Catamailer.md", "Specs_Catamailer_Unit_Tests.md", "ToDoList.md")

foreach ($file in $files) {
    if (Test-Path $file) {
        (Get-Content $file) -replace '<COMMIT[-_]HASH>', $commitHash | Set-Content $file -Encoding UTF8
        Write-Host " -> $file mis a jour." -ForegroundColor Green
    } else {
        Write-Host "ATTENTION : Fichier $file introuvable à la racine." -ForegroundColor Red
    }
}

Write-Host "Creation du commit de tracabilite..." -ForegroundColor Cyan
git add *.md
git commit -m "docs: Tracabilite de la tache (Code Hash: $commitHash)"

Write-Host "Poussée des commits sur le dépôt distant..." -ForegroundColor Cyan
git push origin HEAD

Write-Host "========================================" -ForegroundColor Green
Write-Host "[SUCCES] La tache est figee, documentee et synchronisee." -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green