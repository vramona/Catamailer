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
- [x] ✅ **J1-S1-T1** - Modélisation `CategoryNode` (Arbre auto-référencé et héritage de couleurs). (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 6d50da818d2c0044ff9a3fbaf9cfed37724c4d38) (Cpx: 3 / 3)
- [x] ✅ **J1-S1-T2** - Modélisation Étape 1 & Étape 2 (`DictionaryRule`, `RuleNode`, `RuleAction`). (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 2cf32ba2f8e288abfc13c6de1c2fb10daa1ea146) (Cpx: 5 / 5)
- [x] ✅ **J1-S1-T3** - DbContext SQLite (Création auto) et Repositories. (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 1c3bba4f53bf603ecf5c749ce154869240b5ba06) (Cpx: 5 / 5)

### Step 2 - Moteurs d'Évaluation (TDD) - feature/j1-core-engine_step-2-evaluation
- [x] ✅ **J1-S2-T1** - `ClassificationEngine` : Déduction de la catégorie et extraction de la chaîne d'ascendance. (Validé le 07/09/2026 sur feature/j1-core-engine_step-2-evaluation - Commit : 4fd96526c05075f974eac1fcfc0ef7b0a1915478) (Cpx: 5 / 5)
- [x] ✅ **J1-S2-T2** - `ExecutionEngine` : Évaluation de l'arbre booléen. (Validé le 07/09/2026 sur feature/j1-core-engine_step-2-evaluation - Commit : 1e41888639adabd3622a2c66a4aa6236577d9ad3) (Cpx: 7 / 7)

## Jalon 2 - Outlook COM & Shadow Mode - feature/j2-outlook-integration
### Step 1 - Interop & Synchronisation - feature/j2-outlook-integration_step-1-com
- [x] ✅ **J2-S1-T1** - Abstraction `IMailProvider` et écoute asynchrone (`NewMailEx`). (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-1-com - Commit : a35093bdb4c674b551c5396fe728f0235bc30f3e) (Cpx: 5 / 5)
- [x] ✅ **J2-S1-T2** - `SelectionProvider` : Récupération de l'élément sélectionné dans l'UI Outlook. (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-1-com - Commit : ba7ecce2742edd503c7c7d4d823550ef5cafb4a0) (Cpx: 3 / 3)
- [x] ✅ **J2-S1-T3** - `CategoryManagerProvider` : Synchronisation CRUD avec la Master Category List. (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-1-com - Commit : 12db1d65cd4d200dad3cfca53174282adf42f677) (Cpx: 5 / 5)
- [x] ✅ **J2-S1-T4** - `MassUpdateService` : Renommage rétroactif des e-mails. (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-1-com - Commit : 00edec7a319adf68780fa47c75f4785839687971) (Cpx: 8 / 8)

### Step 2 - Reprise & Shadow Mode - feature/j2-outlook-integration_step-2-history
- [x] ✅ **J2-S2-T1** - Modélisation des paramètres globaux (`IAppSettingsRepository`) et du curseur d'état (`IHistoryStateRepository`). (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : b6194c5f4fc39f2edf9e79815166edcfa6cd3891) (Cpx: 5 / 5)
- [x] ✅ **J2-S2-T2** - `DebounceService` : Suspension de l'analyse d'historique basée sur la configuration globale. (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : 87393971e1ad67d6c022fb3e98ef2c84080e1197) (Cpx: 5 / 5)
- [x] ✅ **J2-S2-T3** - Service Background de Reprise (`HistoryRunner`) exploitant le `LastEntryID`. (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : fa156733005656cd2e6669fbd1ae244822600e9e) (Cpx: 5 / 5)
- [x] ✅ **J2-S2-T4** - `ShadowModeService` : Comptage de télémétrie, gestion de l'activation globale `IsAutoWriteEnabled` et paliers de notification (300/600/900...). (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : 8263a638c0d82f23d2cdbdfa8dda8cbf44385aed) (Cpx: 8 / 8)

## Jalon 3 - Intégration Système et IHM - feature/j3-tray-ui
### Step 1 : Application Hôte et Zone de Notification - feature/j3-tray-ui_step-1-host
- ✅ **J3-S1-T1 - Démarrage Headless et Tray Icon de base** : Configurer le projet MAUI pour démarrer de façon invisible (Headless) et afficher une icône native avec `H.NotifyIcon` dans la zone de notification Windows. Ajout d'un menu contextuel basique (Ouvrir / Quitter) via des Commandes MVVM (App.xaml). Configuration du profil de lancement (launch.json/tasks.json). (Validé le 08/09/2026 sur feature/j3-tray-ui_step-1-host - Commit : <COMMIT_HASH>) (Cpx: 5 / 5).
- [ ] ⚪ **J3-S1-T2** - `GlobalHotkeyService` (Hook Win32).

### Step 2 - Quick Actions (Modales) - feature/j3-tray-ui_step-2-quickactions
- [ ] ⚪ **J3-S2-T1** - Écran "Quick Categorize" : Autocomplete, Tris intelligents (Récents/Suggestions), Actions (Thread/Sender).
- [ ] ⚪ **J3-S2-T2** - Écran "Quick Rule Builder" : Création pré-remplie d'une règle (Étape 1).

### Step 3 - Configuration Rules (UI) - feature/j3-tray-ui_step-3-config
- [ ] ⚪ **J3-S3-T1** - Dashboard Shadow Mode.
- [ ] ⚪ **J3-S3-T2** - Éditeur de Référentiel (TreeView Catégories).
- [ ] ⚪ **J3-S3-T3** - Éditeur Dictionnaires et Builder Visuel (Étape 2).
- [ ] ⚪ **J3-S3-T4** - Écran de Préférences Générales (Paramétrage du délai de Debounce).
- [ ] ⚪ **J3-S3-T5** - Popup de notification d'activation automatique suite aux paliers de télémétrie.

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