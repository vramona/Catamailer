// Historique :
// 2026-09-08 : Création du ViewModel QuickRuleBuilderViewModel (J3-S2-T2).
// 2026-09-08 : Ajout du chargement de la liste des catégories pour l'IHM (J3-S2-T2).
// 2026-09-09 : Adaptation à la nouvelle signature de DictionaryRule (J3-S2-T2-ST1).
// 2026-09-09 : Ajout des options de sélection et détection des catégories déclenchées (J3-S2-T2-ST2).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catamailer.Domain;

namespace Catamailer.Application.ViewModels;

/// <summary>
/// Représente une option sélectionnable dans l'IHM (case à cocher).
/// </summary>
public class SelectableOption
{
    /// <summary>
    /// La valeur textuelle de l'option.
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Indique si l'utilisateur a coché cette option.
    /// </summary>
    public bool IsSelected { get; set; }
}

/// <summary>
/// ViewModel responsable de la logique de l'écran Quick Rule Builder (Étape 1).
/// </summary>
public class QuickRuleBuilderViewModel
{
    private readonly ISelectionProvider _selectionProvider;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IRuleRepository _ruleRepository;
    private readonly ClassificationEngine _classificationEngine;

    /// <summary>
    /// Option de sélection pour le sujet de l'e-mail.
    /// </summary>
    public SelectableOption? SubjectOption { get; private set; }

    /// <summary>
    /// Options de sélection pour les expéditeurs (Sender et OnBehalfOf).
    /// </summary>
    public List<SelectableOption> SenderOptions { get; private set; } = new();

    /// <summary>
    /// Options de sélection pour les destinataires externes.
    /// </summary>
    public List<SelectableOption> RecipientOptions { get; private set; } = new();

    /// <summary>
    /// Obtient la liste globale des catégories disponibles.
    /// </summary>
    public IEnumerable<CategoryNode> Categories { get; private set; } = Array.Empty<CategoryNode>();

    /// <summary>
    /// Obtient la liste des catégories qui sont déjà déclenchées par l'e-mail courant.
    /// </summary>
    public IEnumerable<CategoryNode> TriggeredCategories { get; private set; } = Array.Empty<CategoryNode>();

    /// <summary>
    /// Obtient la catégorie cible actuellement sélectionnée pour la règle.
    /// </summary>
    public CategoryNode? SelectedCategory { get; private set; }

    /// <summary>
    /// Initialise une nouvelle instance du <see cref="QuickRuleBuilderViewModel"/>.
    /// </summary>
    public QuickRuleBuilderViewModel(
        ISelectionProvider selectionProvider, 
        ICategoryRepository categoryRepository,
        IRuleRepository ruleRepository,
        ClassificationEngine classificationEngine)
    {
        _selectionProvider = selectionProvider ?? throw new ArgumentNullException(nameof(selectionProvider));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        _ruleRepository = ruleRepository ?? throw new ArgumentNullException(nameof(ruleRepository));
        _classificationEngine = classificationEngine ?? throw new ArgumentNullException(nameof(classificationEngine));
    }

    /// <summary>
    /// Initialise le ViewModel en récupérant les métadonnées, en construisant les options de filtrage 
    /// et en identifiant les catégories déjà déclenchées.
    /// </summary>
    public async Task InitializeAsync()
    {
        Categories = await _categoryRepository.GetAllAsync();
        var rules = await _ruleRepository.GetAllDictionaryRulesAsync();

        var mail = _selectionProvider.GetSelectedMail();
        if (mail != null)
        {
            if (!string.IsNullOrWhiteSpace(mail.Subject))
            {
                SubjectOption = new SelectableOption { Value = mail.Subject, IsSelected = false };
            }

            if (!string.IsNullOrWhiteSpace(mail.Sender))
            {
                SenderOptions.Add(new SelectableOption { Value = mail.Sender, IsSelected = false });
            }
            if (!string.IsNullOrWhiteSpace(mail.OnBehalfOf))
            {
                SenderOptions.Add(new SelectableOption { Value = mail.OnBehalfOf, IsSelected = false });
            }

            if (mail.ExternalRecipients != null)
            {
                foreach (var recipient in mail.ExternalRecipients)
                {
                    RecipientOptions.Add(new SelectableOption { Value = recipient, IsSelected = false });
                }
            }

            var classificationResult = _classificationEngine.Classify(mail, rules);
            if (classificationResult != null)
            {
                TriggeredCategories = classificationResult.AppliedCategories;
            }
        }
    }

    /// <summary>
    /// Définit la catégorie cible pour la règle en cours de création.
    /// </summary>
    /// <param name="category">La catégorie sélectionnée.</param>
    public void SelectCategory(CategoryNode category)
    {
        SelectedCategory = category;
    }

    /// <summary>
    /// Construit l'objet DictionaryRule final en incluant uniquement les options cochées.
    /// </summary>
    /// <returns>La règle générée, ou null si aucune catégorie n'a été sélectionnée.</returns>
    public DictionaryRule? BuildRule()
    {
        if (SelectedCategory == null)
        {
            return null;
        }

        var subjectKws = SubjectOption != null && SubjectOption.IsSelected 
            ? new[] { SubjectOption.Value } 
            : null;
        
        var senderKws = SenderOptions.Where(o => o.IsSelected).Select(o => o.Value).ToList();
        var recipientKws = RecipientOptions.Where(o => o.IsSelected).Select(o => o.Value).ToList();

        return new DictionaryRule(
            SelectedCategory, 
            subjectKeywords: subjectKws, 
            senderKeywords: senderKws.Any() ? senderKws : null,
            recipientKeywords: recipientKws.Any() ? recipientKws : null);
    }
}