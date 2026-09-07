# Spécifications Fonctionnelles et Techniques - Catamailer

---

## PARTIE 1 : DÉCOUPAGE FONCTIONNEL ET RÈGLES DE GESTION

### 1. Gestion Référentielle des Catégories (Master Data Management)
Catamailer agit comme la source de vérité absolue pour les catégories Outlook.
*   **Arborescence :** Les catégories sont structurées hiérarchiquement (Familles > Sous-catégories).
*   **Héritage des couleurs :** Une sous-catégorie hérite de la couleur de son parent, sauf surcharge explicite.
*   **Tagging en chaîne (Auto-propagation) :** Appliquer une sous-catégorie (ex: `CCOEN-Voyage`) applique automatiquement toute sa chaîne de parenté (`CCOEN` + `CCOEN-Voyage`). L'affichage dans Outlook reste plat.
*   **Synchronisation Outlook :** Toute modification dans Catamailer (Création, Couleur, Suppression) met à jour la *Master Category List* d'Outlook en temps réel.
*   **Renommage Rétroactif (Mass Update) :** Renommer une catégorie dans l'UI déclenche un traitement de fond recherchant les e-mails historiques avec l'ancien tag pour le remplacer par le nouveau.

### 2. Le Moteur de Traitement (Logique en 2 Étapes)
L'analyse d'un e-mail suit un flux séquentiel strict.
*   **Étape 1 - Classification :**
    *   Confrontation des métadonnées (Sujet, Expéditeur, Destinataires) à des dictionnaires de mots-clés.
    *   *Règle :* Dès qu'une correspondance est trouvée, le système déduit le Nœud de Catégorie principal.
*   **Étape 2 - Exécution :**
    *   À partir de la catégorie déduite, le système évalue un arbre de conditions composite (Opérateurs ET/OU infinis, correspondances exactes, partielles ou Regex).
    *   *Règle :* Si l'arbre est validé, le système détermine les actions physiques (Déplacer vers un dossier, Marquer comme lu, Assurer un suivi).

### 3. Prise de Notes "Inbox", Tâches et Agenda (Module PKM)
Catamailer intègre un module de productivité croisant les notes, l'agenda et les communications.
*   **Saisie au kilomètre (Outliner) :** Interface de notes rapides. Chaque ligne tapée génère un bloc indépendant possédant un horodatage de création/modification. Le rendu visuel est polymorphe (texte, case à cocher, etc.).
*   **Contextualisation Agenda (Unified Calendar) :** Le système lit en tâche de fond les calendriers Outlook et Google Workspace. En fonction de l'horodatage d'un bloc de note, il suggère l'association à la réunion en cours. La note hérite alors des catégories de la réunion.
*   **Actions Internes :** Transformation d'un bloc de note en "Action à faire" (Échéance, Responsable), stockée et suivie uniquement dans Catamailer.
*   **Actions de Communication (Quick Insert) :** Un bloc de note peut cibler une audience. Via un raccourci clavier global (ex: dans Confluence ou un mail), Catamailer affiche une popup, formate l'action, l'injecte par simulation presse-papier (`Ctrl+V`) dans l'application cible, et la clôture.

### 4. Interactions et Interfaces Utilisateur (UI Blazor)
L'application est furtive : elle vit dans la zone de notification (Tray Icon) et réagit aux raccourcis globaux.
*   **Quick Categorize (Raccourci) :** Modale superposée à Outlook. Recherche par saisie libre (Autocomplete) avec rendu en "Chips". Tris dynamiques : Alphabétique, Récents, Suggestions contextuelles. Actions : Appliquer au mail, à la conversation (Thread), à l'expéditeur.
*   **Quick Rule Builder (Raccourci) :** Modale générant instantanément une règle d'Étape 1, pré-remplie avec le contexte de l'e-mail actuellement sélectionné dans Outlook.
*   **Écrans de Configuration :** Éditeur d'arborescence des catégories (Drag & Drop), Grilles des dictionnaires, Builder visuel de l'arbre des actions.

