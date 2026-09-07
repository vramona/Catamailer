# Contexte d'Architecture IA et Arbre des Invocations
Généré le : 2026-09-07 13:22

## Projet : Catamailer.Application
### Class : ClassificationEngine
**Fichier** : `src\Catamailer.Application\ClassificationEngine.cs`
**Rôle** : Moteur d'évaluation de l'Étape 1 : déduction de la catégorie principale en fonction des dictionnaires de mots-clés.
**Membres et Invocations :**
- `ClassificationResult? Classify(string subject, string sender, IEnumerable<string> recipients, IEnumerable<DictionaryRule> rules)` : Évalue les métadonnées d'un e-mail par rapport à un ensemble de règles de dictionnaire.
  - *Appelle* ➡️ `CategoryNode.GetAscendanceChain()`

### Class : ClassificationResult
**Fichier** : `src\Catamailer.Application\ClassificationResult.cs`
**Rôle** : Représente le résultat de l'évaluation de l'Étape 1 (Classification).
**Membres et Invocations :**

## Projet : Catamailer.Domain
### Class : CategoryNode
**Fichier** : `src\Catamailer.Domain\CategoryNode.cs`
**Rôle** : Représente un nœud dans l'arbre hiérarchique des catégories, agissant comme vérité absolue (Master Data Management).
**Membres et Invocations :**
- `void AddChild(CategoryNode child)` : Ajoute un nœud enfant à cette catégorie et lie automatiquement ce nœud à ce parent.
- `IEnumerable<CategoryNode> GetAscendanceChain()` : Récupère la chaîne d'ascendance complète depuis la racine jusqu'à ce nœud inclus.

### Class : DictionaryRule
**Fichier** : `src\Catamailer.Domain\DictionaryRule.cs`
**Rôle** : Représente une règle de l'Étape 1 (Classification) liant un ensemble de mots-clés à une catégorie déduite.
**Membres et Invocations :**

### Interface : ICategoryRepository
**Fichier** : `src\Catamailer.Domain\ICategoryRepository.cs`
**Rôle** : Définit le contrat pour l'accès aux données de l'entité CategoryNode.
**Membres et Invocations :**

### Class : RuleAction
**Fichier** : `src\Catamailer.Domain\RuleAction.cs`
**Rôle** : Représente l'action finale à exécuter si un arbre de conditions est validé.
**Membres et Invocations :**

### Class : RuleCriterion
**Fichier** : `src\Catamailer.Domain\RuleCriterion.cs`
**Rôle** : Représente un critère de filtrage unitaire dans l'arbre d'exécution de l'Étape 2.
**Membres et Invocations :**

### Class : RuleNode
**Fichier** : `src\Catamailer.Domain\RuleNode.cs`
**Rôle** : Représente un nœud dans l'arbre composite des conditions d'exécution de l'Étape 2.
**Membres et Invocations :**
- `void AddCriterion(RuleCriterion criterion)` : Ajoute un critère de validation unitaire à ce nœud.
- `void AddChildNode(RuleNode childNode)` : Ajoute un nœud enfant permettant d'imbriquer une nouvelle couche logique.

## Projet : Catamailer.Infrastructure
### Class : CatamailerDbContext
**Fichier** : `src\Catamailer.Infrastructure\CatamailerDbContext.cs`
**Rôle** : Contexte de base de données principal pour Catamailer (Entity Framework Core SQLite).
**Membres et Invocations :**

### Class : CategoryRepository
**Fichier** : `src\Catamailer.Infrastructure\CategoryRepository.cs`
**Rôle** : Implémentation SQLite du dépôt pour les catégories utilisant Entity Framework Core.
**Membres et Invocations :**
- `Task AddAsync(CategoryNode category)`
- `Task<CategoryNode?> GetByNameAsync(string name)`

## Projet : Catamailer.Migrator
## Projet : Catamailer.UI
### Class : App
**Fichier** : `src\Catamailer.UI\App.xaml.cs`
**Membres et Invocations :**

### Class : MainPage
**Fichier** : `src\Catamailer.UI\MainPage.xaml.cs`
**Membres et Invocations :**

### Class : MauiProgram
**Fichier** : `src\Catamailer.UI\MauiProgram.cs`
**Membres et Invocations :**
- `MauiApp CreateMauiApp()`

### Class : App
**Fichier** : `src\Catamailer.UI\Platforms\Windows\App.xaml.cs`
**Rôle** : Provides application-specific behavior to supplement the default Application class.
**Membres et Invocations :**


### Composants Razor
- **Routes** : `src\Catamailer.UI\Components\Routes.razor`
- **_Imports** : `src\Catamailer.UI\Components\_Imports.razor`
- **MainLayout** : `src\Catamailer.UI\Components\Layout\MainLayout.razor`
- **NavMenu** : `src\Catamailer.UI\Components\Layout\NavMenu.razor`
- **Counter** (Route: `/counter`) : `src\Catamailer.UI\Components\Pages\Counter.razor`
- **Home** (Route: `/`) : `src\Catamailer.UI\Components\Pages\Home.razor`
- **NotFound** (Route: `/not-found`) : `src\Catamailer.UI\Components\Pages\NotFound.razor`
- **Weather** (Route: `/weather`) : `src\Catamailer.UI\Components\Pages\Weather.razor`

