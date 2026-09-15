# Spécifications des Tests Unitaires - Catamailer

Ce document référence l'intégralité des tests unitaires (TDD) et d'intégration validant le comportement de la Clean Architecture de Catamailer.

---

## 🟢 JALON 0 - INITIALISATION & OUTILLAGE
### 🟢 J0-S1 : Workspace & Scripts
- [x] ✅ `J0-S1-T3 - Build_ShouldReturnCatamailerSpecificToc` (Validé le 07/09/2026 sur master - Commit : 26220b45c1709946ab997143e2a46159e24d948d) (Cpx: 3 / 3)

---

## 🟢 JALON 1 - CORE ENGINE & SQLITE
### 🟢 J1-S1 : Modélisation Domain
- [x] ✅ `J1-S1-T1 - CategoryNode_Creation_ShouldSetPropertiesCorrectly` (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 6d50da818d2c0044ff9a3fbaf9cfed37724c4d38)
- [x] ✅ `J1-S1-T1 - AddChild_ShouldSetParentAndInheritColor_WhenNoColorSpecified` (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 6d50da818d2c0044ff9a3fbaf9cfed37724c4d38)
- [x] ✅ `J1-S1-T1 - EffectiveColor_ShouldOverrideParentColor_WhenColorIsExplicitlySet` (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 6d50da818d2c0044ff9a3fbaf9cfed37724c4d38)
- [x] ✅ `J1-S1-T1 - GetAscendanceChain_ShouldReturnFullHierarchy_FromRootToNode` (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 6d50da818d2c0044ff9a3fbaf9cfed37724c4d38)
- [x] ✅ `J1-S1-T2 - DictionaryRule_Creation_ShouldSetProperties` (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 2cf32ba2f8e288abfc13c6de1c2fb10daa1ea146)
- [x] ✅ `J1-S1-T2 - RuleAction_Creation_ShouldSetActionTypeAndParameter` (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 2cf32ba2f8e288abfc13c6de1c2fb10daa1ea146)
- [x] ✅ `J1-S1-T2 - RuleCriterion_Creation_ShouldSetConditionFields` (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 2cf32ba2f8e288abfc13c6de1c2fb10daa1ea146)
- [x] ✅ `J1-S1-T2 - RuleNode_ShouldActAsComposite_HoldingCriteriaAndChildNodes` (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 2cf32ba2f8e288abfc13c6de1c2fb10daa1ea146)
- [x] ✅ `J1-S1-T3 - EnsureCreated_ShouldCreateDatabaseAndTables` (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 1c3bba4f53bf603ecf5c749ce154869240b5ba06)
- [x] ✅ `J1-S1-T3 - CanSaveAndRetrieve_CategoryNode` (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 1c3bba4f53bf603ecf5c749ce154869240b5ba06)
- [x] ✅ `J1-S1-T3 - AddAsync_ShouldPersistCategory` (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 1c3bba4f53bf603ecf5c749ce154869240b5ba06)
- [x] ✅ `J1-S1-T3 - GetByNameAsync_ShouldReturnCategory_WhenExists` (Validé le 07/09/2026 sur feature/j1-core-engine_step-1-domain - Commit : 1c3bba4f53bf603ecf5c749ce154869240b5ba06)

