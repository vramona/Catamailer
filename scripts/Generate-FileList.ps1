# scripts/Generate-FileList.ps1
$cheminRacine = Get-Location
$fichierSortie = ".\FileList.txt"

function Get-Arborescence {
    param (
        [string]$Chemin,
        [string]$Prefixe = ""
    )

    $elements = Get-ChildItem -Path $Chemin -Force | Where-Object {
        $estDossierAExclure = $_.PSIsContainer -and ($_.Name -match '^(bin|obj|\.git)$')
        -not $estDossierAExclure
    }

    $total = $elements.Count
    $index = 0

    foreach ($element in $elements) {
        $index++
        $estDernier = ($index -eq $total)

        $connecteur = if ($estDernier) { "└── " } else { "├── " }
        $prefixeEnfant = if ($estDernier) { "    " } else { "│   " }

        if ($element.PSIsContainer) {
            [PSCustomObject]@{
                "Arborescence / Fichier" = "$Prefixe$connecteur$($element.Name)"
                "Taille"                 = ""
                "Date de MAJ"            = $element.LastWriteTime.ToString("yyyy-MM-dd HH:mm")
            }
            Get-Arborescence -Chemin $element.FullName -Prefixe "$Prefixe$prefixeEnfant"
        }
        else {
            $taille = if ($element.Length -ge 1MB) { "{0:N2} Mo" -f ($element.Length / 1MB) } 
                      elseif ($element.Length -ge 1KB) { "{0:N2} Ko" -f ($element.Length / 1KB) } 
                      else { "$($element.Length) octets" }

            [PSCustomObject]@{
                "Arborescence / Fichier" = "$Prefixe$connecteur$($element.Name)"
                "Taille"                 = $taille
                "Date de MAJ"            = $element.LastWriteTime.ToString("yyyy-MM-dd HH:mm")
            }
        }
    }
}

Get-Arborescence -Chemin $cheminRacine | 
    Format-Table -AutoSize | 
    Out-File -FilePath $fichierSortie -Encoding UTF8 -Width 300

Write-Host "L'arborescence a été générée avec succès dans $fichierSortie (sans bin, obj, ni .git)" -ForegroundColor Green