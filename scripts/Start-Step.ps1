# scripts/Start-Step.ps1
param(
    [Parameter(Mandatory=$true)]
    [string]$StepBranch,
    
    [Parameter(Mandatory=$true)]
    [string]$JalonBranch
)

Write-Host "========================================" -ForegroundColor Magenta
Write-Host "[*] DEMARRAGE STEP : $StepBranch" -ForegroundColor Magenta
Write-Host "========================================" -ForegroundColor Magenta

if ($JalonBranch -notlike "feature/*" -and $JalonBranch -notlike "chore/*") {
    $baseJalonBranch = "feature/$JalonBranch"
} else {
    $baseJalonBranch = $JalonBranch
}

Write-Host "Basculement sur $baseJalonBranch..." -ForegroundColor Cyan
git checkout $baseJalonBranch

if ($StepBranch -notlike "feature/*" -and $StepBranch -notlike "bugfix/*" -and $StepBranch -notlike "refacto/*" -and $StepBranch -notlike "chore/*") {
    $newBranch = "feature/$StepBranch"
} else {
    $newBranch = $StepBranch
}

Write-Host "Création de la sous-branche $newBranch..." -ForegroundColor Cyan
git checkout -b $newBranch

Write-Host "Poussée de la sous-branche $newBranch sur le dépôt distant..." -ForegroundColor Cyan
git push -u origin $newBranch

Write-Host "========================================" -ForegroundColor Green
Write-Host "[SUCCES] Sous-branche prête et synchronisée : $newBranch" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green