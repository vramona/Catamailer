# Spécifications Fonctionnelles et Techniques - Catamailer

---

## PARTIE 1 : DÉCOUPAGE FONCTIONNEL ET RÈGLES DE GESTION

### 1. Gestion Référentielle des Catégories (Master Data Management)
*   **Synchronisation Démarrage (Outlook -> Catamailer) :** Au lancement de l'application, Catamailer scanne les catégories existantes dans la *Master Category List* d'Outlook.
    *   **Catégories Inconnues dans Catamailer :** Toute catégorie présente dans Outlook mais absente de Catamailer fait l'objet d'une proposition d'import filtrable via une interface utilisateur dédiée. **L'import n'est jamais automatique.**
    *   **Renommage / Détection de conflit :** En cas de détection d'un écart de nom ou de couleur, une résolution manuelle est proposée à l'utilisateur.
    *   **Suppression Outlook :** Aucune catégorie n'est supprimée de Catamailer (ou d'Outlook) sans une validation explicite de l'utilisateur.
*   **Parents Implicites et Remontée de Couleur :** Les nœuds intermédiaires déduits du séparateur mais n'existant pas physiquement dans Outlook sont créés virtuellement. Si tous les enfants physiques d'un parent virtuel partagent la même couleur, ce parent hérite (bubble-up) de cette couleur pour optimiser l'arborescence.
*   **Synchronisation Temps Réel (Catamailer -> Outlook) :** Une fois le référentiel initialisé, Catamailer agit comme la source de vérité absolue. Toute modification dans Catamailer (Création, Changement de Couleur, Suppression validée) met à jour la *Master Category List* d'Outlook instantanément.
*   **Arborescence :** Les catégories sont structurées hiérarchiquement (Familles > Sous-catégories).
*   **Héritage des couleurs :** Une sous-catégorie hérite de la couleur de son parent, sauf surcharge explicite.
*   **Tagging en chaîne (Auto-propagation) :** Appliquer une sous-catégorie (ex: `CCOEN-Voyage`) applique automatiquement toute sa chaîne de parenté (`CCOEN` + `CCOEN-Voyage`). L'affichage dans Outlook reste plat.
*   **Renommage Rétroactif (Mass Update) :** Renommer une catégorie dans l'UI déclenche un traitement de fond recherchant les e-mails historiques avec l'ancien tag pour le remplacer par le nouveau.

### 2. Le Moteur de Traitement (Logique en 2 Étapes)
L'analyse d'un e-mail suit un flux séquentiel strict.
*   **Étape 1 - Classification :**
    *   Confrontation des métadonnées (Sujet, Expéditeur, Destinataires) à des dictionnaires de mots-clés.
    *   *Règle :* Le moteur parcourt l'intégralité des règles de dictionnaires (parité VBA) et déduit toutes les catégories correspondantes (avec propagation de leur chaîne d'ascendance respective) ainsi que le déclencheur exact (champ et mot-clé ayant matché).
