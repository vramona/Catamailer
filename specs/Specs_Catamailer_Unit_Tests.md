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
- [x] ✅ `J1-S2-T1 - Classify_ShouldReturnNull_WhenNoRulesProvided` (Validé le 07/09/2026 sur feature/j1-core-engine_step-2-evaluation - Commit : <COMMIT_HASH>)
- [x] ✅ `J1-S2-T1 - Classify_ShouldReturnCategoryAndAscendanceChain_WhenKeywordMatches` (Validé le 07/09/2026 sur feature/j1-core-engine_step-2-evaluation - Commit : <COMMIT_HASH>)
- [x] ✅ `J1-S2-T1 - Classify_ShouldReturnCategory_WhenKeywordMatchesRecipient` (Validé le 07/09/2026 sur feature/j1-core-engine_step-2-evaluation - Commit : <COMMIT_HASH>)

---

## 🟢 JALON 2 - OUTLOOK COM & SHADOW MODE
*À compléter.*

---

## 🟢 JALON 3 - TRAY APP, GLOBAL HOTKEYS & UI
*À compléter.*

---

## 🟢 JALON 4 - INBOX NOTES, AGENDA & ACTIONS INTERNES
*À compléter.*

---

## 🟢 JALON 5 - MIGRATION CSV
*À compléter.*