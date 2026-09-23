# ✅ CE QUI EST FAIT

## Jalon 0 - Initialisation & Outillage - chore/j0-init
### Step 1 - Workspace & Scripts - chore/j0-init_step-1-setup
- [x] ✅ **J0-S1-T1** - Création de l'arborescence (src, tests, scripts, docs) et de la solution Catamailer.sln avec les projets .NET 10 vides. (Validé le 07/09/2026 sur master - Commit : 29cea92daaee8cc1011b60a8712282242ed0b0fe) (Cpx: 2 / 2)
- [x] ✅ **J0-S1-T2** - Intégration et adaptation des scripts PowerShell de flux (Start-Jalon, Start-Step, Commit-Task, etc.). (Validé le 07/09/2026 sur master - Commit : 29cea92daaee8cc1011b60a8712282242ed0b0fe) (Cpx: 3 / 3)
- [x] ✅ **J0-S1-T3** - Intégration et adaptation du script de génération de documentation AI (Ai_Architecture.md). (Validé le 07/09/2026 sur master - Commit : 26220b45c1709946ab997143e2a46159e24d948d) (Cpx: 3 / 3)
- [x] ✅ **J0-S1-T4** - Initialisation physique des fichiers de suivi locaux (ToDoList, Specs). (Validé le 07/09/2026 sur master - Commit : 26220b45c1709946ab997143e2a46159e24d948d) (Cpx: 1 / 1)

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
- [x] ✅ **J3-S1-T1** - Démarrage Headless et Tray Icon de base. (Validé le 08/09/2026 sur feature/j3-tray-ui_step-1-host - Commit : c7161baebfa314a65a8d7fef263e91a6583e8804) (Cpx: 5 / 5).
- [x] ✅ **J3-S1-T2** - GlobalHotkeyService (Hook Win32). (Validé le 08/09/2026 sur feature/j3-tray-ui_step-1-host - Commit : 4a5b88b8e70c9201a863d0e9ac33c541ea287541) (Cpx: 5 / 5).

### Step 2 - Quick Actions (Modales) - feature/j3-tray-ui_step-2-quickactions
- [x] ✅ **J3-S2-T1** - Écran Quick Categorize. (Validé le 08/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 740d643e59e21240fd8065f2a27d0ab624cced68) (Cpx: 8 / 10).
- [x] ✅ **J3-S2-T2** - Écran "Quick Rule Builder" : Création pré-remplie d'une règle (Étape 1).
- [x] ✅ **J3-S2-T2-ST1** - Refonte Core : Modification de `MailMetadata`, `DictionaryRule`, `ClassificationEngine`, et `OutlookSelectionProvider`. (Validé le 09/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 73be44ce1ca1e95da0a28a6931f56ddd9cdbd57d) (Cpx: 12 / 13).
- [x] ✅ **J3-S2-T2-ST2** - Écran "Quick Rule Builder" : IHM 2 colonnes (Arbre des catégories et cases à cocher des métadonnées). (Validé le 09/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 77ab5b1d1b41156996a84ce536280719680f3526) (Cpx: 8 / 8).

