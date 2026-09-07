# scripts/Complete-Step.ps1
param(
    [Parameter(Mandatory=$true)]
    [string]$Message,

    [Parameter(Mandatory=$false)]
    [string]$JalonBranch
)

$currentBranch = git branch --show-current
$scriptDir = Split-Path -Parent $MyInvocation.MyCommand.Definition

Write-Host "========================================" -ForegroundColor Magenta
Write-Host "[*] CLOTURE DU STEP : $currentBranch" -ForegroundColor Magenta
Write-Host "========================================" -ForegroundColor Magenta

Write-Host "Lancement de la validation de la derniere tache..." -ForegroundColor Cyan
& "$scriptDir\Commit-Task.ps1" -Message $Message

if (-not [string]::IsNullOrWhiteSpace($JalonBranch)) {
    if ($JalonBranch -notlike "feature/*" -and $JalonBranch -notlike "chore/*") {
        $targetJalonBranch = "feature/$JalonBranch"
    } else {
        $targetJalonBranch = $JalonBranch
    }

    Write-Host "Lancement de la fusion automatique dans $targetJalonBranch..." -ForegroundColor Cyan
    $mergeMessage = "Merge de l'etape ($currentBranch) : $Message"
    
    & "$scriptDir\Merge-Branch.ps1" -SourceBranch $currentBranch -TargetBranch $targetJalonBranch -Message $mergeMessage
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "Suppression de la branche locale $currentBranch..." -ForegroundColor Yellow
        git branch -d $currentBranch
        
        Write-Host "Poussée de $targetJalonBranch sur le dépôt distant..." -ForegroundColor Cyan
        git push origin $targetJalonBranch

        Write-Host "Suppression de la branche distante $currentBranch..." -ForegroundColor Yellow
        git push origin --delete $currentBranch 2>$null
        
        Write-Host "========================================" -ForegroundColor Green
        Write-Host "[SUCCES] ETAPE CLOTUREE, FUSIONNEE, NETTOYEE ET SYNCHRONISEE !" -ForegroundColor Green
        Write-Host "Vous etes maintenant sur $targetJalonBranch." -ForegroundColor Green
        Write-Host "========================================" -ForegroundColor Green
    } else {
        Write-Host "========================================" -ForegroundColor Red
        Write-Host "[ERREUR] Conflits detectes ou echec du merge ! La branche $currentBranch n a pas ete supprimee." -ForegroundColor Red
        Write-Host "========================================" -ForegroundColor Red
    }
}