## Projet : AiDocGenerator
### Class : CSharpAnalyzer
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\CSharpAnalyzer.cs`
**Membres et Invocations :**
- `Task<string> AnalyzeProjectAsync(Project project, string solutionDir)`
  - *Appelle* ➡️ `CSharpAnalyzer.GetXmlSummary()`

### Class : CssComponentInfo
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\CssAnalyzer.cs`
**Membres et Invocations :**

### Class : CssAnalyzer
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\CssAnalyzer.cs`
**Membres et Invocations :**
- `CssComponentInfo ExtractCssInfo(string fileContent, string absoluteFilePath, string solutionDir)`

### Class : ScriptParameter
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\PowerShellAnalyzer.cs`
**Rôle** : Représente un paramètre extrait d'un script PowerShell.
**Membres et Invocations :**

### Class : PowerShellScriptInfo
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\PowerShellAnalyzer.cs`
**Rôle** : Contient les métadonnées et dépendances extraites d'un fichier script PowerShell (.ps1).
**Membres et Invocations :**

### Class : PowerShellAnalyzer
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\PowerShellAnalyzer.cs`
**Rôle** : Analyseur statique chargé de parser le contenu des fichiers PowerShell (.ps1) pour extraire l'arbre des invocations et les paramètres via des expressions régulières.
**Membres et Invocations :**
- `PowerShellScriptInfo ExtractScriptInfo(string fileContent, string absoluteFilePath, string solutionDir)` : Extrait les informations d'un script PowerShell à partir de son contenu texte.

### Class : RazorComponentInfo
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\RazorAnalyzer.cs`
**Membres et Invocations :**

### Class : RazorAnalyzer
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\RazorAnalyzer.cs`
**Membres et Invocations :**
- `RazorComponentInfo ExtractComponentInfo(string fileContent, string absoluteFilePath, string solutionDir)`

### Class : TestDetail
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\TestResultsAnalyzer.cs`
**Membres et Invocations :**

### Class : TestRunSummary
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\TestResultsAnalyzer.cs`
**Membres et Invocations :**

