# ✅ CE QUI EST FAIT

## Jalon 0 - Initialisation & Outillage - chore/j0-init
### Step 1 - Workspace & Scripts - chore/j0-init_step-1-setup
- [x] ✅ **J0-S1-T1** - Création de l'arborescence (src, tests, scripts, docs) et de la solution Catamailer.sln avec les projets .NET 10 vides. (Validé le 07/09/2026 sur master - Commit : 29cea92daaee8cc1011b60a8712282242ed0b0fe) (Cpx: 2 / 2)
- [x] ✅ **J0-S1-T2** - Intégration et adaptation des scripts PowerShell de flux (Start-Jalon, Start-Step, Commit-Task, etc.). (Validé le 07/09/2026 sur master - Commit : 29cea92daaee8cc1011b60a8712282242ed0b0fe) (Cpx: 3 / 3)
- [x] ✅ **J0-S1-T3** - Intégration et adaptation du script de génération de documentation AI (Ai_Architecture.md). (Validé le 07/09/2026 sur master - Commit : 26220b45c1709946ab997143e2a46159e24d948d) (Cpx: 3 / 3)
- [x] ✅ **J0-S1-T4** - Initialisation physique des fichiers de suivi locaux (ToDoList, Specs). (Validé le 07/09/2026 sur master - Commit : 26220b45c1709946ab997143e2a46159e24d948d) (Cpx: 1 / 1)

# ⏳ CE QUI RESTE À FAIRE

## Jalon 1 - Core Engine & SQLite - feature/j1-core-engine
### Step 1 - Modélisation Domain - feature/j1-core-engine_step-1-domain
- [x] ✅ **J1-S1-T1** - Modélisation `CategoryNode` (Arbre auto-référencé et héritage de couleurs). (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : <COMMIT_HASH>) (Cpx: 3 / 3)
- [ ] ⚪ **J1-S1-T2** - Modélisation Étape 1 & Étape 2 (`DictionaryRule`, `RuleNode`, `RuleAction`).
- [ ] ⚪ **J1-S1-T3** - DbContext SQLite (Création auto) et Repositories.

### Step 2 - Moteurs d'Évaluation (TDD) - feature/j1-core-engine_step-2-evaluation
- [ ] ⚪ **J1-S2-T1** - `ClassificationEngine` : Déduction de la catégorie et extraction de la chaîne d'ascendance.
- [ ] ⚪ **J1-S2-T2** - `ExecutionEngine` : Évaluation de l'arbre booléen.

## Jalon 2 - Outlook COM & Shadow Mode - feature/j2-outlook-integration
### Step 1 - Interop & Synchronisation - feature/j2-outlook-integration_step-1-com
- [ ] ⚪ **J2-S1-T1** - Abstraction `IMailProvider` et écoute asynchrone (`NewMailEx`).
- [ ] ⚪ **J2-S1-T2** - `SelectionProvider` : Récupération de l'élément sélectionné dans l'UI Outlook.
- [ ] ⚪ **J2-S1-T3** - `CategoryManagerProvider` : Synchronisation CRUD avec la Master Category List.
- [ ] ⚪ **J2-S1-T4** - `MassUpdateService` : Renommage rétroactif des e-mails.

### Step 2 - Reprise & Shadow Mode - feature/j2-outlook-integration_step-2-history
- [ ] ⚪ **J2-S2-T1** - Curseur d'état et Service Background de Reprise (`LastEntryID`, Pattern Debounce 30 min).
- [ ] ⚪ **J2-S2-T2** - `ShadowModeService` : Validation croisée (C# vs VBA) et Télémétrie (Verrou 300/30).

## Jalon 3 - Tray App, Global Hotkeys & UI - feature/j3-tray-ui
### Step 1 - Host & Hotkeys - feature/j3-tray-ui_step-1-host
- [ ] ⚪ **J3-S1-T1** - Host MAUI Blazor Hybrid (Démarrage masqué) et Tray Icon avec Menu contextuel.
- [ ] ⚪ **J3-S1-T2** - `GlobalHotkeyService` (Hook Win32).

### Step 2 - Quick Actions (Modales) - feature/j3-tray-ui_step-2-quickactions
- [ ] ⚪ **J3-S2-T1** - Écran "Quick Categorize" : Autocomplete, Tris intelligents (Récents/Suggestions), Actions (Thread/Sender).
- [ ] ⚪ **J3-S2-T2** - Écran "Quick Rule Builder" : Création pré-remplie d'une règle (Étape 1).

### Step 3 - Configuration Rules (UI) - feature/j3-tray-ui_step-3-config
- [ ] ⚪ **J3-S3-T1** - Dashboard Shadow Mode.
- [ ] ⚪ **J3-S3-T2** - Éditeur de Référentiel (TreeView Catégories).
- [ ] ⚪ **J3-S3-T3** - Éditeur Dictionnaires et Builder Visuel (Étape 2).

## Jalon 4 - Inbox Notes, Agenda & Actions Internes - feature/j4-inbox-notes
### Step 1 - Modélisation & Agendas - feature/j4-inbox-notes_step-1-core
- [ ] ⚪ **J4-S1-T1** - Modélisation `NoteBlock`, `InternalTask`, `CommunicationAction`.
- [ ] ⚪ **J4-S1-T2** - Implémentation `OutlookCalendarProvider` et `GoogleCalendarProvider` (OAuth 2.0).
- [ ] ⚪ **J4-S1-T3** - Algorithme de matching Temporel (Note Timestamp vs Réunion).

### Step 2 - UI Outliner & Quick Insert - feature/j4-inbox-notes_step-2-ui
- [ ] ⚪ **J4-S2-T1** - Composant Blazor "Outliner" polymorphe (Rendu dynamique Texte/Tâche).
- [ ] ⚪ **J4-S2-T2** - Écran "Quick Insert Comms" : Popup sur raccourci global, copie presse-papier et simulation frappe.

## Jalon 5 - Outil de Migration CSV (Standalone) - feature/j5-csv-migrator
- [ ] ⚪ **J5-S1-T1** - Console App isolée : Parser CSV, nettoyage et génération du fichier `rules.db`.