### 🟢 J1-S2 : Moteurs d'Évaluation
- [x] ✅ `J1-S2-T1 - Classify_ShouldReturnNull_WhenNoRulesProvided` (Validé le 07/09/2026 sur feature/j1-core-engine_step-2-evaluation - Commit : 4fd96526c05075f974eac1fcfc0ef7b0a1915478)
- [x] ✅ `J1-S2-T1 - Classify_ShouldReturnCategoryAndAscendanceChain_WhenKeywordMatches` (Validé le 07/09/2026 sur feature/j1-core-engine_step-2-evaluation - Commit : 4fd96526c05075f974eac1fcfc0ef7b0a1915478)
- [x] ✅ `J1-S2-T1 - Classify_ShouldReturnCategory_WhenKeywordMatchesRecipient` (Validé le 07/09/2026 sur feature/j1-core-engine_step-2-evaluation - Commit : 4fd96526c05075f974eac1fcfc0ef7b0a1915478)
- [x] ✅ `J1-S2-T2 - Evaluate_ShouldReturnFalse_WhenNodeIsEmpty` (Validé le 07/09/2026 sur feature/j1-core-engine_step-2-evaluation - Commit : 1e41888639adabd3622a2c66a4aa6236577d9ad3)
- [x] ✅ `J1-S2-T2 - Evaluate_ShouldReturnTrue_WhenSenderMatchesEqualsCriterion` (Validé le 07/09/2026 sur feature/j1-core-engine_step-2-evaluation - Commit : 1e41888639adabd3622a2c66a4aa6236577d9ad3)
- [x] ✅ `J1-S2-T2 - Evaluate_ShouldReturnFalse_WhenSenderDoesNotMatchEqualsCriterion` (Validé le 07/09/2026 sur feature/j1-core-engine_step-2-evaluation - Commit : 1e41888639adabd3622a2c66a4aa6236577d9ad3)
- [x] ✅ `J1-S2-T2 - Evaluate_ShouldReturnTrue_WhenSubjectMatchesContainsCriterion` (Validé le 07/09/2026 sur feature/j1-core-engine_step-2-evaluation - Commit : 1e41888639adabd3622a2c66a4aa6236577d9ad3)
- [x] ✅ `J1-S2-T2 - Evaluate_ShouldReturnTrue_WhenOrNodeHasOneValidCriterion` (Validé le 07/09/2026 sur feature/j1-core-engine_step-2-evaluation - Commit : 1e41888639adabd3622a2c66a4aa6236577d9ad3)

---

## 🟢 JALON 2 - OUTLOOK COM & SHADOW MODE
### 🟢 J2-S1 : Interop & Synchronisation

