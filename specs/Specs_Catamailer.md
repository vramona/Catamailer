# SpÃ©cifications Fonctionnelles et Techniques - Catamailer

---

## PARTIE 1 : DÃ‰COUPAGE FONCTIONNEL ET RÃˆGLES DE GESTION

### 1. Gestion RÃ©fÃ©rentielle des CatÃ©gories (Master Data Management)
Catamailer agit comme la source de vÃ©ritÃ© absolue pour les catÃ©gories Outlook.
*   **Arborescence :** Les catÃ©gories sont structurÃ©es hiÃ©rarchiquement (Familles > Sous-catÃ©gories).
*   **HÃ©ritage des couleurs :** Une sous-catÃ©gorie hÃ©rite de la couleur de son parent, sauf surcharge explicite.
*   **Tagging en chaÃ®ne (Auto-propagation) :** Appliquer une sous-catÃ©gorie (ex: `CCOEN-Voyage`) applique automatiquement toute sa chaÃ®ne de parentÃ© (`CCOEN` + `CCOEN-Voyage`). L'affichage dans Outlook reste plat.
*   **Synchronisation Outlook :** Toute modification dans Catamailer (CrÃ©ation, Couleur, Suppression) met Ã  jour la *Master Category List* d'Outlook en temps rÃ©el.
*   **Renommage RÃ©troactif (Mass Update) :** Renommer une catÃ©gorie dans l'UI dÃ©clenche un traitement de fond recherchant les e-mails historiques avec l'ancien tag pour le remplacer par le nouveau.

### 2. Le Moteur de Traitement (Logique en 2 Ã‰tapes)
L'analyse d'un e-mail suit un flux sÃ©quentiel strict.
*   **Ã‰tape 1 - Classification :**
    *   Confrontation des mÃ©tadonnÃ©es (Sujet, ExpÃ©diteur, Destinataires) Ã  des dictionnaires de mots-clÃ©s.
    *   *RÃ¨gle :* DÃ¨s qu'une correspondance est trouvÃ©e, le systÃ¨me dÃ©duit le NÅ“ud de CatÃ©gorie principal.
*   **Ã‰tape 2 - ExÃ©cution :**
    *   Ã€ partir de la catÃ©gorie dÃ©duite, le systÃ¨me Ã©value un arbre de conditions composite (OpÃ©rateurs ET/OU infinis, correspondances exactes, partielles ou Regex).
    *   *RÃ¨gle :* Si l'arbre est validÃ©, le systÃ¨me dÃ©termine les actions physiques (DÃ©placer vers un dossier, Marquer comme lu, Assurer un suivi).

### 3. Prise de Notes "Inbox", TÃ¢ches et Agenda (Module PKM)
Catamailer intÃ¨gre un module de productivitÃ© croisant les notes, l'agenda et les communications.
*   **Saisie au kilomÃ¨tre (Outliner) :** Interface de notes rapides. Chaque ligne tapÃ©e gÃ©nÃ¨re un bloc indÃ©pendant possÃ©dant un horodatage de crÃ©ation/modification. Le rendu visuel est polymorphe (texte, case Ã  cocher, etc.).
*   **Contextualisation Agenda (Unified Calendar) :** Le systÃ¨me lit en tÃ¢che de fond les calendriers Outlook et Google Workspace. En fonction de l'horodatage d'un bloc de note, il suggÃ¨re l'association Ã  la rÃ©union en cours. La note hÃ©rite alors des catÃ©gories de la rÃ©union.
*   **Actions Internes :** Transformation d'un bloc de note en "Action Ã  faire" (Ã‰chÃ©ance, Responsable), stockÃ©e et suivie uniquement dans Catamailer.
*   **Actions de Communication (Quick Insert) :** Un bloc de note peut cibler une audience. Via un raccourci clavier global (ex: dans Confluence ou un mail), Catamailer affiche une popup, formate l'action, l'injecte par simulation presse-papier (`Ctrl+V`) dans l'application cible, et la clÃ´ture.