### Class : TestResultsAnalyzer
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\TestResultsAnalyzer.cs`
**Membres et Invocations :**
- `TestRunSummary ParseTrx(string filePath)`

### Class : CatamailerTocBuilder
**Fichier** : `src\Tools\AiDocGenerator\Helpers\CatamailerTocBuilder.cs`
**Rôle** : Classe responsable de la génération du contenu YAML pour la table des matières principale de la documentation de la solution Catamailer.
**Membres et Invocations :**
- `string Build()` : Construit le contenu YAML de la table des matières principale.

### Class : TocModifier
**Fichier** : `src\Tools\AiDocGenerator\Helpers\TocModifiers.cs`
**Rôle** : Utilitaire pour manipuler et mettre à jour les fichiers de table des matières (toc.yml) de DocFX.
**Membres et Invocations :**
- `void InjectEntryIfNeeded(string tocPath, string name, string href)` : Injecte une nouvelle entrée dans un fichier toc.yml si elle n'existe pas déjà.

## Projet : Catamailer.Application.Tests
### Class : ClassificationEngineTests
**Fichier** : `tests\Catamailer.Application.Tests\ClassificationEngineTests.cs`
**Rôle** : Classe de test validant le comportement du moteur de classification (Étape 1).
**Membres et Invocations :**
- `void Classify_ShouldReturnNull_WhenNoRulesProvided()`
  - *Appelle* ➡️ `ClassificationEngine.Classify()`
- `void Classify_ShouldReturnCategoryAndAscendanceChain_WhenKeywordMatches()`
  - *Appelle* ➡️ `CategoryNode.AddChild()`
  - *Appelle* ➡️ `ClassificationEngine.Classify()`
- `void Classify_ShouldReturnCategory_WhenKeywordMatchesRecipient()`
  - *Appelle* ➡️ `ClassificationEngine.Classify()`

## Projet : Catamailer.Domain.Tests
### Class : CategoryNodeTests
**Fichier** : `tests\Catamailer.Domain.Tests\CategoryNodeTests.cs`
**Rôle** : Classe de test validant les règles métier de l'arbre des catégories (CategoryNode).
**Membres et Invocations :**
- `void CategoryNode_Creation_ShouldSetPropertiesCorrectly()`
- `void AddChild_ShouldSetParentAndInheritColor_WhenNoColorSpecified()`
  - *Appelle* ➡️ `CategoryNode.AddChild()`
- `void EffectiveColor_ShouldOverrideParentColor_WhenColorIsExplicitlySet()`
  - *Appelle* ➡️ `CategoryNode.AddChild()`
- `void GetAscendanceChain_ShouldReturnFullHierarchy_FromRootToNode()`
  - *Appelle* ➡️ `CategoryNode.AddChild()`
  - *Appelle* ➡️ `CategoryNode.GetAscendanceChain()`

### Class : RuleModelsTests
**Fichier** : `tests\Catamailer.Domain.Tests\RuleModelsTests.cs`
**Rôle** : Classe de test validant la modélisation des entités de règles (DictionaryRule, RuleNode, RuleCriterion, RuleAction).
**Membres et Invocations :**
- `void DictionaryRule_Creation_ShouldSetProperties()`
- `void RuleAction_Creation_ShouldSetActionTypeAndParameter()`
- `void RuleCriterion_Creation_ShouldSetConditionFields()`
- `void RuleNode_ShouldActAsComposite_HoldingCriteriaAndChildNodes()`
  - *Appelle* ➡️ `RuleNode.AddCriterion()`
  - *Appelle* ➡️ `RuleNode.AddChildNode()`

## Projet : Catamailer.Infrastructure.Tests
### Class : CatamailerDbContextTests
**Fichier** : `tests\Catamailer.Infrastructure.Tests\CatamailerDbContextTests.cs`
**Rôle** : Classe de test validant l'intégration d'Entity Framework Core SQLite.
**Membres et Invocations :**
- `void EnsureCreated_ShouldCreateDatabaseAndTables()`
- `void CanSaveAndRetrieve_CategoryNode()`

### Class : CategoryRepositoryTests
**Fichier** : `tests\Catamailer.Infrastructure.Tests\CategoryRepositoryTests.cs`
**Rôle** : Classe de test validant le comportement du dépôt (Repository) des catégories.
**Membres et Invocations :**
- `Task AddAsync_ShouldPersistCategory()`
  - *Appelle* ➡️ `CategoryRepositoryTests.GetInMemoryContext()`
  - *Appelle* ➡️ `CategoryRepository.AddAsync()`
- `Task GetByNameAsync_ShouldReturnCategory_WhenExists()`
  - *Appelle* ➡️ `CategoryRepositoryTests.GetInMemoryContext()`
  - *Appelle* ➡️ `CategoryRepository.GetByNameAsync()`

## Projet : Tools.AiDocGenerator.Tests
### Class : CatamailerTocBuilderTests
**Fichier** : `tests\Tools.AiDocGenerator.Tests\CatamailerTocBuilderTests.cs`
**Membres et Invocations :**
- `void Build_ShouldReturnCatamailerSpecificToc()` : Vérifie que le TOC généré est spécifiquement adapté à l'architecture de Catamailer et ne contient plus aucune référence à l'ancien projet Sudoku.
  - *Appelle* ➡️ `CatamailerTocBuilder.Build()`

### Scripts PowerShell et Outils d'Automatisation
- **Build-DocFx.ps1** : `scripts\Build-DocFx.ps1`
- **Commit-Task.ps1** : `scripts\Commit-Task.ps1`
  - Paramètre : `[string] $Message` *(Obligatoire)*
  - *Appelle* ➡️ `Generate-AiDoc.ps1`
  - *Appelle* ➡️ `Generate-FileList.ps1`
- **Complete-Jalon.ps1** : `scripts\Complete-Jalon.ps1`
  - Paramètre : `[string] $JalonBranch` *(Obligatoire)*
  - Paramètre : `[string] $BaseBranch`
- **Complete-Step.ps1** : `scripts\Complete-Step.ps1`
  - Paramètre : `[string] $Message` *(Obligatoire)*
  - Paramètre : `[string] $JalonBranch`
  - *Appelle* ➡️ `Commit-Task.ps1`
  - *Appelle* ➡️ `Merge-Branch.ps1`
- **Fix-MissingHash.ps1** : `scripts\Fix-MissingHash.ps1`
- **Generate-AiDoc.ps1** : `scripts\Generate-AiDoc.ps1`
  - Paramètre : `[string] $SolutionPath`
  - Paramètre : `[string] $PSScriptRoot`
  - Paramètre : `[string] $OutputPath`
  - Paramètre : `[string] $PSScriptRoot`
- **Generate-FileList.ps1** : `scripts\Generate-FileList.ps1`
  - Paramètre : `[string] $Chemin`
  - Paramètre : `[string] $Prefixe`
- **Merge-Branch.ps1** : `scripts\Merge-Branch.ps1`
  - Paramètre : `[string] $SourceBranch` *(Obligatoire)*
  - Paramètre : `[string] $TargetBranch` *(Obligatoire)*
  - Paramètre : `[string] $Message` *(Obligatoire)*
- **Serve-Site.ps1** : `scripts\Serve-Site.ps1`
- **setup-workspace.ps1** : `scripts\setup-workspace.ps1`
- **Start-Jalon.ps1** : `scripts\Start-Jalon.ps1`
  - Paramètre : `[string] $JalonBranch` *(Obligatoire)*
  - Paramètre : `[string] $BaseBranch`
- **Start-Step.ps1** : `scripts\Start-Step.ps1`
  - Paramètre : `[string] $StepBranch` *(Obligatoire)*
  - Paramètre : `[string] $JalonBranch` *(Obligatoire)*
- **Test-DocGen.ps1** : `scripts\Test-DocGen.ps1`
- **zip_source.ps1** : `scripts\zip_source.ps1`
  - *Appelle* ➡️ `Generate-FileList.ps1`