- [x] ✅ `J2-S1-T1 - StartListening_ShouldTriggerNewMailReceived_WhenOutlookRaisesNewMailEx` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-1-com - Commit : a35093bdb4c674b551c5396fe728f0235bc30f3e) (Cpx: 5 / 5)
- [x] ✅ `J2-S1-T2 - GetSelectedMail_ShouldReturnNull_WhenNoMailIsSelected` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-1-com - Commit : ba7ecce2742edd503c7c7d4d823550ef5cafb4a0)
- [x] ✅ `J2-S1-T2 - GetSelectedMail_ShouldReturnMetadata_WhenMailIsSelected` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-1-com - Commit : ba7ecce2742edd503c7c7d4d823550ef5cafb4a0)
- [x] ✅ `J2-S1-T3 - AddCategory_ShouldCallWrapperAdd_WhenCategoryDoesNotExist` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-1-com - Commit : 12db1d65cd4d200dad3cfca53174282adf42f677)
- [x] ✅ `J2-S1-T3 - UpdateCategoryColor_ShouldCallWrapperUpdate_WhenCategoryExists` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-1-com - Commit : 12db1d65cd4d200dad3cfca53174282adf42f677)
- [x] ✅ `J2-S1-T3 - RemoveCategory_ShouldCallWrapperRemove_WhenCategoryExists` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-1-com - Commit : 12db1d65cd4d200dad3cfca53174282adf42f677)
- [x] ✅ `J2-S1-T4 - UpdateCategoryNameOnItems_ShouldCallWrapperReplaceCategory` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-1-com - Commit : 00edec7a319adf68780fa47c75f4785839687971)
- [x] ✅ `J2-S2-T1 - HistoryStateRepository_ShouldPersistAndRetrieveState` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : b6194c5f4fc39f2edf9e79815166edcfa6cd3891)
- [x] ✅ `J2-S2-T1 - HistoryStateRepository_ShouldUpdateExistingState` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : b6194c5f4fc39f2edf9e79815166edcfa6cd3891)
- [x] ✅ `J2-S2-T1 - AppSettingsRepository_ShouldPersistAndRetrieveSetting` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : b6194c5f4fc39f2edf9e79815166edcfa6cd3891)
- [x] ✅ `J2-S2-T2 - IsAnalysisSuspendedAsync_ShouldReturnFalse_WhenNoSuspensionRecorded` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : 87393971e1ad67d6c022fb3e98ef2c84080e1197)
- [x] ✅ `J2-S2-T2 - SuspendAnalysisAsync_ShouldSaveTargetTimeBasedOnConfiguredDelay` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : 87393971e1ad67d6c022fb3e98ef2c84080e1197)
- [x] ✅ `J2-S2-T2 - IsAnalysisSuspendedAsync_ShouldReturnFalse_WhenTimeHasPassed` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : 87393971e1ad67d6c022fb3e98ef2c84080e1197)
- [x] ✅ `J2-S2-T3 - ProcessPendingHistoryAsync_ShouldReturnZero_WhenAnalysisIsSuspended` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : fa156733005656cd2e6669fbd1ae244822600e9e)
- [x] ✅ `J2-S2-T3 - ProcessPendingHistoryAsync_ShouldFetchCursorAndProcess_WhenNotSuspended` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : fa156733005656cd2e6669fbd1ae244822600e9e)
- [x] ✅ `J2-S2-T4 - IsAutoWriteEnabledAsync_ShouldReturnFalse_ByDefault` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : 8263a638c0d82f23d2cdbdfa8dda8cbf44385aed)
- [x] ✅ `J2-S2-T4 - ShouldPromptForActivationAsync_ShouldReturnTrue_WhenPalierReached` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : 8263a638c0d82f23d2cdbdfa8dda8cbf44385aed)
- [x] ✅ `J2-S2-T4 - ShouldPromptForActivationAsync_ShouldReturnFalse_WhenPromptAlreadyAcknowledgedForCurrentPalier` (Validé le 07/09/2026 sur feature/j2-outlook-integration_step-2-history - Commit : 8263a638c0d82f23d2cdbdfa8dda8cbf44385aed)

---

## 🟢 JALON 3 - TRAY APP, GLOBAL HOTKEYS & UI
### 🟢 J3-S1 : Host & Hotkeys
- [x] ✅ `J3-S1-T1 - App_ShouldStartHidden_AndRegisterTrayIcon` (Validé le 08/09/2026 sur feature/j3-tray-ui_step-1-host - Commit : c7161baebfa314a65a8d7fef263e91a6583e8804) (Cpx: 3 / 3)
- [x] ✅ `J3-S1-T2 - RegisterHotkey_ShouldReturnTrue_WhenHotkeyIsAvailable` (Validé le 08/09/2026 sur feature/j3-tray-ui_step-1-host - Commit : 4a5b88b8e70c9201a863d0e9ac33c541ea287541)
- [x] ✅ `J3-S1-T2 - UnregisterHotkey_ShouldReturnFalse_WhenHotkeyDoesNotExist` (Validé le 08/09/2026 sur feature/j3-tray-ui_step-1-host - Commit : 4a5b88b8e70c9201a863d0e9ac33c541ea287541)

