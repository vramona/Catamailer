# scripts/Generate-AiDoc.ps1
param (
    [string]$SolutionPath = "$PSScriptRoot\..\Catamailer.slnx",
    [string]$OutputPath = "$PSScriptRoot\..\doc\Ai_Architecture.md"
)

Write-Host "Lancement de la génération de documentation IA..." -ForegroundColor Cyan

$ResolvedSolutionPath = Resolve-Path $SolutionPath -ErrorAction Stop
$ResolvedOutputPath = [System.IO.Path]::GetFullPath([System.IO.Path]::Combine($PWD.Path, $OutputPath))

$AiDocGeneratorPath = "$PSScriptRoot\..\src\Tools\AiDocGenerator\AiDocGenerator.csproj"

if (Test-Path $AiDocGeneratorPath) {
    dotnet run --project "$AiDocGeneratorPath" "$ResolvedSolutionPath" "$ResolvedOutputPath"

    if ($LASTEXITCODE -eq 0) {
        Write-Host "Documentation générée avec succès : $ResolvedOutputPath" -ForegroundColor Green
    } else {
        Write-Host "Erreur lors de la génération de la documentation." -ForegroundColor Red
    }
} else {
    Write-Host "ATTENTION : Le projet AiDocGenerator n'existe pas encore à l'emplacement $AiDocGeneratorPath." -ForegroundColor Yellow
}