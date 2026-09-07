# scripts/Start-Jalon.ps1
param(
    [Parameter(Mandatory=$true)]
    [string]$JalonBranch,
    
    [string]$BaseBranch = "master"
)

Write-Host "========================================" -ForegroundColor Magenta
Write-Host "[*] DEMARRAGE JALON : $JalonBranch" -ForegroundColor Magenta
Write-Host "========================================" -ForegroundColor Magenta

Write-Host "Basculement sur $BaseBranch..." -ForegroundColor Cyan
git checkout $BaseBranch
git pull origin $BaseBranch

if ($JalonBranch -notlike "feature/*" -and $JalonBranch -notlike "chore/*") {
    $newBranch = "feature/$JalonBranch"
} else {
    $newBranch = $JalonBranch
}

Write-Host "Création de la branche $newBranch..." -ForegroundColor Cyan
git checkout -b $newBranch

Write-Host "Poussée de la branche $newBranch sur le dépôt distant..." -ForegroundColor Cyan
git push -u origin $newBranch

Write-Host "========================================" -ForegroundColor Green
Write-Host "[SUCCES] Branche de Jalon prête et synchronisée : $newBranch" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green