### 🟢 J3-S2 : Quick Actions (Modales)
- [x] ✅ `J3-S2-T1 - UpdateSearchAsync_ShouldFilterCategories_IgnoringCase` (Validé le 08/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 740d643e59e21240fd8065f2a27d0ab624cced68)
- [x] ✅ `J3-S2-T1 - UpdateSearchAsync_ShouldReturnEmpty_WhenNoMatchFound` (Validé le 08/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 740d643e59e21240fd8065f2a27d0ab624cced68)
- [x] ✅ `J3-S2-T2-ST1 - DictionaryRule_Creation_ShouldSetProperties` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 73be44ce1ca1e95da0a28a6931f56ddd9cdbd57d)
- [x] ✅ `J3-S2-T2-ST1 - Classify_ShouldReturnNull_WhenNoRulesProvided` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 73be44ce1ca1e95da0a28a6931f56ddd9cdbd57d)
- [x] ✅ `J3-S2-T2-ST1 - Classify_ShouldReturnCategoryAndAscendanceChain_WhenSubjectKeywordMatches` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 73be44ce1ca1e95da0a28a6931f56ddd9cdbd57d)
- [x] ✅ `J3-S2-T2-ST1 - Classify_ShouldReturnCategory_WhenKeywordMatchesRecipient` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 73be44ce1ca1e95da0a28a6931f56ddd9cdbd57d)
- [x] ✅ `J3-S2-T2-ST1 - StartListening_ShouldTriggerNewMailReceived_WhenOutlookRaisesNewMailEx` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 73be44ce1ca1e95da0a28a6931f56ddd9cdbd57d) (Cpx: 5 / 5)
- [x] ✅ `J3-S2-T2-ST1 - GetSelectedMail_ShouldReturnMetadata_WhenMailIsSelected` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 73be44ce1ca1e95da0a28a6931f56ddd9cdbd57d)
- [x] ✅ `J3-S2-T2-ST1 - InitializeAsync_ShouldPopulateFields_FromSelectedMail` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 73be44ce1ca1e95da0a28a6931f56ddd9cdbd57d)
- [x] ✅ `J3-S2-T2-ST1 - InitializeAsync_ShouldLeaveFieldsEmpty_WhenNoMailSelected` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 73be44ce1ca1e95da0a28a6931f56ddd9cdbd57d)
- [x] ✅ `J3-S2-T2-ST1 - BuildRule_ShouldReturnPopulatedDictionaryRule` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 73be44ce1ca1e95da0a28a6931f56ddd9cdbd57d)
- [x] ✅ `J3-S2-T2-ST2 - InitializeAsync_ShouldPopulateSelectableOptions_FromSelectedMail` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 77ab5b1d1b41156996a84ce536280719680f3526)
- [x] ✅ `J3-S2-T2-ST2 - InitializeAsync_ShouldIdentifyTriggeredCategories_WhenRulesMatch` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 77ab5b1d1b41156996a84ce536280719680f3526)
- [x] ✅ `J3-S2-T2-ST2 - BuildRule_ShouldOnlyIncludeSelectedOptions` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-2-quickactions - Commit : 77ab5b1d1b41156996a84ce536280719680f3526)
- [x] ✅ `J3-S3-T4 - InitializeAsync_ShouldLoadDebounceDelay_WhenSettingExists` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 0c93ff19e4e27e3d3c7febafcf76865e38cc430c)
- [x] ✅ `J3-S3-T4 - InitializeAsync_ShouldSetDefaultDebounceDelay_WhenSettingDoesNotExist` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 0c93ff19e4e27e3d3c7febafcf76865e38cc430c)
- [x] ✅ `J3-S3-T4 - SaveAsync_ShouldPersistDebounceDelayToRepository` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 0c93ff19e4e27e3d3c7febafcf76865e38cc430c)
- [x] ✅ `J3-S3-T5 - InitializeAsync_ShouldSetShouldShowPrompt_WhenServiceReturnsTrue` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 45c96b958cf6ed0db00266dccf13940c86acd88c)
- [x] ✅ `J3-S3-T5 - InitializeAsync_ShouldNotSetShouldShowPrompt_WhenServiceReturnsFalse` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 45c96b958cf6ed0db00266dccf13940c86acd88c)
- [x] ✅ `J3-S3-T5 - ActivateAsync_ShouldEnableAutoWrite_AndHidePrompt` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 45c96b958cf6ed0db00266dccf13940c86acd88c)
- [x] ✅ `J3-S3-T5 - AcknowledgeAsync_ShouldAcknowledgePrompt_AndHidePrompt` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 45c96b958cf6ed0db00266dccf13940c86acd88c)
- [x] ✅ `J3-S3-T7 - RequestExit_ShouldRaiseExitRequestedEvent` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 5a84daaabac29c06de35006d94d09afa29fa02dc)