### Step 3 - Configuration Rules (UI) - feature/j3-tray-ui_step-3-config
- [x] ✅ **J3-S3-T0** - Refactoring Initialisation (Extraction DatabaseBootstrapper et DummyCategorySeeder). (Validé le 09/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 8716510d2f4d94ed1122bb8e05ed4c7a8745ecd5) (Cpx: 2 / 2).
- [x] ✅ **J3-S3-T1** - Dashboard Shadow Mode. (Validé le 09/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : d0eead0a537423fd2b38f45da235aed9610bb599) (Cpx: 5 / 5).
- [x] ✅ **J3-S3-T2** - Éditeur de Référentiel (TreeView Catégories). (Validé le 09/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : b44b14a9859fda40c6380608c76881b90588fef9) (Cpx: 8 / 8).
- [x] ✅ **J3-S3-T3** - Éditeur Dictionnaires et Builder Visuel (Étape 2). (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7e4dc91acb58fbe6d2a0008d32f4266bf7523281) (Cpx: 18 / 18).
- [x] ✅ **J3-S3-T3-ST1** - Éditeur de Dictionnaires (Étape 1). (Validé le 11/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 78f75f23e681d1fb024a6cb3f5bcf4f1330cab7b) (Cpx: 5 / 5).
- [x] ✅ **J3-S3-T3-ST2** - Builder Visuel de Règles d'exécution (Étape 2). (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7e4dc91acb58fbe6d2a0008d32f4266bf7523281) (Cpx: 13 / 13).
- [x] ✅ **J3-S3-T4** - Écran de Préférences Générales (Paramétrage du délai de Debounce). (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 0c93ff19e4e27e3d3c7febafcf76865e38cc430c) (Cpx: 3 / 3).
- [x] ✅ **J3-S3-T5** - Popup de notification d'activation automatique suite aux paliers de télémétrie. (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 45c96b958cf6ed0db00266dccf13940c86acd88c) (Cpx: 5 / 6).
- [x] ✅ **J3-S3-T6** - Amélioration AiDocGenerator (Extraction des propriétés C#). (Validé le 11/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7adbcda98463c58774e96c9283ca58177944036d) (Cpx: 3 / 3).
- [x] ✅ **J3-S3-T7** - Bouton Quitter Blazor et lien natif. (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 5a84daaabac29c06de35006d94d09afa29fa02dc) (Cpx: 2 / 2).


## Jalon 4 - Synchronisation Référentiel Catégories - feature/j4-category-sync
### Step 1 - Moteur de Détection et Domain - feature/j4-category-sync_step-1-core
- [x] ✅ **J4-S1-T1** - Modélisation des entités de comparaison (SyncResult, CategoryDelta) dans le Domain. (Validé le 15/09/2026 sur feature/j4-category-sync_step-1-core - Commit : c4bb8617b86d6b3921da6408fc012ccc3afe6eef) (Cpx: 3 / 3)
- [x] ✅ **J4-S1-T2** - Implémentation du service de comparaison (Outlook MCL vs SQLite) et détection des orphelins. (Validé le 15/09/2026 sur feature/j4-category-sync_step-1-core - Commit : c9ea11acbebd2e4bbcf7ddc894908c3426fb12be) (Cpx: 10 / 10)

### Step 2 - Interface de Validation au Démarrage - feature/j4-category-sync_step-2-ui
- [x] ✅ **J4-S2-T1** - Composant de synchronisation (Blazor) affichant les catégories manquantes et permettant d'accepter/ignorer l'import. (Validé le 15/09/2026 sur feature/j4-category-sync_step-2-ui - Commit : 446e031435b349db4f35f5769c47cae0bba1155f) (Cpx: 6 / 6)
- [x] ✅ **J4-S2-T2** - Hook au démarrage de l'application MAUI pour déclencher l'analyse de synchronisation en tâche de fond. (Validé le 15/09/2026 sur feature/j4-category-sync_step-2-ui - Commit : 446e031435b349db4f35f5769c47cae0bba1155f) (Cpx: 4 / 4)

### Step 3 - Intégration Infrastructure COM - feature/j4-category-sync_step-3-infra
- [x] ✅ **J4-S3-T1** - Implémentation de la lecture de la Master Category List via COM et suppression des Dummies. (Validé le 16/09/2026 sur feature/j4-category-sync_step-3-infra - Commit : 65fdf358aa1004d3c444ec976e7a4fbf1a771835) (Cpx: 7 / 7)

### Step 4 - Synchronisation Avancée, UI et Tests d'Intégration COM - feature/j4-category-sync_step-4-advanced-sync
- [x] ✅ **J4-S4-T1** - Modification de `CategorySyncService` et du Domain pour structurer les deltas en arbre avec gestion du séparateur dynamique. (Validé le 16/09/2026 sur feature/j4-category-sync_step-4-advanced-sync - Commit : faae02e697a31159296aab3d678f38ee1d9fbaf7) (Cpx: 10 / 10)
- [x] ✅ **J4-S4-T2** - UI : Remplacement de la modale par un écran complet, vue comparée arborescente/dense, fix FullName/Trim et Bubbling IsImplicit. (Validé le 17/09/2026 sur feature/j4-category-sync_step-4-advanced-sync - Commit : 9b2c1b18e71a851c4520652969405adfe598c0df) (Cpx: 18 / 29)
- [x] ✅ **J4-S4-T3** - Infrastructure & Tests : Ajout de `RenameCategory` au Wrapper, création du projet `Catamailer.Infrastructure.IntegrationTests` sur profil PST dédié. (Validé le 22/09/2026 sur feature/j4-category-sync_step-4-advanced-sync - Commit : 30526a52a8e2b7d67f38968ddf46d833a0dc1ff1)
- [x] ✅ **J4-S4-T4** - Bugfix : Reconstruction d'arbre à l'import, Direction des couleurs, Maintien du contexte visuel et auto-guérison SQLite. (Validé le 17/09/2026 sur feature/j4-category-sync_step-4-advanced-sync - Commit : 7855c889fbec1297be4902c5d21b535bf453cfff) (Cpx: 11 / 11)
- [x] ✅ **J4-S4-T6** - Implémentation de la suppression logique (`IsDeleted`) dans le référentiel des catégories et résolution bidirectionnelle. (Validé le 23/09/2026 sur feature/j4-category-sync_step-4-advanced-sync - Commit : cbc82f8d17948ab88d87c8202cbf6700efd05d87) (Cpx: 3 / 4)
- [x] ✅ **J4-S4-T7** - Optimisation des performances de synchronisation (Correction Lenteurs O(N²), Implémentation AddRangeAsync). (Validé le 23/09/2026 sur feature/j4-category-sync_step-4-advanced-sync - Commit : eb52f4cd21ade75da033fde3b67fce0d8b68e20c) (Cpx: 2 / 2)

# ⏳ CE QUI RESTE À FAIRE

## Jalon 5 - Parité VBA et Actions d'Exécution - feature/j5-iso-vba
### Step 1 - Enrichissement Domain et Exécution - feature/j5-iso-vba_step-1-core
- [x] ✅ **J5-S1-T1** - Ajout des ActionType manquantes (Transférer, Importance, Rappel, SuiviAujourdhui, DeplacerDossier, SignatureHTML) dans `Catamailer.Domain`. (Validé le 23/09/2026 sur feature/j5-iso-vba_step-1-core - Commit : 37bfc82ff148806334e15d7d0aa2191ac1e61ad5) (Cpx: 2 / 2)
- [x] ✅ **J5-S1-T2** - Mise à jour de l'UI RuleBuilder pour supporter la saisie des paramètres complexes (ex: adresses e-mail pour transfert, délai en jours, dossier de destination). (Validé le 23/09/2026 sur feature/j5-iso-vba_step-1-core - Commit : 23edee26cedadd7cc9c087868675d8cd58a1fbb9) (Cpx: 3 / 3)

### Step 2 - Implémentation Infrastructure COM - feature/j5-iso-vba_step-2-infra
- [x] ✅ **J5-S2-T1** - Implémentation des actions physiques manquantes via `IOutlookApplicationWrapper`. (Validé le 23/09/2026 sur feature/j5-iso-vba_step-2-infra - Commit : <COMMIT_HASH>) (Cpx: 3 / 3)
- [ ] ⚪ **J5-S2-T2** - Lecture de l'arborescence des dossiers Outlook via COM (`IOutlookApplicationWrapper`).
- [ ] ⚪ **J5-S2-T3** - Lecture des fichiers physiques de signature HTML (%APPDATA%\Microsoft\Signatures).

## Jalon 6 - Outil de Migration CSV (Standalone) - feature/j6-csv-migrator
- [ ] ⚪ **J6-S1-T1** - Console App isolée : Parser le CSV VBA, nettoyer, mapper les nouvelles actions, et générer le fichier `rules.db` SQLite.

## Jalon 7 - Inbox Notes, Agenda & Actions Internes - feature/j7-inbox-notes
### Step 1 - Modélisation & Agendas - feature/j7-inbox-notes_step-1-core
- [ ] ⚪ **J7-S1-T1** - Modélisation `NoteBlock`, `InternalTask`, `CommunicationAction`.
- [ ] ⚪ **J7-S1-T2** - Implémentation `OutlookCalendarProvider` et `GoogleCalendarProvider` (OAuth 2.0).
- [ ] ⚪ **J7-S1-T3** - Algorithme de matching Temporel (Note Timestamp vs Réunion).
### Step 2 - UI Outliner & Quick Insert - feature/j7-inbox-notes_step-2-ui
- [ ] ⚪ **J7-S2-T1** - Composant Blazor "Outliner" polymorphe (Rendu dynamique Texte/Tâche).
- [ ] ⚪ **J7-S2-T2** - Écran "Quick Insert Comms" : Popup sur raccourci global, copie presse-papier et simulation frappe.