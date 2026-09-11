# Contexte d'Architecture IA et Arbre des Invocations
Généré le : 2026-09-11 14:41

## Projet : Catamailer.Application
### Class : ClassificationEngine
**Fichier** : `src\Catamailer.Application\ClassificationEngine.cs`
**Rôle** : Moteur d'évaluation de l'Étape 1 : déduction de la catégorie principale en fonction des dictionnaires de mots-clés contextuels.
**Membres et Invocations :**
- `ClassificationResult? Classify(MailMetadata metadata, IEnumerable<DictionaryRule> rules)` : Évalue les métadonnées d'un e-mail par rapport à un ensemble de règles de dictionnaire.
  - *Appelle* ➡️ `CategoryNode.GetAscendanceChain()`

### Class : ClassificationResult
**Fichier** : `src\Catamailer.Application\ClassificationResult.cs`
**Rôle** : Représente le résultat de l'évaluation de l'Étape 1 (Classification).
**Membres et Invocations :**
- `CategoryNode MatchedCategory { get; }` : Obtient la catégorie principale déduite par le moteur.
- `IReadOnlyList<CategoryNode> AppliedCategories { get; }` : Obtient la chaîne d'ascendance complète des catégories appliquées.

### Class : DebounceService
**Fichier** : `src\Catamailer.Application\DebounceService.cs`
**Rôle** : Service gérant la suspension temporaire de l'analyse d'historique lors des modifications de règles.
**Membres et Invocations :**
- `Task SuspendAnalysisAsync()`
  - *Appelle* ➡️ `IAppSettingsRepository.GetSettingAsync()`
  - *Appelle* ➡️ `IHistoryStateRepository.SetStateAsync()`
- `Task<bool> IsAnalysisSuspendedAsync()`
  - *Appelle* ➡️ `IHistoryStateRepository.GetStateAsync()`

### Class : ExecutionEngine
**Fichier** : `src\Catamailer.Application\ExecutionEngine.cs`
**Rôle** : Moteur d'exécution de l'Étape 2 : évaluation de l'arbre booléen des conditions.
**Membres et Invocations :**
- `bool Evaluate(string subject, string sender, RuleNode node)` : Évalue un nœud de règle composite par rapport aux métadonnées d'un e-mail.
  - *Appelle* ➡️ `ExecutionEngine.EvaluateCriterion()`
  - *Appelle* ➡️ `ExecutionEngine.Evaluate()`

### Class : HistoryRunner
**Fichier** : `src\Catamailer.Application\HistoryRunner.cs`
**Rôle** : Implémentation du service d'arrière-plan de rattrapage de l'historique des e-mails.
**Membres et Invocations :**
- `Task<int> ProcessPendingHistoryAsync(int maxItemsToProcess)`
  - *Appelle* ➡️ `IDebounceService.IsAnalysisSuspendedAsync()`
  - *Appelle* ➡️ `IHistoryStateRepository.GetStateAsync()`

### Class : ShadowModeService
**Fichier** : `src\Catamailer.Application\ShadowModeService.cs`
**Rôle** : Service gérant la télémétrie Shadow Mode, le contrôle d'activation et les paliers de notification.
**Membres et Invocations :**
- `Task RecordPredictionAsync(string entryId, string predictedCategory, string actualCategory)`
  - *Appelle* ➡️ `IHistoryStateRepository.GetStateAsync()`
  - *Appelle* ➡️ `IHistoryStateRepository.SetStateAsync()`
- `Task<bool> IsAutoWriteEnabledAsync()`
  - *Appelle* ➡️ `IAppSettingsRepository.GetSettingAsync()`
- `Task SetAutoWriteEnabledAsync(bool enabled)`
  - *Appelle* ➡️ `IAppSettingsRepository.SetSettingAsync()`
- `Task<bool> ShouldPromptForActivationAsync()`
  - *Appelle* ➡️ `ShadowModeService.IsAutoWriteEnabledAsync()`
  - *Appelle* ➡️ `IHistoryStateRepository.GetStateAsync()`
- `Task AcknowledgeActivationPromptAsync()`
  - *Appelle* ➡️ `IHistoryStateRepository.GetStateAsync()`
  - *Appelle* ➡️ `IHistoryStateRepository.SetStateAsync()`
- `Task<(int MatchCount, int CategoryCount, bool IsAutoWriteEnabled)> GetTelemetryStatsAsync()`
  - *Appelle* ➡️ `ShadowModeService.IsAutoWriteEnabledAsync()`
  - *Appelle* ➡️ `IHistoryStateRepository.GetStateAsync()`

