# âœ… CE QUI EST FAIT

## Jalon 0 - Initialisation & Outillage - chore/j0-init
### Step 1 - Workspace & Scripts - chore/j0-init_step-1-setup
- [x] âœ… **J0-S1-T1** - CrÃ©ation de l'arborescence (src, tests, scripts, docs) et de la solution Catamailer.sln avec les projets .NET 10 vides. (ValidÃ© le 07/09/2026 sur master - Commit : 29cea92daaee8cc1011b60a8712282242ed0b0fe) (Cpx: 2 / 2)
- [x] âœ… **J0-S1-T2** - IntÃ©gration et adaptation des scripts PowerShell de flux (Start-Jalon, Start-Step, Commit-Task, etc.). (ValidÃ© le 07/09/2026 sur master - Commit : 29cea92daaee8cc1011b60a8712282242ed0b0fe) (Cpx: 3 / 3)

# â³ CE QUI RESTE Ã€ FAIRE

## Jalon 0 - Initialisation & Outillage - chore/j0-init
### Step 1 - Workspace & Scripts - chore/j0-init_step-1-setup
- [ ] ðŸ—ï¸ **J0-S1-T3** - IntÃ©gration et adaptation du script de gÃ©nÃ©ration de documentation AI (Ai_Architecture.md).
- [ ] ðŸ—ï¸ **J0-S1-T4** - Initialisation physique des fichiers de suivi locaux (ToDoList, Specs).

## Jalon 1 - Core Engine & SQLite - feature/j1-core-engine
### Step 1 - ModÃ©lisation Domain - feature/j1-core-engine_step-1-domain
- [ ] âšª **J1-S1-T1** - ModÃ©lisation `CategoryNode` (Arbre auto-rÃ©fÃ©rencÃ© et hÃ©ritage de couleurs).
- [ ] âšª **J1-S1-T2** - ModÃ©lisation Ã‰tape 1 & Ã‰tape 2 (`DictionaryRule`, `RuleNode`, `RuleAction`).
- [ ] âšª **J1-S1-T3** - DbContext SQLite (CrÃ©ation auto) et Repositories.

### Step 2 - Moteurs d'Ã‰valuation (TDD) - feature/j1-core-engine_step-2-evaluation
- [ ] âšª **J1-S2-T1** - `ClassificationEngine` : DÃ©duction de la catÃ©gorie et extraction de la chaÃ®ne d'ascendance.
- [ ] âšª **J1-S2-T2** - `ExecutionEngine` : Ã‰valuation de l'arbre boolÃ©en.

## Jalon 2 - Outlook COM & Shadow Mode - feature/j2-outlook-integration
### Step 1 - Interop & Synchronisation - feature/j2-outlook-integration_step-1-com
- [ ] âšª **J2-S1-T1** - Abstraction `IMailProvider` et Ã©coute asynchrone (`NewMailEx`).
- [ ] âšª **J2-S1-T2** - `SelectionProvider` : RÃ©cupÃ©ration de l'Ã©lÃ©ment sÃ©lectionnÃ© dans l'UI Outlook.
- [ ] âšª **J2-S1-T3** - `CategoryManagerProvider` : Synchronisation CRUD avec la Master Category List.
- [ ] âšª **J2-S1-T4** - `MassUpdateService` : Renommage rÃ©troactif des e-mails.

### Step 2 - Reprise & Shadow Mode - feature/j2-outlook-integration_step-2-history
- [ ] âšª **J2-S2-T1** - Curseur d'Ã©tat et Service Background de Reprise (`LastEntryID`, Pattern Debounce 30 min).
- [ ] âšª **J2-S2-T2** - `ShadowModeService` : Validation croisÃ©e (C# vs VBA) et TÃ©lÃ©mÃ©trie (Verrou 300/30).

## Jalon 3 - Tray App, Global Hotkeys & UI - feature/j3-tray-ui
### Step 1 - Host & Hotkeys - feature/j3-tray-ui_step-1-host
- [ ] âšª **J3-S1-T1** - Host MAUI Blazor Hybrid (DÃ©marrage masquÃ©) et Tray Icon avec Menu contextuel.
- [ ] âšª **J3-S1-T2** - `GlobalHotkeyService` (Hook Win32).

### Step 2 - Quick Actions (Modales) - feature/j3-tray-ui_step-2-quickactions
- [ ] âšª **J3-S2-T1** - Ã‰cran "Quick Categorize" : Autocomplete, Tris intelligents (RÃ©cents/Suggestions), Actions (Thread/Sender).
- [ ] âšª **J3-S2-T2** - Ã‰cran "Quick Rule Builder" : CrÃ©ation prÃ©-remplie d'une rÃ¨gle (Ã‰tape 1).

### Step 3 - Configuration Rules (UI) - feature/j3-tray-ui_step-3-config
- [ ] âšª **J3-S3-T1** - Dashboard Shadow Mode.
- [ ] âšª **J3-S3-T2** - Ã‰diteur de RÃ©fÃ©rentiel (TreeView CatÃ©gories).
- [ ] âšª **J3-S3-T3** - Ã‰diteur Dictionnaires et Builder Visuel (Ã‰tape 2).

## Jalon 4 - Inbox Notes, Agenda & Actions Internes - feature/j4-inbox-notes
### Step 1 - ModÃ©lisation & Agendas - feature/j4-inbox-notes_step-1-core
- [ ] âšª **J4-S1-T1** - ModÃ©lisation `NoteBlock`, `InternalTask`, `CommunicationAction`.
- [ ] âšª **J4-S1-T2** - ImplÃ©mentation `OutlookCalendarProvider` et `GoogleCalendarProvider` (OAuth 2.0).
- [ ] âšª **J4-S1-T3** - Algorithme de matching Temporel (Note Timestamp vs RÃ©union).

### Step 2 - UI Outliner & Quick Insert - feature/j4-inbox-notes_step-2-ui
- [ ] âšª **J4-S2-T1** - Composant Blazor "Outliner" polymorphe (Rendu dynamique Texte/TÃ¢che).
- [ ] âšª **J4-S2-T2** - Ã‰cran "Quick Insert Comms" : Popup sur raccourci global, copie presse-papier et simulation frappe.

## Jalon 5 - Outil de Migration CSV (Standalone) - feature/j5-csv-migrator
- [ ] âšª **J5-S1-T1** - Console App isolÃ©e : Parser CSV, nettoyage et gÃ©nÃ©ration du fichier `rules.db`.