### 🟢 J3-S3 : Configuration Rules (UI)
- [x] ✅ `J3-S3-T1 - GetTelemetryStatsAsync_ShouldReturnParsedValues_FromRepositories` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : d0eead0a537423fd2b38f45da235aed9610bb599)
- [x] ✅ `J3-S3-T1 - InitializeAsync_ShouldLoadTelemetryStats_FromService` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : d0eead0a537423fd2b38f45da235aed9610bb599)
- [x] ✅ `J3-S3-T1 - ToggleAutoWriteAsync_ShouldUpdateService_AndRefreshStats` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : d0eead0a537423fd2b38f45da235aed9610bb599)
- [x] ✅ `J3-S3-T1 - BuildRule_ShouldOnlyIncludeSelectedOptions` (Refacto Async) (Validé le 09/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : d0eead0a537423fd2b38f45da235aed9610bb599)
- [x] ✅ `J3-S3-T2 - InitializeAsync_ShouldLoadOnlyRootCategories` (Validé le 09/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : b44b14a9859fda40c6380608c76881b90588fef9) (Cpx: 8 / 8)
- [x] ✅ `J3-S3-T6 - AnalyzeProjectAsync_ShouldExtractPublicProperties` (Validé le 11/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7adbcda98463c58774e96c9283ca58177944036d) (Cpx: 3 / 3)
- [x] ✅ `J3-S3-T3-ST1 - InitializeAsync_ShouldLoadDictionaryRules_FromRepository` (Validé le 11/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 78f75f23e681d1fb024a6cb3f5bcf4f1330cab7b) (Cpx: 5 / 5)
- [x] ✅ `J3-S3-T3-ST1 - AddRuleAsync_ShouldCallRepository_AndRefreshList` (Validé le 11/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 78f75f23e681d1fb024a6cb3f5bcf4f1330cab7b)
- [x] ✅ `J3-S3-T3-ST1 - DeleteRuleAsync_ShouldCallRepository_AndRefreshList` (Validé le 11/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 78f75f23e681d1fb024a6cb3f5bcf4f1330cab7b)
- [x] ✅ `J3-S3-T3-ST1 - InitializeAsync_ShouldLoadCategories_FromCategoryRepository` (Validé le 11/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 78f75f23e681d1fb024a6cb3f5bcf4f1330cab7b)
- [x] ✅ `J3-S3-T3-ST1 - CreateRuleFromFormAsync_ShouldAddRule_AndClearForm_WhenValid` (Validé le 11/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 78f75f23e681d1fb024a6cb3f5bcf4f1330cab7b)
- [x] ✅ `J3-S3-T3-ST2 - Constructor_ShouldInitializeRootNode` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7e4dc91acb58fbe6d2a0008d32f4266bf7523281)
- [x] ✅ `J3-S3-T3-ST2 - AddCriterion_ShouldAddCriterionToTargetNode` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7e4dc91acb58fbe6d2a0008d32f4266bf7523281)
- [x] ✅ `J3-S3-T3-ST2 - RemoveCriterion_ShouldRemoveCriterionFromTargetNode` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7e4dc91acb58fbe6d2a0008d32f4266bf7523281)
- [x] ✅ `J3-S3-T3-ST2 - AddChildNode_ShouldAddNodeToTargetNode` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7e4dc91acb58fbe6d2a0008d32f4266bf7523281)
- [x] ✅ `J3-S3-T3-ST2 - SetAction_ShouldUpdateFinalActionProperty` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7e4dc91acb58fbe6d2a0008d32f4266bf7523281)
- [x] ✅ `J3-S3-T3-ST2 - SetOperator_ShouldUpdateTargetNodeOperator` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7e4dc91acb58fbe6d2a0008d32f4266bf7523281)
- [x] ✅ `J3-S3-T3-ST2 - UpdateCriterion_ShouldReplaceCriterionInTargetNode` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7e4dc91acb58fbe6d2a0008d32f4266bf7523281)
- [x] ✅ `J3-S3-T3-ST2 - SaveRuleAsync_ShouldCallRepository_WhenNameAndActionAreSet` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7e4dc91acb58fbe6d2a0008d32f4266bf7523281)
- [x] ✅ `J3-S3-T3-ST2 - RuleCriterion_WithCategoryField_AndValidOperator_ShouldBeValid` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7e4dc91acb58fbe6d2a0008d32f4266bf7523281)
- [x] ✅ `J3-S3-T3-ST2 - RuleCriterion_WithInvalidCombinations_ShouldThrowArgumentException` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7e4dc91acb58fbe6d2a0008d32f4266bf7523281)
- [x] ✅ `J3-S3-T3-ST2 - InitializeAsync_ShouldLoadCategories_FromCategoryRepository` (Validé le 15/09/2026 sur feature/j3-tray-ui_step-3-config - Commit : 7e4dc91acb58fbe6d2a0008d32f4266bf7523281)

