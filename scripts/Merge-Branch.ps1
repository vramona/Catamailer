# scripts/Merge-Branch.ps1
param(
    [Parameter(Mandatory=$true)]
    [string]$SourceBranch,
    
    [Parameter(Mandatory=$true)]
    [string]$TargetBranch,
    
    [Parameter(Mandatory=$true)]
    [string]$Message
)

Write-Host "Basculement vers la branche $TargetBranch..." -ForegroundColor Cyan
git checkout $TargetBranch

Write-Host "Fusion de $SourceBranch dans $TargetBranch (--no-ff)..." -ForegroundColor Cyan
git merge $SourceBranch --no-ff -m $Message

Write-Host "Fusion terminée." -ForegroundColor Green