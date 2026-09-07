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
$files = @("specs/Specs_Catamailer.md", "specs/Specs_Catamailer_Unit_Tests.md", "specs/ToDoList.md")

# Utilisation stricte de UTF-8 sans BOM pour éviter la corruption
$utf8NoBom = New-Object System.Text.UTF8Encoding $false

foreach ($file in $files) {
    if (Test-Path $file) {
        $path = (Resolve-Path $file).Path
        $content = [System.IO.File]::ReadAllText($path, $utf8NoBom)
        
        if ($content -match '<COMMIT[-_]HASH>') {
            $newContent = $content -replace '<COMMIT[-_]HASH>', $commitHash
            [System.IO.File]::WriteAllText($path, $newContent, $utf8NoBom)
            Write-Host " -> $file mis a jour." -ForegroundColor Green
        } else {
            Write-Host " -> $file ignore (aucune balise trouvee)." -ForegroundColor DarkGray
        }
    } else {
        Write-Host "ATTENTION : Fichier $file introuvable." -ForegroundColor Red
    }
}

Write-Host "Creation du commit de tracabilite..." -ForegroundColor Cyan
git add specs/*.md
git commit -m "docs: Tracabilite de la tache (Code Hash: $commitHash)"

Write-Host "Poussée des commits sur le dépôt distant..." -ForegroundColor Cyan
git push origin HEAD

Write-Host "========================================" -ForegroundColor Green
Write-Host "[SUCCES] La tache est figee, documentee et synchronisee." -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green