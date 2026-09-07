# Documentation des Scripts PowerShell

## Script : Build-DocFx.ps1
**Chemin relatif** : `scripts\Build-DocFx.ps1`

---
## Script : Commit-Task.ps1
**Chemin relatif** : `scripts\Commit-Task.ps1`

### Paramètres
- `[string] $Message` *(Obligatoire)*

### Dépendances (Invocations)
- *Appelle* ➡️ `Generate-AiDoc.ps1`
- *Appelle* ➡️ `Generate-FileList.ps1`

---
## Script : Complete-Jalon.ps1
**Chemin relatif** : `scripts\Complete-Jalon.ps1`

### Paramètres
- `[string] $JalonBranch` *(Obligatoire)*
- `[string] $BaseBranch`

---
## Script : Complete-Step.ps1
**Chemin relatif** : `scripts\Complete-Step.ps1`

### Paramètres
- `[string] $Message` *(Obligatoire)*
- `[string] $JalonBranch`

### Dépendances (Invocations)
- *Appelle* ➡️ `Commit-Task.ps1`
- *Appelle* ➡️ `Merge-Branch.ps1`

---
## Script : Fix-MissingHash.ps1
**Chemin relatif** : `scripts\Fix-MissingHash.ps1`

---
## Script : Generate-AiDoc.ps1
**Chemin relatif** : `scripts\Generate-AiDoc.ps1`

### Paramètres
- `[string] $SolutionPath`
- `[string] $PSScriptRoot`
- `[string] $OutputPath`
- `[string] $PSScriptRoot`

---
## Script : Generate-FileList.ps1
**Chemin relatif** : `scripts\Generate-FileList.ps1`

### Paramètres
- `[string] $Chemin`
- `[string] $Prefixe`

---
## Script : Merge-Branch.ps1
**Chemin relatif** : `scripts\Merge-Branch.ps1`

### Paramètres
- `[string] $SourceBranch` *(Obligatoire)*
- `[string] $TargetBranch` *(Obligatoire)*
- `[string] $Message` *(Obligatoire)*

---
## Script : Serve-Site.ps1
**Chemin relatif** : `scripts\Serve-Site.ps1`

---
## Script : setup-workspace.ps1
**Chemin relatif** : `scripts\setup-workspace.ps1`

---
## Script : Start-Jalon.ps1
**Chemin relatif** : `scripts\Start-Jalon.ps1`

### Paramètres
- `[string] $JalonBranch` *(Obligatoire)*
- `[string] $BaseBranch`

---
## Script : Start-Step.ps1
**Chemin relatif** : `scripts\Start-Step.ps1`

### Paramètres
- `[string] $StepBranch` *(Obligatoire)*
- `[string] $JalonBranch` *(Obligatoire)*

---
## Script : Test-DocGen.ps1
**Chemin relatif** : `scripts\Test-DocGen.ps1`

---
## Script : zip_source.ps1
**Chemin relatif** : `scripts\zip_source.ps1`

### Dépendances (Invocations)
- *Appelle* ➡️ `Generate-FileList.ps1`

---
