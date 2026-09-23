<#
.SYNOPSIS
Purge (supprime) le fichier de base de données SQLite catamailer.db.

.DESCRIPTION
Effectue une recherche récursive dans l'intégralité du dossier LocalAppData de l'utilisateur courant, ignorant ainsi les variations de nommage de package MAUI.
#>
param (
    [string]$DbName = "catamailer.db"
)

$searchRoot = $env:LOCALAPPDATA

Write-Host "[Purge] Recherche récursive de '$DbName' dans $searchRoot..." -ForegroundColor Cyan

$fileDeleted = $false

$dbFiles = Get-ChildItem -Path $searchRoot -Filter $DbName -Recurse -ErrorAction SilentlyContinue

foreach ($file in $dbFiles) {
    Write-Host "[Purge] Fichier trouvé : $($file.FullName)" -ForegroundColor Yellow
    
    try {
        Remove-Item -Path $file.FullName -Force -ErrorAction Stop
        Write-Host "[Purge] Fichier supprimé avec succès." -ForegroundColor Green
        $fileDeleted = $true
    }
    catch {
        Write-Error "[Purge] Impossible de supprimer le fichier. Vérifie que Catamailer.UI est bien fermé. Détail : $_"
    }
}

if (-not $fileDeleted) {
    Write-Warning "[Purge] Aucun fichier '$DbName' n'a été trouvé dans $searchRoot."
}