# scripts/Fix-MissingHash.ps1
git add Specs_Catamailer.md
git add Specs_Catamailer_Unit_Tests.md
git add ToDoList.md

git commit -m "chore: Mise a jour des documents de suivi oublies lors de la cloture"

$commitHash = git rev-parse HEAD
Write-Host "Nouveau Hash : $commitHash" -ForegroundColor Yellow

(Get-Content Specs_Catamailer.md) -replace '<COMMIT[-_]HASH>', $commitHash | Set-Content Specs_Catamailer.md -Encoding UTF8
(Get-Content Specs_Catamailer_Unit_Tests.md) -replace '<COMMIT[-_]HASH>', $commitHash | Set-Content Specs_Catamailer_Unit_Tests.md -Encoding UTF8
(Get-Content ToDoList.md) -replace '<COMMIT[-_]HASH>', $commitHash | Set-Content ToDoList.md -Encoding UTF8

git add Specs_Catamailer.md
git add Specs_Catamailer_Unit_Tests.md
git add ToDoList.md
git commit -m "docs: Tracabilite du correctif (Code Hash: $commitHash)"