# scripts/zip_source.ps1
# ==========================================
# 0. Détermination du répertoire de la solution
# ==========================================
$ScriptPath = $MyInvocation.MyCommand.Definition
if ($ScriptPath) {
    $PossibleSolutionDir = Split-Path -Parent (Split-Path -Parent $ScriptPath)
} else {
    $PossibleSolutionDir = (Get-Location).Path
}

# Fallback sur le répertoire courant si Generate-FileList.ps1 n'est pas trouvé dans scripts
if (Test-Path (Join-Path $PossibleSolutionDir "scripts\Generate-FileList.ps1")) {
    $SolutionDir = $PossibleSolutionDir
} else {
    $SolutionDir = (Get-Location).Path
}

Set-Location -Path $SolutionDir

# ==========================================
# 1. Récupération de la Date et de la Branche Git
# ==========================================
Write-Host "[1/5] Récupération de l'environnement..." -ForegroundColor Cyan
$DateStr = Get-Date -Format "yyyyMMdd-HHmmss"
try {
    $Branch = git rev-parse --abbrev-ref HEAD 2>$null
    if ([string]::IsNullOrWhiteSpace($Branch)) { $Branch = "main" }
} catch {
    $Branch = "main"
}
$Branch = $Branch -replace '[\\/:*?"<>|]', '_'

$ZipFileName = "Catamailer_${DateStr}_${Branch}.zip"
$ZipFilePath = Join-Path $SolutionDir $ZipFileName

# ==========================================
# 🛡️ MÉCANISME ANTI-SPAM (COOLDOWN 5 MIN)
# ==========================================
$recentZip = Get-ChildItem -Path $SolutionDir -Filter "Catamailer_*_${Branch}.zip" | Sort-Object LastWriteTime -Descending | Select-Object -First 1
if ($recentZip) {
    $timeSinceLastZip = (Get-Date) - $recentZip.LastWriteTime
    if ($timeSinceLastZip.TotalMinutes -lt 5) {
        Write-Host "⏳ [Anti-Spam] Archive récente détectée sur cette branche ($($recentZip.Name)). Sauvegarde ignorée." -ForegroundColor Yellow
        exit 0
    }
}
# ==========================================

# ==========================================
# 2. Exécution du script de liste de fichiers
# ==========================================
$ListScript = Join-Path $SolutionDir "scripts\Generate-FileList.ps1"
if (Test-Path $ListScript) {
    Write-Host "[2/5] Exécution du script de liste de fichiers..." -ForegroundColor Cyan
    & $ListScript
} else {
    Write-Host "[2/5] Attention : Generate-FileList.ps1 introuvable dans $SolutionDir\scripts" -ForegroundColor Yellow
}

if (Test-Path $ZipFilePath) { Remove-Item $ZipFilePath -Force }
Write-Host "      -> Fichier cible : $ZipFileName"

# ==========================================
# 3. Préparation et copie des fichiers
# ==========================================
Write-Host "[3/5] Préparation des fichiers..." -ForegroundColor Cyan
$TempDir = Join-Path $SolutionDir "TempZip"
if (Test-Path $TempDir) { Remove-Item $TempDir -Recurse -Force }
New-Item -ItemType Directory -Path $TempDir | Out-Null

$RootExclusions = @(".git", ".github", ".vs", "TempZip", "logs")
$FileExclusions = @(".gitignore", ".gitattributes")

Get-ChildItem -Path $SolutionDir | Where-Object {
    $_.Name -notin $RootExclusions -and $_.Name -notin $FileExclusions -and $_.Extension -ne ".zip"
} | Copy-Item -Destination $TempDir -Recurse -Force

Get-ChildItem -Path $TempDir -Include "bin","obj","TestResults","*.user" -Recurse -Directory -Force | Remove-Item -Recurse -Force

# ==========================================
# 4. Compression vers l'archive finale
# ==========================================
Write-Host "[4/5] Compression en cours..." -ForegroundColor Cyan
Compress-Archive -Path "$TempDir\*" -DestinationPath $ZipFilePath -Force

Remove-Item $TempDir -Recurse -Force

Write-Host "Archive générée avec succès : $ZipFilePath" -ForegroundColor Green

# ==========================================
# 5. Purge des anciennes archives
# ==========================================
Write-Host "[5/5] Purge des anciennes archives..." -ForegroundColor Cyan
$TodayStr = (Get-Date).ToString("yyyyMMdd")
$AllZips = Get-ChildItem -Path $SolutionDir -Filter "Catamailer_*.zip"

$Regex = "^Catamailer_(\d{8})-\d{6}_(.+)\.zip$"

$ZipsWithMeta = foreach ($zip in $AllZips) {
    if ($zip.Name -match $Regex) {
        [PSCustomObject]@{
            File = $zip
            DateStr = $Matches[1]
            Branch = $Matches[2]
        }
    }
}

$GroupedByBranch = $ZipsWithMeta | Group-Object Branch

foreach ($branchGroup in $GroupedByBranch) {
    $GroupedByDay = $branchGroup.Group | Group-Object DateStr
    
    foreach ($dayGroup in $GroupedByDay) {
        $SortedFiles = $dayGroup.Group | Sort-Object {$_.File.LastWriteTime} -Descending
        
        if ($dayGroup.Name -eq $TodayStr) {
            $FilesToDelete = $SortedFiles | Select-Object -Skip 5
        } else {
            $FilesToDelete = $SortedFiles | Select-Object -Skip 1
        }
        
        if ($FilesToDelete) {
            foreach ($f in $FilesToDelete) {
                Remove-Item $f.File.FullName -Force
                Write-Host "      -> Supprimé (Purge) : $($f.File.Name)" -ForegroundColor DarkGray
            }
        }
    }
}

Write-Host "Opération terminée !" -ForegroundColor Green