### 5. Shadow Mode, Sécurité et Reprise d'Historique
*   **Shadow Mode (Double Run) :** Le moteur tourne en parallèle de l'ancienne macro Excel. Il évalue le mail, attend, puis compare sa prédiction théorique avec les catégories réellement appliquées par VBA.
*   **Verrou de sécurité et Activation Manuelle :** Catamailer fonctionne en mode lecture seule / Shadow Mode par défaut. Aucune modification n'est appliquée à Outlook tant que l'utilisateur n'a pas activé explicitement l'écriture automatique depuis un menu de préférences.
*   **Alerte d'Activation (Palier 300) :** Dès que la télémétrie enregistre 300 correspondances exactes sur au moins 30 catégories distinctes, l'IHM affiche une popup d'invitation à activer Catamailer. Si l'utilisateur refuse ou ignore l'activation, la notification réapparaît tous les 300 nouveaux messages qualifiés (palier 600, 900, etc.).
*   **Règle de Conflit :** Une classification manuelle par l'utilisateur (mail déjà déplacé) annule l'action de déplacement automatique.
*   **Pattern Debounce Configurable :** Modifier une règle suspend l'analyse d'historique (rattrapage de stock) pendant un délai paramétrable (défaut : 30 minutes), accessible via l'IHM des préférences. Le traitement des e-mails entrants en temps réel n'est pas affecté. Le curseur (`LastEntryID` et `ReceivedTime`) permet ensuite une reprise propre en tâche de fond sans bloquer l'IHM.

---

## PARTIE 2 : PROJECTION DANS LA CLEAN ARCHITECTURE

### 1. Projet `Catamailer.Domain` (Cœur Métier)
*Agnostique. Zéro dépendance. Pattern `Result<T>` absolu.*
*   **Arbre des Catégories :** `CategoryNode`.
*   **Classification & Exécution :** `DictionaryRule`, `RuleNode` (Composite), `RuleCriterion`, `RuleAction`.
*   **Notes & PKM :** `NoteBlock`, `InternalTask`, `CommunicationAction`.
*   **Télémétrie :** `ShadowModeLog`, `UsageStats`.
*   **Interfaces :** `IMailProvider`, `ICategoryProvider`, `ICalendarProvider`, `IGlobalHotkeyService`.

### 2. Projet `Catamailer.Application` (Orchestration)
*   **Engines :** `ClassificationEngine`, `ExecutionEngine`.
*   **PKM Services :** `NoteContextMatcher` (Croise timestamp Note avec créneaux Agenda).
*   **Background Services :** `DebounceService`, `HistoryRunner`, `ShadowModeValidator`.

### 3. Projet `Catamailer.Infrastructure` (Adaptateurs Externes)
*   **Persistance :** `CatamailerDbContext` (EF Core SQLite).
*   **Outlook COM :** `OutlookMailInterceptor` (`NewMailEx`), `OutlookCategoryMaster` (MCL Sync & Mass Update via DASL), `OutlookSelectionTracker`, `OutlookCalendarProvider`.
*   **Google Workspace :** `GoogleCalendarProvider` (OAuth 2.0).
*   **OS :** `Win32GlobalHotkeyService`, `ClipboardSimulator`.

### 4. Projet `Catamailer.UI` (MAUI Blazor Hybrid)
*   **Host :** Démarrage "Headless" (Tray Icon).
*   **Pages (Configuration) :** Dashboard (Shadow Mode logs), `CategoryTreeView.razor`, Dictionary Grids, Rule Builder.
*   **Modales (Quick Actions) :** `QuickCategorizeModal.razor`, `QuickRuleBuilderModal.razor`, `QuickInsertModal.razor`.
*   **PKM :** `InboxOutliner.razor` (Rendu polymorphe des NoteBlocks).

### 5. Projet `Catamailer.Migrator` (Application Console)
*   Parser l'ancien fichier `Configuration_Regles.csv`, ignorer les entrées vides, générer `rules.db`.