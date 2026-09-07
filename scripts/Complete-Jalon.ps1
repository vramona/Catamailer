# scripts/Complete-Jalon.ps1
param(
    [Parameter(Mandatory=$true)]
    [string]$JalonBranch,
    
    [string]$BaseBranch = "master"
)

if ($JalonBranch -notlike "feature/*" -and $JalonBranch -notlike "chore/*") {
    $fullJalonBranch = "feature/$JalonBranch"
    $tagName = $JalonBranch
} else {
    $fullJalonBranch = $JalonBranch
    $tagName = $JalonBranch -replace '^(feature|chore)/', ''
}

Write-Host "========================================" -ForegroundColor Magenta
Write-Host "[*] CLOTURE DU JALON : $tagName" -ForegroundColor Magenta
Write-Host "========================================" -ForegroundColor Magenta

Write-Host "[1/3] Basculement et mise a jour de $BaseBranch..." -ForegroundColor Cyan
git checkout $BaseBranch
git pull origin $BaseBranch

Write-Host "[2/3] Fusion de $fullJalonBranch dans $BaseBranch..." -ForegroundColor Cyan
git merge $fullJalonBranch --no-ff -m "Merge du Jalon : $tagName"

if ($LASTEXITCODE -ne 0) {
    Write-Host "========================================" -ForegroundColor Red
    Write-Host "[ERREUR] Conflits detectes ou erreur lors du merge ! Resolvez les conflits manuellement." -ForegroundColor Red
    Write-Host "========================================" -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host "[3/3] Creation du Tag Git '$tagName'..." -ForegroundColor Cyan
git tag $tagName

Write-Host "Poussée des modifications et des tags sur le dépôt distant..." -ForegroundColor Cyan
git push origin $BaseBranch --tags

Write-Host "========================================" -ForegroundColor Green
Write-Host "[SUCCES] JALON CLOTURE ET SYNCHRONISE AVEC SUCCES !" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green