### 4. Interactions et Interfaces Utilisateur (UI Blazor)
L'application est furtive : elle vit dans la zone de notification (Tray Icon) et rÃ©agit aux raccourcis globaux.
*   **Quick Categorize (Raccourci) :** Modale superposÃ©e Ã  Outlook. Recherche par saisie libre (Autocomplete) avec rendu en "Chips". Tris dynamiques : AlphabÃ©tique, RÃ©cents, Suggestions contextuelles. Actions : Appliquer au mail, Ã  la conversation (Thread), Ã  l'expÃ©diteur.
*   **Quick Rule Builder (Raccourci) :** Modale gÃ©nÃ©rant instantanÃ©ment une rÃ¨gle d'Ã‰tape 1, prÃ©-remplie avec le contexte de l'e-mail actuellement sÃ©lectionnÃ© dans Outlook.
*   **Ã‰crans de Configuration :** Ã‰diteur d'arborescence des catÃ©gories (Drag & Drop), Grilles des dictionnaires, Builder visuel de l'arbre des actions.

### 5. Shadow Mode, SÃ©curitÃ© et Reprise d'Historique
*   **Shadow Mode (Double Run) :** Le moteur tourne en parallÃ¨le de l'ancienne macro Excel. Il Ã©value le mail, attend, puis compare sa prÃ©diction thÃ©orique avec les catÃ©gories rÃ©ellement appliquÃ©es par VBA.
*   **Verrou de sÃ©curitÃ© :** Catamailer n'Ã©crira pas dans Outlook tant que la tÃ©lÃ©mÃ©trie n'aura pas certifiÃ© 300 correspondances exactes sur 30 catÃ©gories distinctes. (LevÃ©e du verrou par recompilation).
*   **RÃ¨gle de Conflit :** Une classification manuelle par l'utilisateur (mail dÃ©jÃ  dÃ©placÃ©) annule l'action de dÃ©placement automatique.
*   **Pattern Debounce (30 minutes) :** Modifier une rÃ¨gle suspend l'analyse d'historique pendant 30 minutes. Le curseur (`LastEntryID` et `ReceivedTime`) permet ensuite une reprise propre en tÃ¢che de fond sans bloquer l'IHM.

---

## PARTIE 2 : PROJECTION DANS LA CLEAN ARCHITECTURE

### 1. Projet `Catamailer.Domain` (CÅ“ur MÃ©tier)
*Agnostique. ZÃ©ro dÃ©pendance. Pattern `Result<T>` absolu.*
*   **Arbre des CatÃ©gories :** `CategoryNode`.
*   **Classification & ExÃ©cution :** `DictionaryRule`, `RuleNode` (Composite), `RuleCriterion`, `RuleAction`.
*   **Notes & PKM :** `NoteBlock`, `InternalTask`, `CommunicationAction`.
*   **TÃ©lÃ©mÃ©trie :** `ShadowModeLog`, `UsageStats`.
*   **Interfaces :** `IMailProvider`, `ICategoryProvider`, `ICalendarProvider`, `IGlobalHotkeyService`.

### 2. Projet `Catamailer.Application` (Orchestration)
*   **Engines :** `ClassificationEngine`, `ExecutionEngine`.
*   **PKM Services :** `NoteContextMatcher` (Croise timestamp Note avec crÃ©neaux Agenda).
*   **Background Services :** `DebounceService`, `HistoryRunner`, `ShadowModeValidator`.

### 3. Projet `Catamailer.Infrastructure` (Adaptateurs Externes)
*   **Persistance :** `CatamailerDbContext` (EF Core SQLite).
*   **Outlook COM :** `OutlookMailInterceptor` (`NewMailEx`), `OutlookCategoryMaster` (MCL Sync & Mass Update via DASL), `OutlookSelectionTracker`, `OutlookCalendarProvider`.
*   **Google Workspace :** `GoogleCalendarProvider` (OAuth 2.0).
*   **OS :** `Win32GlobalHotkeyService`, `ClipboardSimulator`.

### 4. Projet `Catamailer.UI` (MAUI Blazor Hybrid)
*   **Host :** DÃ©marrage "Headless" (Tray Icon).
*   **Pages (Configuration) :** Dashboard (Shadow Mode logs), `CategoryTreeView.razor`, Dictionary Grids, Rule Builder.
*   **Modales (Quick Actions) :** `QuickCategorizeModal.razor`, `QuickRuleBuilderModal.razor`, `QuickInsertModal.razor`.
*   **PKM :** `InboxOutliner.razor` (Rendu polymorphe des NoteBlocks).

### 5. Projet `Catamailer.Migrator` (Application Console)
*   Parser l'ancien fichier `Configuration_Regles.csv`, ignorer les entrÃ©es vides, gÃ©nÃ©rer `rules.db`.