*   **Étape 2 - Exécution :**
    *   À partir des catégories déduites et des métadonnées de l'e-mail, le système évalue l'ensemble des arbres de conditions composites (Opérateurs ET/OU infinis, correspondances exactes, partielles ou Regex).
    *   *Règle :* Si un arbre est validé, le système exécute **une liste d'actions séquentielles**. Les actions supportées sont :
        *   **Déplacer vers un dossier**
        *   **Marquer comme lu**
        *   **Transférer le message** (Nécessite la définition d'un ou plusieurs destinataires).
        *   **Définir l'importance** (Haute, Normale, Faible).
        *   **Ajouter un rappel** (Date/Heure spécifiques ou relance standardisée).
        *   **Assurer un suivi (Aujourd'hui)** (Drapeau de tâche Outlook).
        *   **Insérer une signature HTML** lors d'un transfert ou d'une réponse.
        *   **Ajouter une catégorie** (`AddCategory`) suite à l'évaluation d'un arbre de conditions complexes.

### 3. Prise de Notes "Inbox", Tâches et Agenda (Module PKM)
Catamailer intègre un module de productivité croisant les notes, l'agenda et les communications.
*   **Saisie au kilomètre (Outliner) :** Interface de notes rapides. Chaque ligne tapée génère un bloc indépendant possédant un horodatage de création/modification. Le rendu visuel est polymorphe (texte, case à cocher, etc.).
*   **Contextualisation Agenda (Unified Calendar) :** Le système lit en tâche de fond les calendriers Outlook et Google Workspace. En fonction de l'horodatage d'un bloc de note, il suggère l'association à la réunion en cours. La note hérite alors des catégories de la réunion.
*   **Actions Internes :** Transformation d'un bloc de note en "Action à faire" (Échéance, Responsable), stockée et suivie uniquement dans Catamailer.
*   **Actions de Communication (Quick Insert) :** Un bloc de note peut cibler une audience. Via un raccourci clavier global (ex: dans Confluence ou un mail), Catamailer affiche une popup, formate l'action, l'injecte par simulation presse-papier (`Ctrl+V`) dans l'application cible, et la clôture.

### 4. Interactions et Interfaces Utilisateur (UI Blazor)
L'application est furtive : elle vit dans la zone de notification (Tray Icon) et réagit aux raccourcis globaux.
*   **Synchronisation au Démarrage :** Modale non-bloquante de validation des deltas de catégories entre Outlook et Catamailer.
*   **Quick Categorize (Raccourci) :** Modale superposée à Outlook. Recherche par saisie libre (Autocomplete) avec rendu en "Chips". Tris dynamiques : Alphabétique, Récents, Suggestions contextuelles. Actions : Appliquer au mail, à la conversation (Thread), à l'expéditeur.
*   **Quick Rule Builder (Raccourci) :** Modale générant instantanément une règle d'Étape 1, pré-remplie avec le contexte de l'e-mail actuellement sélectionné dans Outlook.
*   **Écrans de Configuration :** Éditeur d'arborescence des catégories (Drag & Drop), Grilles des dictionnaires (avec rendu arborescent indenté), Builder visuel de l'arbre des actions.

### 5. Shadow Mode, Sécurité et Reprise d'Historique
*   **Shadow Mode (Double Run & Inspection Interactive) :** Le moteur tourne en parallèle de l'ancienne macro VBA. Dès réception d'un nouvel e-mail (`NewMailEx`), après un délai de 2 secondes (permettant à la macro VBA de s'exécuter), si le toggle de notification Shadow Mode est actif sur la page d'accueil, Catamailer présente une popup d'inspection séquentielle (un seul mail à la fois) affichant :
    *   Les métadonnées du mail (Sujet, Expéditeur, Destinataires) et les catégories réellement présentes dans Outlook (avec un bouton **Rafraîchir**).
    *   Le diagnostic de l'Étape 1 (catégories proposées et mot-clé déclencheur exact) et de l'Étape 2 (règles applicables, critères déclencheurs et actions prévues).
    *   Un curseur persistant (`LastShadowInspectedEntryId`) permettant de reprendre l'inspection séquentielle là où l'utilisateur s'était arrêté.
    *   Les actions de navigation et d'exécution manuelle à la demande : **Appliquer Étape 1**, **Appliquer Étape 2**, **Appliquer Tout (Global)**, **Suivant**, **Annuler** (ferme la popup), et **Stop** (ferme la popup et désactive le toggle de notifications).
*   **Verrou de sécurité et Activation Manuelle :** Catamailer fonctionne en mode lecture seule / Shadow Mode par défaut. Aucune modification automatique n'est appliquée à Outlook tant que l'utilisateur n'a pas activé explicitement l'écriture automatique (seules les actions manuelles déclenchées par l'utilisateur dans la popup d'inspection écrivent dans Outlook en Shadow Mode).
*   **Alerte d'Activation (Palier 300) :** Dès que la télémétrie enregistre 300 correspondances exactes sur au moins 30 catégories distinctes, l'IHM affiche une popup d'invitation à activer Catamailer. Si l'utilisateur refuse ou ignore l'activation, la notification réapparaît tous les 300 nouveaux messages qualifiés (palier 600, 900, etc.).
*   **Règle de Conflit :** Une classification manuelle par l'utilisateur (mail déjà déplacé) annule l'action de déplacement automatique.
*   **Pattern Debounce Configurable :** Modifier une règle suspend l'analyse d'historique (rattrapage de stock) pendant un délai paramétrable (défaut : 30 minutes), accessible via l'IHM des préférences. Le traitement des e-mails entrants en temps réel n'est pas affecté. Le curseur (`LastEntryID` et `ReceivedTime`) permet ensuite une reprise propre en tâche de fond sans bloquer l'IHM.

---

## PARTIE 2 : PROJECTION DANS LA CLEAN ARCHITECTURE

### 1. Projet `Catamailer.Domain` (Cœur Métier)
*Agnostique. Zéro dépendance. Pattern `Result<T>` absolu.*
*   **Arbre des Catégories :** `CategoryNode`.
*   **Synchronisation :** `CategoryDelta`, `SyncResult`.
*   **Classification & Exécution :** `DictionaryRule`, `RuleNode` (Composite), `RuleCriterion`, `RuleAction`.
*   **Notes & PKM :** `NoteBlock`, `InternalTask`, `CommunicationAction`.
*   **Télémétrie :** `ShadowModeLog`, `UsageStats`.
*   **Interfaces :** `IMailProvider`, `ICategoryProvider`, `ICategoryManagerProvider`, `ICalendarProvider`, `IGlobalHotkeyService`.

### 2. Projet `Catamailer.Application` (Orchestration)
*   **Engines :** `ClassificationEngine`, `ExecutionEngine`, `CategorySyncService`.
*   **PKM Services :** `NoteContextMatcher` (Croise timestamp Note avec créneaux Agenda).
*   **Background Services :** `DebounceService`, `HistoryRunner`, `ShadowModeValidator`, `ShadowInspectionService`.

### 3. Projet `Catamailer.Infrastructure` (Adaptateurs Externes)
*   **Persistance :** `CatamailerDbContext` (EF Core SQLite).
*   **Outlook COM :** `OutlookMailInterceptor` (`NewMailEx`), `OutlookCategoryMaster` (MCL Sync & Mass Update via DASL), `OutlookSelectionTracker`, `OutlookCalendarProvider`.
*   **Google Workspace :** `GoogleCalendarProvider` (OAuth 2.0).
*   **OS :** `Win32GlobalHotkeyService`, `ClipboardSimulator`.

### 4. Projet `Catamailer.UI` (MAUI Blazor Hybrid)
*   **Host :** Démarrage "Headless" (Tray Icon).
*   **Pages (Configuration) :** Dashboard (Shadow Mode logs & Toggle notifications), `CategoryTreeView.razor`, Dictionary Grids, Rule Builder.
*   **Modales (Quick Actions & Inspection) :** `QuickCategorizeModal.razor`, `QuickRuleBuilderModal.razor`, `QuickInsertModal.razor`, `CategorySyncModal.razor`, `ShadowInspectorModal.razor`.
*   **PKM :** `InboxOutliner.razor` (Rendu polymorphe des NoteBlocks).

### 5. Projet `Catamailer.Migrator` (Application Console)
*   Parser l'ancien fichier `Configuration_Regles.csv`, ignorer les entrées vides, mapper les actions avancées et initialiser en dur les 13 règles d'exécution VBA complexes, générer `catamailer.db`.