---

## 🟢 JALON 4 - SYNCHRONISATION RÉFÉRENTIEL CATÉGORIES
### 🟢 J4-S1 : Moteur de Détection et Domain
- [x] ✅ `J4-S1-T1 - CategoryDelta_CreateMissingInCatamailer_ShouldSetProperties` (Validé le 15/09/2026 sur feature/j4-category-sync_step-1-core - Commit : c4bb8617b86d6b3921da6408fc012ccc3afe6eef)
- [x] ✅ `J4-S1-T1 - CategoryDelta_CreateMissingInOutlook_ShouldSetProperties` (Validé le 15/09/2026 sur feature/j4-category-sync_step-1-core - Commit : c4bb8617b86d6b3921da6408fc012ccc3afe6eef)
- [x] ✅ `J4-S1-T1 - CategoryDelta_CreateColorMismatch_ShouldSetBothColors` (Validé le 15/09/2026 sur feature/j4-category-sync_step-1-core - Commit : c4bb8617b86d6b3921da6408fc012ccc3afe6eef)
- [x] ✅ `J4-S1-T1 - SyncResult_AddDelta_ShouldUpdateConflictsAndLists` (Validé le 15/09/2026 sur feature/j4-category-sync_step-1-core - Commit : c4bb8617b86d6b3921da6408fc012ccc3afe6eef)
- [x] ✅ `J4-S1-T2 - AnalyzeSyncDeltasAsync_ShouldReturnNoConflicts_WhenRepositoriesMatchPerfectly` (Validé le 15/09/2026 sur feature/j4-category-sync_step-1-core - Commit : <COMMIT_HASH>)
- [x] ✅ `J4-S1-T2 - AnalyzeSyncDeltasAsync_ShouldDetectMissingInCatamailer` (Validé le 15/09/2026 sur feature/j4-category-sync_step-1-core - Commit : <COMMIT_HASH>)
- [x] ✅ `J4-S1-T2 - AnalyzeSyncDeltasAsync_ShouldDetectMissingInOutlook` (Validé le 15/09/2026 sur feature/j4-category-sync_step-1-core - Commit : <COMMIT_HASH>)
- [x] ✅ `J4-S1-T2 - AnalyzeSyncDeltasAsync_ShouldDetectColorMismatch` (Validé le 15/09/2026 sur feature/j4-category-sync_step-1-core - Commit : <COMMIT_HASH>)

---

## 🟢 JALON 5 - MIGRATION CSV
*À compléter.*