### Class : CategoryTreeViewModel
**Fichier** : `src\Catamailer.Application\ViewModels\CategoryTreeViewModel.cs`
**Rôle** : ViewModel responsable de la gestion et de l'affichage de l'arborescence des catégories.
**Membres et Invocations :**
- `IReadOnlyList<CategoryNode> RootCategories { get; set; }` : Obtient la liste des catégories de niveau racine (n'ayant aucun parent).
- `Task InitializeAsync()` : Charge l'ensemble des catégories depuis le dépôt et construit la liste des nœuds racines.
  - *Appelle* ➡️ `ICategoryRepository.GetAllAsync()`

### Class : DictionaryEditorViewModel
**Fichier** : `src\Catamailer.Application\ViewModels\DictionaryEditorViewModel.cs`
**Rôle** : ViewModel responsable de la gestion (CRUD) des règles de dictionnaires (Étape 1).
**Membres et Invocations :**
- `IEnumerable<DictionaryRule> Rules { get; }` : Obtient la liste observable des règles de dictionnaire.
- `IEnumerable<CategoryNode> AvailableCategories { get; }` : Obtient la liste des catégories disponibles pour la création d'une règle.
- `CategoryNode? SelectedCategory { get; set; }` : Obtient ou définit la catégorie sélectionnée dans le formulaire.
- `string SubjectKeywordsInput { get; set; }` : Obtient ou définit les mots-clés du sujet saisis dans le formulaire (séparés par des virgules).
- `string SenderKeywordsInput { get; set; }` : Obtient ou définit les mots-clés de l'expéditeur saisis dans le formulaire (séparés par des virgules).
- `string RecipientKeywordsInput { get; set; }` : Obtient ou définit les mots-clés du destinataire saisis dans le formulaire (séparés par des virgules).
- `Task InitializeAsync()` : Charge l'ensemble des règles de dictionnaire et des catégories depuis les dépôts.
  - *Appelle* ➡️ `IRuleRepository.GetAllDictionaryRulesAsync()`
  - *Appelle* ➡️ `ICategoryRepository.GetAllAsync()`
- `Task AddRuleAsync(DictionaryRule rule)` : Ajoute une nouvelle règle au dépôt et met à jour la liste en mémoire.
  - *Appelle* ➡️ `IRuleRepository.AddDictionaryRuleAsync()`
- `Task DeleteRuleAsync(DictionaryRule rule)` : Supprime une règle du dépôt et met à jour la liste en mémoire.
  - *Appelle* ➡️ `IRuleRepository.DeleteDictionaryRuleAsync()`
- `Task CreateRuleFromFormAsync()` : Crée une nouvelle règle à partir des données saisies dans le formulaire.
  - *Appelle* ➡️ `DictionaryEditorViewModel.AddRuleAsync()`

### Class : QuickCategorizeViewModel
**Fichier** : `src\Catamailer.Application\ViewModels\QuickCategorizeViewModel.cs`
**Rôle** : ViewModel responsable de la logique de l'écran Quick Categorize (Recherche, Tris, Actions).
**Membres et Invocations :**
- `string SearchText { get; set; }` : Obtient ou définit le texte de recherche courant.
- `IEnumerable<CategoryNode> FilteredCategories { get; set; }` : Obtient la liste des catégories filtrées selon la recherche.
- `CategoryNode? SelectedCategory { get; set; }` : Obtient la catégorie actuellement sélectionnée par l'utilisateur.
- `Task InitializeAsync()` : Initialise le ViewModel en chargeant l'intégralité des catégories existantes.
  - *Appelle* ➡️ `ICategoryRepository.GetAllAsync()`
- `Task UpdateSearchAsync(string searchText)` : Met à jour les résultats filtrés en fonction du texte de recherche fourni. La recherche ignore la casse et retourne l'intégralité des éléments si le texte est vide.
- `void SelectCategory(CategoryNode category)` : Définit la catégorie sélectionnée par l'utilisateur.

### Class : SelectableOption
**Fichier** : `src\Catamailer.Application\ViewModels\QuickRuleBuilderViewModel.cs`
**Rôle** : Représente une option sélectionnable dans l'IHM (case à cocher).
**Membres et Invocations :**
- `string Value { get; set; }` : La valeur textuelle de l'option.
- `bool IsSelected { get; set; }` : Indique si l'utilisateur a coché cette option.

### Class : QuickRuleBuilderViewModel
**Fichier** : `src\Catamailer.Application\ViewModels\QuickRuleBuilderViewModel.cs`
**Rôle** : ViewModel responsable de la logique de l'écran Quick Rule Builder (Étape 1).
**Membres et Invocations :**
- `SelectableOption? SubjectOption { get; set; }` : Option de sélection pour le sujet de l'e-mail.
- `List<SelectableOption> SenderOptions { get; set; }` : Options de sélection pour les expéditeurs (Sender et OnBehalfOf).
- `List<SelectableOption> RecipientOptions { get; set; }` : Options de sélection pour les destinataires externes.
- `IEnumerable<CategoryNode> Categories { get; set; }` : Obtient la liste globale des catégories disponibles.
- `IEnumerable<CategoryNode> TriggeredCategories { get; set; }` : Obtient la liste des catégories qui sont déjà déclenchées par l'e-mail courant.
- `CategoryNode? SelectedCategory { get; set; }` : Obtient la catégorie cible actuellement sélectionnée pour la règle.
- `Task InitializeAsync()` : Initialise le ViewModel en récupérant les métadonnées, en construisant les options de filtrage et en identifiant les catégories déjà déclenchées.
  - *Appelle* ➡️ `ICategoryRepository.GetAllAsync()`
  - *Appelle* ➡️ `IRuleRepository.GetAllDictionaryRulesAsync()`
  - *Appelle* ➡️ `ISelectionProvider.GetSelectedMail()`
  - *Appelle* ➡️ `ClassificationEngine.Classify()`
- `void SelectCategory(CategoryNode category)` : Définit la catégorie cible pour la règle en cours de création.
- `DictionaryRule? BuildRule()` : Construit l'objet DictionaryRule final en incluant uniquement les options cochées.

### Class : ShadowModeDashboardViewModel
**Fichier** : `src\Catamailer.Application\ViewModels\ShadowModeDashboardViewModel.cs`
**Rôle** : ViewModel responsable de l'affichage et de la gestion des statistiques du Shadow Mode.
**Membres et Invocations :**
- `int MatchCount { get; set; }` : Obtient le nombre de correspondances exactes validées par la télémétrie.
- `int CategoryCount { get; set; }` : Obtient le nombre de catégories distinctes validées par la télémétrie.
- `bool IsAutoWriteEnabled { get; set; }` : Obtient un indicateur précisant si l'écriture automatique est activée.
- `Task InitializeAsync()` : Charge les statistiques actuelles depuis le service.
  - *Appelle* ➡️ `IShadowModeService.GetTelemetryStatsAsync()`
- `Task ToggleAutoWriteAsync(bool enabled)` : Active ou désactive l'écriture automatique et met à jour l'état.
  - *Appelle* ➡️ `IShadowModeService.SetAutoWriteEnabledAsync()`
  - *Appelle* ➡️ `ShadowModeDashboardViewModel.InitializeAsync()`

## Projet : Catamailer.Domain
### Class : AppSetting
**Fichier** : `src\Catamailer.Domain\AppSetting.cs`
**Rôle** : Représente un paramètre de configuration globale de l'application.
**Membres et Invocations :**
- `string Key { get; set; }` : La clé unique du paramètre (ex: "HistoryDebounceDelayMinutes").
- `string Value { get; set; }` : La valeur du paramètre stockée sous forme de chaîne.

### Class : CategoryNode
**Fichier** : `src\Catamailer.Domain\CategoryNode.cs`
**Rôle** : Représente un nœud dans l'arbre hiérarchique des catégories, agissant comme vérité absolue (Master Data Management).
**Membres et Invocations :**
- `string Name { get; set; }` : Obtient le nom de la catégorie.
- `string? Color { get; set; }` : Obtient la couleur explicitement définie pour cette catégorie, ou null si elle doit hériter de son parent.
- `CategoryNode? Parent { get; set; }` : Obtient le parent de cette catégorie dans l'arbre.
- `IReadOnlyList<CategoryNode> Children { get; }` : Obtient la liste en lecture seule des enfants de cette catégorie.
- `string? EffectiveColor { get; }` : Obtient la couleur effective de la catégorie, en héritant de son ascendance si aucune couleur explicite n'est définie.
- `void AddChild(CategoryNode child)` : Ajoute un nœud enfant à cette catégorie et lie automatiquement ce nœud à ce parent.
- `IEnumerable<CategoryNode> GetAscendanceChain()` : Récupère la chaîne d'ascendance complète depuis la racine jusqu'à ce nœud inclus.

### Class : DictionaryRule
**Fichier** : `src\Catamailer.Domain\DictionaryRule.cs`
**Rôle** : Représente une règle de l'Étape 1 (Classification) liant des mots-clés spécifiques (Sujet, Expéditeur, Destinataire) à une catégorie déduite.
**Membres et Invocations :**
- `CategoryNode TargetCategory { get; }` : Obtient la catégorie cible qui sera déduite si la règle correspond.
- `IReadOnlyList<string> SubjectKeywords { get; }` : Obtient la liste des mots-clés recherchés dans le sujet.
- `IReadOnlyList<string> SenderKeywords { get; }` : Obtient la liste des mots-clés (adresses ou noms) recherchés parmi les expéditeurs.
- `IReadOnlyList<string> RecipientKeywords { get; }` : Obtient la liste des mots-clés (adresses ou noms) recherchés parmi les destinataires.

### Interface : IAppSettingsRepository
**Fichier** : `src\Catamailer.Domain\IAppSettingsRepository.cs`
**Rôle** : Contrat pour la gestion des paramètres globaux de l'application.
**Membres et Invocations :**

### Interface : ICategoryManagerProvider
**Fichier** : `src\Catamailer.Domain\ICategoryManagerProvider.cs`
**Rôle** : Définit le contrat permettant de gérer les catégories au sein du fournisseur de messagerie (ex: Master Category List).
**Membres et Invocations :**

### Interface : ICategoryRepository
**Fichier** : `src\Catamailer.Domain\ICategoryRepository.cs`
**Rôle** : Définit le contrat pour l'accès aux données de l'entité CategoryNode.
**Membres et Invocations :**

### Interface : IDebounceService
**Fichier** : `src\Catamailer.Domain\IDebounceService.cs`
**Rôle** : Définit le contrat permettant de gérer la suspension temporaire des traitements d'arrière-plan (Debounce).
**Membres et Invocations :**

### Interface : IGlobalHotkeyService
**Fichier** : `src\Catamailer.Domain\IGlobalHotkeyService.cs`
**Rôle** : Définit le contrat pour le service de gestion des raccourcis clavier globaux au niveau de l'OS.
**Membres et Invocations :**

### Interface : IHistoryRunner
**Fichier** : `src\Catamailer.Domain\IHistoryRunner.cs`
**Rôle** : Définit le contrat du service d'arrière-plan analysant le stock d'e-mails historiques.
**Membres et Invocations :**

### Interface : IHistoryStateRepository
**Fichier** : `src\Catamailer.Domain\IHistoryStateRepository.cs`
**Rôle** : Contrat pour la gestion des états systèmes internes.
**Membres et Invocations :**

### Interface : IMailProvider
**Fichier** : `src\Catamailer.Domain\IMailProvider.cs`
**Rôle** : Définit le contrat d'écoute et d'interaction avec le fournisseur de messagerie.
**Membres et Invocations :**

### Interface : IMassUpdateProvider
**Fichier** : `src\Catamailer.Domain\IMassUpdateProvider.cs`
**Rôle** : Définit le contrat permettant la mise à jour en masse (renommage rétroactif) des catégories sur les e-mails existants.
**Membres et Invocations :**

### Interface : IRuleRepository
**Fichier** : `src\Catamailer.Domain\IRuleRepository.cs`
**Rôle** : Définit le contrat pour l'accès aux données des règles de classification et d'exécution.
**Membres et Invocations :**

### Interface : ISelectionProvider
**Fichier** : `src\Catamailer.Domain\ISelectionProvider.cs`
**Rôle** : Définit le contrat permettant de récupérer l'élément actuellement sélectionné dans le client de messagerie.
**Membres et Invocations :**

### Interface : IShadowModeService
**Fichier** : `src\Catamailer.Domain\IShadowModeService.cs`
**Rôle** : Définit le contrat du service de Shadow Mode et de contrôle des autorisations d'écriture dans Outlook.
**Membres et Invocations :**

### Record : MailMetadata
**Fichier** : `src\Catamailer.Domain\MailMetadata.cs`
**Rôle** : Représente les métadonnées agnostiques extraites d'un e-mail.
**Membres et Invocations :**

### Class : RuleAction
**Fichier** : `src\Catamailer.Domain\RuleAction.cs`
**Rôle** : Représente l'action finale à exécuter si un arbre de conditions est validé.
**Membres et Invocations :**
- `ActionType Type { get; }` : Obtient le type de l'action à exécuter.
- `string Parameter { get; }` : Obtient le paramètre associé à l'action (par exemple, le nom du dossier cible).

### Class : RuleCriterion
**Fichier** : `src\Catamailer.Domain\RuleCriterion.cs`
**Rôle** : Représente un critère de filtrage unitaire dans l'arbre d'exécution de l'Étape 2.
**Membres et Invocations :**
- `MailField Field { get; }` : Obtient le champ de l'e-mail à analyser.
- `MatchOperator Operator { get; }` : Obtient l'opérateur de comparaison.
- `string Value { get; }` : Obtient la valeur cible de la condition.

### Class : RuleNode
**Fichier** : `src\Catamailer.Domain\RuleNode.cs`
**Rôle** : Représente un nœud dans l'arbre composite des conditions d'exécution de l'Étape 2.
**Membres et Invocations :**
- `LogicalOperator Operator { get; }` : Obtient l'opérateur logique liant les enfants de ce nœud.
- `IReadOnlyList<RuleCriterion> Criteria { get; }` : Obtient la liste en lecture seule des critères associés à ce nœud.
- `IReadOnlyList<RuleNode> ChildNodes { get; }` : Obtient la liste en lecture seule des nœuds enfants (sous-arbres).
- `void AddCriterion(RuleCriterion criterion)` : Ajoute un critère de validation unitaire à ce nœud.
- `void AddChildNode(RuleNode childNode)` : Ajoute un nœud enfant permettant d'imbriquer une nouvelle couche logique.

### Class : SystemState
**Fichier** : `src\Catamailer.Domain\SystemState.cs`
**Rôle** : Représente un état système interne (ex: curseur d'avancement).
**Membres et Invocations :**
- `string Key { get; set; }` : La clé unique identifiant l'état (ex: "LastEntryID").
- `string Value { get; set; }` : La valeur associée à l'état.

## Projet : Catamailer.Infrastructure
### Class : AppSettingsRepository
**Fichier** : `src\Catamailer.Infrastructure\AppSettingsRepository.cs`
**Rôle** : Implémentation EF Core pour le dépôt des paramètres d'application.
**Membres et Invocations :**
- `Task<string?> GetSettingAsync(string key)`
- `Task SetSettingAsync(string key, string value)`

### Class : CatamailerDbContext
**Fichier** : `src\Catamailer.Infrastructure\CatamailerDbContext.cs`
**Rôle** : Contexte de base de données principal pour Catamailer (Entity Framework Core SQLite).
**Membres et Invocations :**
- `DbSet<CategoryNode> Categories { get; set; }` : Obtient ou définit la collection des nœuds de catégories.
- `DbSet<SystemState> SystemStates { get; set; }` : Obtient ou définit la collection des états systèmes.
- `DbSet<AppSetting> AppSettings { get; set; }` : Obtient ou définit la collection des paramètres d'application.

### Class : CategoryRepository
**Fichier** : `src\Catamailer.Infrastructure\CategoryRepository.cs`
**Rôle** : Implémentation SQLite du dépôt pour les catégories utilisant Entity Framework Core.
**Membres et Invocations :**
- `Task AddAsync(CategoryNode category)`
- `Task<CategoryNode?> GetByNameAsync(string name)`
- `Task<IEnumerable<CategoryNode>> GetAllAsync()`

### Class : HistoryStateRepository
**Fichier** : `src\Catamailer.Infrastructure\HistoryStateRepository.cs`
**Rôle** : Implémentation EF Core pour le dépôt des états systèmes.
**Membres et Invocations :**
- `Task<string?> GetStateAsync(string key)`
- `Task SetStateAsync(string key, string value)`

### Interface : IOutlookApplicationWrapper
**Fichier** : `src\Catamailer.Infrastructure\IOutlookApplicationWrapper.cs`
**Rôle** : Interface d'abstraction pour l'application COM Outlook, facilitant les tests unitaires.
**Membres et Invocations :**

### Class : OutlookCategoryManagerProvider
**Fichier** : `src\Catamailer.Infrastructure\OutlookCategoryManagerProvider.cs`
**Rôle** : Implémentation du fournisseur de gestion des catégories via l'Interop COM Outlook.
**Membres et Invocations :**
- `void AddCategory(string name, string colorCode)`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.CategoryExists()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.AddCategory()`
- `void UpdateCategoryColor(string name, string newColorCode)`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.CategoryExists()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.UpdateCategory()`
- `void RemoveCategory(string name)`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.CategoryExists()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.RemoveCategory()`

### Class : OutlookMailProvider
**Fichier** : `src\Catamailer.Infrastructure\OutlookMailProvider.cs`
**Rôle** : Implémentation du fournisseur de messagerie basée sur l'Interop COM Outlook.
**Membres et Invocations :**
- `void StartListening()`
- `void StopListening()`

### Class : OutlookMassUpdateProvider
**Fichier** : `src\Catamailer.Infrastructure\OutlookMassUpdateProvider.cs`
**Rôle** : Implémentation du fournisseur de mise à jour en masse via l'Interop COM Outlook.
**Membres et Invocations :**
- `void UpdateCategoryNameOnItems(string oldCategoryName, string newCategoryName)`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.ReplaceCategoryOnAllItems()`

### Class : OutlookSelectionProvider
**Fichier** : `src\Catamailer.Infrastructure\OutlookSelectionProvider.cs`
**Rôle** : Implémentation du fournisseur de sélection basée sur l'Interop COM Outlook.
**Membres et Invocations :**
- `MailMetadata? GetSelectedMail()` : Récupère les métadonnées de l'e-mail actuellement sélectionné. (Le IOutlookApplicationWrapper devra être adapté pour instancier la nouvelle version de MailMetadata).
  - *Appelle* ➡️ `IOutlookApplicationWrapper.GetSelectedEntryId()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.GetMailMetadata()`

### Class : Win32GlobalHotkeyService
**Fichier** : `src\Catamailer.Infrastructure\Win32GlobalHotkeyService.cs`
**Rôle** : Implémentation Win32 du service de raccourcis globaux via P/Invoke (user32.dll et comctl32.dll).
**Membres et Invocations :**
- `void Initialize(IntPtr hwnd)` : Initialise le hook de la boucle de messages native. Doit être appelé une fois la fenêtre WinUI 3 créée.
  - *Appelle* ➡️ `Win32GlobalHotkeyService.SetWindowSubclass()`
- `bool RegisterHotkey(int id, uint modifiers, uint key)`
  - *Appelle* ➡️ `Win32GlobalHotkeyService.RegisterHotKey()`
- `bool UnregisterHotkey(int id)`
  - *Appelle* ➡️ `Win32GlobalHotkeyService.UnregisterHotKey()`
- `void Dispose()`
  - *Appelle* ➡️ `Win32GlobalHotkeyService.UnregisterHotKey()`

## Projet : Catamailer.Migrator
## Projet : Catamailer.UI
### Class : App
**Fichier** : `src\Catamailer.UI\App.xaml.cs`
**Rôle** : Représente l'application principale MAUI. Gère le cycle de vie de la fenêtre, le démarrage en mode furtif (Headless) et les actions globales (Tray Icon).
**Membres et Invocations :**
- `ICommand OpenCommand { get; }`
- `ICommand ExitCommand { get; }`

### Class : DatabaseBootstrapper
**Fichier** : `src\Catamailer.UI\DatabaseBootstrapper.cs`
**Rôle** : Gère l'initialisation de l'infrastructure de base de données.
**Membres et Invocations :**
- `void EnsureDatabaseCreated(IServiceProvider serviceProvider)` : S'assure que le schéma de la base de données est créé.

### Class : DummyCategorySeeder
**Fichier** : `src\Catamailer.UI\Dummies\DummyCategorySeeder.cs`
**Rôle** : Injecte un jeu de catégories factices pour le développement. TODO: À supprimer une fois l'import depuis Outlook implémenté.
**Membres et Invocations :**
- `void SeedDummyCategories(IServiceProvider serviceProvider)` : Injecte les catégories de test si la base est vide.
  - *Appelle* ➡️ `CategoryNode.AddChild()`

### Class : DummyRuleRepository
**Fichier** : `src\Catamailer.UI\Dummies\DummyRuleRepository.cs`
**Rôle** : Dépôt factice pour fournir des règles en l'absence de base de données implémentée pour DictionaryRule. TODO: À supprimer une fois le vrai dépôt implémenté.
**Membres et Invocations :**
- `Task<IEnumerable<DictionaryRule>> GetAllDictionaryRulesAsync()`
- `Task AddDictionaryRuleAsync(DictionaryRule rule)`
- `Task DeleteDictionaryRuleAsync(DictionaryRule rule)`

### Class : DummySelectionProvider
**Fichier** : `src\Catamailer.UI\Dummies\DummySelectionProvider.cs`
**Rôle** : Fournisseur factice pour valider l'IHM sans dépendre d'Outlook en phase de développement. TODO: À supprimer une fois l'intégration Outlook finalisée.
**Membres et Invocations :**
- `MailMetadata? GetSelectedMail()`

### Class : MainPage
**Fichier** : `src\Catamailer.UI\MainPage.xaml.cs`
**Rôle** : Page principale hébergeant exclusivement la vue Blazor.
**Membres et Invocations :**

### Class : MauiProgram
**Fichier** : `src\Catamailer.UI\MauiProgram.cs`
**Rôle** : Classe statique responsable de l'amorçage et de la configuration de l'application MAUI Blazor.
**Membres et Invocations :**
- `MauiApp CreateMauiApp()` : Crée et configure l'instance principale de l'application MAUI. Injecte les dépendances Blazor et initialise le composant de zone de notification (Tray Icon).
  - *Appelle* ➡️ `DatabaseBootstrapper.EnsureDatabaseCreated()`
  - *Appelle* ➡️ `DummyCategorySeeder.SeedDummyCategories()`

### Class : App
**Fichier** : `src\Catamailer.UI\Platforms\Windows\App.xaml.cs`
**Rôle** : Provides application-specific behavior to supplement the default Application class.
**Membres et Invocations :**


### Composants Razor
- **CategoryTreeNode** : `src\Catamailer.UI\Components\CategoryTreeNode.razor`
- **CategoryTreeView** : `src\Catamailer.UI\Components\CategoryTreeView.razor`
- **QuickCategorizeModal** : `src\Catamailer.UI\Components\QuickCategorizeModal.razor`
- **QuickRuleBuilderModal** : `src\Catamailer.UI\Components\QuickRuleBuilderModal.razor`
- **Routes** : `src\Catamailer.UI\Components\Routes.razor`
- **_Imports** : `src\Catamailer.UI\Components\_Imports.razor`
- **TestQuickActionsPage** (Route: `/test-quick-actions`) : `src\Catamailer.UI\Dummies\TestQuickActionsPage.razor`
- **MainLayout** : `src\Catamailer.UI\Components\Layout\MainLayout.razor`
- **CategoryEditor** (Route: `/category-editor`) : `src\Catamailer.UI\Components\Pages\CategoryEditor.razor`
- **DictionaryEditor** (Route: `/dictionary-editor`) : `src\Catamailer.UI\Components\Pages\DictionaryEditor.razor`
- **Home** (Route: `/`) : `src\Catamailer.UI\Components\Pages\Home.razor`
- **NotFound** (Route: `/not-found`) : `src\Catamailer.UI\Components\Pages\NotFound.razor`

## Projet : AiDocGenerator
### Class : CSharpAnalyzer
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\CSharpAnalyzer.cs`
**Membres et Invocations :**
- `Task<string> AnalyzeProjectAsync(Project project, string solutionDir)`
  - *Appelle* ➡️ `CSharpAnalyzer.GetXmlSummary()`

### Class : CssComponentInfo
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\CssAnalyzer.cs`
**Membres et Invocations :**
- `string RelativePath { get; set; }`
- `IEnumerable<string> Classes { get; set; }`

### Class : CssAnalyzer
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\CssAnalyzer.cs`
**Membres et Invocations :**
- `CssComponentInfo ExtractCssInfo(string fileContent, string absoluteFilePath, string solutionDir)`

### Class : ScriptParameter
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\PowerShellAnalyzer.cs`
**Rôle** : Représente un paramètre extrait d'un script PowerShell.
**Membres et Invocations :**
- `string Name { get; set; }` : Obtient ou définit le nom du paramètre (sans le $).
- `string Type { get; set; }` : Obtient ou définit le type du paramètre (ex: string, int).
- `bool IsMandatory { get; set; }` : Indique si le paramètre est obligatoire (Mandatory=$true).

### Class : PowerShellScriptInfo
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\PowerShellAnalyzer.cs`
**Rôle** : Contient les métadonnées et dépendances extraites d'un fichier script PowerShell (.ps1).
**Membres et Invocations :**
- `string ScriptName { get; set; }` : Obtient ou définit le nom du fichier script.
- `string RelativePath { get; set; }` : Obtient ou définit le chemin relatif du script par rapport à la racine de la solution.
- `List<ScriptParameter> Parameters { get; set; }` : Obtient la liste des paramètres déclarés dans le bloc param() du script.
- `List<string> CalledScripts { get; set; }` : Obtient la liste des noms des autres scripts PowerShell invoqués par ce script.

### Class : PowerShellAnalyzer
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\PowerShellAnalyzer.cs`
**Rôle** : Analyseur statique chargé de parser le contenu des fichiers PowerShell (.ps1) pour extraire l'arbre des invocations et les paramètres via des expressions régulières.
**Membres et Invocations :**
- `PowerShellScriptInfo ExtractScriptInfo(string fileContent, string absoluteFilePath, string solutionDir)` : Extrait les informations d'un script PowerShell à partir de son contenu texte.

### Class : RazorComponentInfo
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\RazorAnalyzer.cs`
**Membres et Invocations :**
- `string? Route { get; set; }`
- `string RelativePath { get; set; }`

### Class : RazorAnalyzer
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\RazorAnalyzer.cs`
**Membres et Invocations :**
- `RazorComponentInfo ExtractComponentInfo(string fileContent, string absoluteFilePath, string solutionDir)`

### Class : TestDetail
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\TestResultsAnalyzer.cs`
**Membres et Invocations :**
- `string Name { get; set; }`
- `string Outcome { get; set; }`
- `string Duration { get; set; }`

### Class : TestRunSummary
**Fichier** : `src\Tools\AiDocGenerator\Analyzers\TestResultsAnalyzer.cs`
**Membres et Invocations :**
- `int Total { get; set; }`
- `int Passed { get; set; }`
- `int Failed { get; set; }`
- `string? ErrorMessage { get; set; }`
- `List<TestDetail> AllTests { get; set; }`

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
  - *Appelle* ➡️ `ClassificationEngineTests.CreateDummyMetadata()`
  - *Appelle* ➡️ `ClassificationEngine.Classify()`
- `void Classify_ShouldReturnCategoryAndAscendanceChain_WhenSubjectKeywordMatches()`
  - *Appelle* ➡️ `CategoryNode.AddChild()`
  - *Appelle* ➡️ `ClassificationEngineTests.CreateDummyMetadata()`
  - *Appelle* ➡️ `ClassificationEngine.Classify()`
- `void Classify_ShouldReturnCategory_WhenKeywordMatchesRecipient()`
  - *Appelle* ➡️ `ClassificationEngineTests.CreateDummyMetadata()`
  - *Appelle* ➡️ `ClassificationEngine.Classify()`

### Class : DebounceServiceTests
**Fichier** : `tests\Catamailer.Application.Tests\DebounceServiceTests.cs`
**Rôle** : Classe de tests validant le comportement du service de Debounce de l'analyse d'historique.
**Membres et Invocations :**
- `Task IsAnalysisSuspendedAsync_ShouldReturnFalse_WhenNoSuspensionRecorded()`
  - *Appelle* ➡️ `IHistoryStateRepository.GetStateAsync()`
  - *Appelle* ➡️ `DebounceService.IsAnalysisSuspendedAsync()`
- `Task SuspendAnalysisAsync_ShouldSaveTargetTimeBasedOnConfiguredDelay()`
  - *Appelle* ➡️ `IAppSettingsRepository.GetSettingAsync()`
  - *Appelle* ➡️ `IHistoryStateRepository.SetStateAsync()`
  - *Appelle* ➡️ `DebounceService.SuspendAnalysisAsync()`
- `Task IsAnalysisSuspendedAsync_ShouldReturnFalse_WhenTimeHasPassed()`
  - *Appelle* ➡️ `IHistoryStateRepository.GetStateAsync()`
  - *Appelle* ➡️ `DebounceService.IsAnalysisSuspendedAsync()`

### Class : ExecutionEngineTests
**Fichier** : `tests\Catamailer.Application.Tests\ExecutionEngineTests.cs`
**Rôle** : Classe de test validant le comportement du moteur d'exécution (Étape 2).
**Membres et Invocations :**
- `void Evaluate_ShouldReturnFalse_WhenNodeIsEmpty()`
  - *Appelle* ➡️ `ExecutionEngine.Evaluate()`
- `void Evaluate_ShouldReturnTrue_WhenSenderMatchesEqualsCriterion()`
  - *Appelle* ➡️ `RuleNode.AddCriterion()`
  - *Appelle* ➡️ `ExecutionEngine.Evaluate()`
- `void Evaluate_ShouldReturnFalse_WhenSenderDoesNotMatchEqualsCriterion()`
  - *Appelle* ➡️ `RuleNode.AddCriterion()`
  - *Appelle* ➡️ `ExecutionEngine.Evaluate()`
- `void Evaluate_ShouldReturnTrue_WhenSubjectMatchesContainsCriterion()`
  - *Appelle* ➡️ `RuleNode.AddCriterion()`
  - *Appelle* ➡️ `ExecutionEngine.Evaluate()`
- `void Evaluate_ShouldReturnTrue_WhenOrNodeHasOneValidCriterion()`
  - *Appelle* ➡️ `RuleNode.AddCriterion()`
  - *Appelle* ➡️ `ExecutionEngine.Evaluate()`

### Class : HistoryRunnerTests
**Fichier** : `tests\Catamailer.Application.Tests\HistoryRunnerTests.cs`
**Rôle** : Classe de tests unitaires pour la tâche J2-S2-T3 (HistoryRunner).
**Membres et Invocations :**
- `Task ProcessPendingHistoryAsync_ShouldReturnZero_WhenAnalysisIsSuspended()`
  - *Appelle* ➡️ `IDebounceService.IsAnalysisSuspendedAsync()`
  - *Appelle* ➡️ `HistoryRunner.ProcessPendingHistoryAsync()`
  - *Appelle* ➡️ `IHistoryStateRepository.GetStateAsync()`
- `Task ProcessPendingHistoryAsync_ShouldFetchCursorAndProcess_WhenNotSuspended()`
  - *Appelle* ➡️ `IDebounceService.IsAnalysisSuspendedAsync()`
  - *Appelle* ➡️ `IHistoryStateRepository.GetStateAsync()`
  - *Appelle* ➡️ `HistoryRunner.ProcessPendingHistoryAsync()`

### Class : ShadowModeServiceTests
**Fichier** : `tests\Catamailer.Application.Tests\ShadowModeServiceTests.cs`
**Rôle** : Tests unitaires pour le service ShadowModeService.
**Membres et Invocations :**
- `Task IsAutoWriteEnabledAsync_ShouldReturnFalse_ByDefault()`
  - *Appelle* ➡️ `IAppSettingsRepository.GetSettingAsync()`
  - *Appelle* ➡️ `ShadowModeService.IsAutoWriteEnabledAsync()`
- `Task ShouldPromptForActivationAsync_ShouldReturnTrue_WhenPalierReached()`
  - *Appelle* ➡️ `IAppSettingsRepository.GetSettingAsync()`
  - *Appelle* ➡️ `IHistoryStateRepository.GetStateAsync()`
  - *Appelle* ➡️ `ShadowModeService.ShouldPromptForActivationAsync()`
- `Task ShouldPromptForActivationAsync_ShouldReturnFalse_WhenPromptAlreadyAcknowledgedForCurrentPalier()`
  - *Appelle* ➡️ `IAppSettingsRepository.GetSettingAsync()`
  - *Appelle* ➡️ `IHistoryStateRepository.GetStateAsync()`
  - *Appelle* ➡️ `ShadowModeService.ShouldPromptForActivationAsync()`
- `Task GetTelemetryStatsAsync_ShouldReturnParsedValues_FromRepositories()`
  - *Appelle* ➡️ `IAppSettingsRepository.GetSettingAsync()`
  - *Appelle* ➡️ `IHistoryStateRepository.GetStateAsync()`
  - *Appelle* ➡️ `ShadowModeService.GetTelemetryStatsAsync()`

### Class : CategoryTreeViewModelTests
**Fichier** : `tests\Catamailer.Application.Tests\ViewModels\CategoryTreeViewModelTests.cs`
**Rôle** : Tests unitaires pour la classe CategoryTreeViewModel.
**Membres et Invocations :**
- `Task InitializeAsync_ShouldLoadOnlyRootCategories()`
  - *Appelle* ➡️ `CategoryNode.AddChild()`
  - *Appelle* ➡️ `ICategoryRepository.GetAllAsync()`
  - *Appelle* ➡️ `CategoryTreeViewModel.InitializeAsync()`

### Class : DictionaryEditorViewModelTests
**Fichier** : `tests\Catamailer.Application.Tests\ViewModels\DictionaryEditorViewModelTests.cs`
**Membres et Invocations :**
- `Task InitializeAsync_ShouldLoadDictionaryRules_FromRepository()`
  - *Appelle* ➡️ `DictionaryEditorViewModel.InitializeAsync()`
- `Task AddRuleAsync_ShouldCallRepository_AndRefreshList()`
  - *Appelle* ➡️ `DictionaryEditorViewModel.AddRuleAsync()`
- `Task DeleteRuleAsync_ShouldCallRepository_AndRefreshList()`
  - *Appelle* ➡️ `DictionaryEditorViewModel.InitializeAsync()`
  - *Appelle* ➡️ `DictionaryEditorViewModel.DeleteRuleAsync()`
- `Task InitializeAsync_ShouldLoadCategories_FromCategoryRepository()`
  - *Appelle* ➡️ `DictionaryEditorViewModel.InitializeAsync()`
- `Task CreateRuleFromFormAsync_ShouldAddRule_AndClearForm_WhenValid()`
  - *Appelle* ➡️ `DictionaryEditorViewModel.InitializeAsync()`
  - *Appelle* ➡️ `DictionaryEditorViewModel.CreateRuleFromFormAsync()`

### Class : QuickCategorizeViewModelTests
**Fichier** : `tests\Catamailer.Application.Tests\ViewModels\QuickCategorizeViewModelTests.cs`
**Rôle** : Tests unitaires validant la logique de recherche et de tri de la modale Quick Categorize.
**Membres et Invocations :**
- `Task UpdateSearchAsync_ShouldFilterCategories_IgnoringCase()`
  - *Appelle* ➡️ `QuickCategorizeViewModel.InitializeAsync()`
  - *Appelle* ➡️ `QuickCategorizeViewModel.UpdateSearchAsync()`
- `Task UpdateSearchAsync_ShouldReturnEmpty_WhenNoMatchFound()`
  - *Appelle* ➡️ `QuickCategorizeViewModel.InitializeAsync()`
  - *Appelle* ➡️ `QuickCategorizeViewModel.UpdateSearchAsync()`

### Class : QuickRuleBuilderViewModelTests
**Fichier** : `tests\Catamailer.Application.Tests\ViewModels\QuickRuleBuilderViewModelTests.cs`
**Membres et Invocations :**
- `Task InitializeAsync_ShouldPopulateSelectableOptions_FromSelectedMail()`
  - *Appelle* ➡️ `ISelectionProvider.GetSelectedMail()`
  - *Appelle* ➡️ `IRuleRepository.GetAllDictionaryRulesAsync()`
  - *Appelle* ➡️ `QuickRuleBuilderViewModelTests.CreateViewModel()`
  - *Appelle* ➡️ `QuickRuleBuilderViewModel.InitializeAsync()`
- `Task InitializeAsync_ShouldIdentifyTriggeredCategories_WhenRulesMatch()`
  - *Appelle* ➡️ `ISelectionProvider.GetSelectedMail()`
  - *Appelle* ➡️ `IRuleRepository.GetAllDictionaryRulesAsync()`
  - *Appelle* ➡️ `QuickRuleBuilderViewModelTests.CreateViewModel()`
  - *Appelle* ➡️ `QuickRuleBuilderViewModel.InitializeAsync()`
- `Task BuildRule_ShouldOnlyIncludeSelectedOptions()`
  - *Appelle* ➡️ `ISelectionProvider.GetSelectedMail()`
  - *Appelle* ➡️ `IRuleRepository.GetAllDictionaryRulesAsync()`
  - *Appelle* ➡️ `QuickRuleBuilderViewModelTests.CreateViewModel()`
  - *Appelle* ➡️ `QuickRuleBuilderViewModel.InitializeAsync()`
  - *Appelle* ➡️ `QuickRuleBuilderViewModel.SelectCategory()`
  - *Appelle* ➡️ `QuickRuleBuilderViewModel.BuildRule()`

### Class : ShadowModeDashboardViewModelTests
**Fichier** : `tests\Catamailer.Application.Tests\ViewModels\ShadowModeDashboardViewModelTests.cs`
**Rôle** : Tests unitaires pour la classe ShadowModeDashboardViewModel.
**Membres et Invocations :**
- `Task InitializeAsync_ShouldLoadTelemetryStats_FromService()`
  - *Appelle* ➡️ `IShadowModeService.GetTelemetryStatsAsync()`
  - *Appelle* ➡️ `ShadowModeDashboardViewModel.InitializeAsync()`
- `Task ToggleAutoWriteAsync_ShouldUpdateService_AndRefreshStats()`
  - *Appelle* ➡️ `IShadowModeService.GetTelemetryStatsAsync()`
  - *Appelle* ➡️ `ShadowModeDashboardViewModel.InitializeAsync()`
  - *Appelle* ➡️ `ShadowModeDashboardViewModel.ToggleAutoWriteAsync()`
  - *Appelle* ➡️ `IShadowModeService.SetAutoWriteEnabledAsync()`

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

### Class : OutlookCategoryManagerProviderTests
**Fichier** : `tests\Catamailer.Infrastructure.Tests\OutlookCategoryManagerProviderTests.cs`
**Rôle** : Classe de tests validant le comportement du gestionnaire de catégories Outlook.
**Membres et Invocations :**
- `void AddCategory_ShouldCallWrapperAdd_WhenCategoryDoesNotExist()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.CategoryExists()`
  - *Appelle* ➡️ `OutlookCategoryManagerProvider.AddCategory()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.AddCategory()`
- `void UpdateCategoryColor_ShouldCallWrapperUpdate_WhenCategoryExists()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.CategoryExists()`
  - *Appelle* ➡️ `OutlookCategoryManagerProvider.UpdateCategoryColor()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.UpdateCategory()`
- `void RemoveCategory_ShouldCallWrapperRemove_WhenCategoryExists()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.CategoryExists()`
  - *Appelle* ➡️ `OutlookCategoryManagerProvider.RemoveCategory()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.RemoveCategory()`

### Class : OutlookMailProviderTests
**Fichier** : `tests\Catamailer.Infrastructure.Tests\OutlookMailProviderTests.cs`
**Rôle** : Classe de tests validant le comportement du fournisseur Outlook COM.
**Membres et Invocations :**
- `void StartListening_ShouldTriggerNewMailReceived_WhenOutlookRaisesNewMailEx()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.GetMailMetadata()`
  - *Appelle* ➡️ `OutlookMailProvider.StartListening()`

### Class : OutlookMassUpdateProviderTests
**Fichier** : `tests\Catamailer.Infrastructure.Tests\OutlookMassUpdateProviderTests.cs`
**Rôle** : Classe de tests validant le comportement du fournisseur de mise à jour en masse Outlook.
**Membres et Invocations :**
- `void UpdateCategoryNameOnItems_ShouldCallWrapperReplaceCategory()`
  - *Appelle* ➡️ `OutlookMassUpdateProvider.UpdateCategoryNameOnItems()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.ReplaceCategoryOnAllItems()`

### Class : OutlookSelectionProviderTests
**Fichier** : `tests\Catamailer.Infrastructure.Tests\OutlookSelectionProviderTests.cs`
**Rôle** : Classe de tests validant le comportement du fournisseur de sélection Outlook.
**Membres et Invocations :**
- `void GetSelectedMail_ShouldReturnNull_WhenNoMailIsSelected()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.GetSelectedEntryId()`
  - *Appelle* ➡️ `OutlookSelectionProvider.GetSelectedMail()`
- `void GetSelectedMail_ShouldReturnMetadata_WhenMailIsSelected()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.GetSelectedEntryId()`
  - *Appelle* ➡️ `IOutlookApplicationWrapper.GetMailMetadata()`
  - *Appelle* ➡️ `OutlookSelectionProvider.GetSelectedMail()`

### Class : StateAndSettingsRepositoriesTests
**Fichier** : `tests\Catamailer.Infrastructure.Tests\StateAndSettingsRepositoriesTests.cs`
**Rôle** : Classe de tests validant le comportement des dépôts de configuration et d'état.
**Membres et Invocations :**
- `Task HistoryStateRepository_ShouldPersistAndRetrieveState()`
  - *Appelle* ➡️ `StateAndSettingsRepositoriesTests.GetInMemoryContext()`
  - *Appelle* ➡️ `HistoryStateRepository.SetStateAsync()`
  - *Appelle* ➡️ `HistoryStateRepository.GetStateAsync()`
- `Task HistoryStateRepository_ShouldUpdateExistingState()`
  - *Appelle* ➡️ `StateAndSettingsRepositoriesTests.GetInMemoryContext()`
  - *Appelle* ➡️ `HistoryStateRepository.SetStateAsync()`
  - *Appelle* ➡️ `HistoryStateRepository.GetStateAsync()`
- `Task AppSettingsRepository_ShouldPersistAndRetrieveSetting()`
  - *Appelle* ➡️ `StateAndSettingsRepositoriesTests.GetInMemoryContext()`
  - *Appelle* ➡️ `AppSettingsRepository.SetSettingAsync()`
  - *Appelle* ➡️ `AppSettingsRepository.GetSettingAsync()`

### Class : Win32GlobalHotkeyServiceTests
**Fichier** : `tests\Catamailer.Infrastructure.Tests\Win32GlobalHotkeyServiceTests.cs`
**Rôle** : Classe de tests validant le comportement du service de raccourcis globaux Win32.
**Membres et Invocations :**
- `void RegisterHotkey_StateShouldReflectRegistrationAttempt()`
  - *Appelle* ➡️ `Win32GlobalHotkeyService.RegisterHotkey()`
  - *Appelle* ➡️ `Win32GlobalHotkeyService.UnregisterHotkey()`
- `void UnregisterHotkey_ShouldReturnFalse_WhenHotkeyDoesNotExist()`
  - *Appelle* ➡️ `Win32GlobalHotkeyService.UnregisterHotkey()`

## Projet : Tools.AiDocGenerator.Tests
### Class : CatamailerTocBuilderTests
**Fichier** : `tests\Tools.AiDocGenerator.Tests\CatamailerTocBuilderTests.cs`
**Membres et Invocations :**
- `void Build_ShouldReturnCatamailerSpecificToc()` : Vérifie que le TOC généré est spécifiquement adapté à l'architecture de Catamailer et ne contient plus aucune référence à l'ancien projet Sudoku.
  - *Appelle* ➡️ `CatamailerTocBuilder.Build()`

### Class : CSharpAnalyzerTests
**Fichier** : `tests\Tools.AiDocGenerator.Tests\CSharpAnalyzerTests.cs`
**Membres et Invocations :**
- `Task AnalyzeProjectAsync_ShouldExtractPublicProperties()`
  - *Appelle* ➡️ `CSharpAnalyzer.AnalyzeProjectAsync()`

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

