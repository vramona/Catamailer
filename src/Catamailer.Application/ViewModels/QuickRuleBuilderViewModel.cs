// Historique :
// 2026-09-08 : Création du ViewModel QuickRuleBuilderViewModel (J3-S2-T2).
// 2026-09-08 : Ajout du chargement de la liste des catégories pour l'IHM (J3-S2-T2).
// 2026-09-09 : Adaptation à la nouvelle signature de DictionaryRule (J3-S2-T2-ST1).

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catamailer.Domain;

namespace Catamailer.Application.ViewModels;

/// <summary>
/// ViewModel responsable de la logique de l'écran Quick Rule Builder (Étape 1).
/// </summary>
public class QuickRuleBuilderViewModel
{
    private readonly ISelectionProvider _selectionProvider;
    private readonly ICategoryRepository _categoryRepository;

    /// <summary>
    /// Obtient ou définit le mot-clé extrait du sujet de l'e-mail.
    /// </summary>
    public string SubjectKeyword { get; set; } = string.Empty;

    /// <summary>
    /// Obtient ou définit le mot-clé extrait de l'expéditeur de l'e-mail.
    /// </summary>
    public string SenderKeyword { get; set; } = string.Empty;

    /// <summary>
    /// Obtient la liste des catégories disponibles pour la règle.
    /// </summary>
    public IEnumerable<CategoryNode> Categories { get; private set; } = Array.Empty<CategoryNode>();

    /// <summary>
    /// Obtient la catégorie cible sélectionnée pour la règle.
    /// </summary>
    public CategoryNode? SelectedCategory { get; private set; }

    /// <summary>
    /// Initialise une nouvelle instance du <see cref="QuickRuleBuilderViewModel"/>.
    /// </summary>
    /// <param name="selectionProvider">Le fournisseur permettant de récupérer l'e-mail sélectionné.</param>
    /// <param name="categoryRepository">Le dépôt de catégories.</param>
    public QuickRuleBuilderViewModel(ISelectionProvider selectionProvider, ICategoryRepository categoryRepository)
    {
        _selectionProvider = selectionProvider ?? throw new ArgumentNullException(nameof(selectionProvider));
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    /// <summary>
    /// Initialise le ViewModel en récupérant les métadonnées de l'e-mail sélectionné et les catégories.
    /// </summary>
    public async Task InitializeAsync()
    {
        Categories = await _categoryRepository.GetAllAsync();

        var mail = _selectionProvider.GetSelectedMail();
        if (mail != null)
        {
            SubjectKeyword = mail.Subject ?? string.Empty;
            SenderKeyword = mail.Sender ?? string.Empty;
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
    /// Construit l'objet DictionaryRule final à partir des champs saisis.
    /// </summary>
    /// <returns>La règle générée, ou null si aucune catégorie n'a été sélectionnée.</returns>
    public DictionaryRule? BuildRule()
    {
        if (SelectedCategory == null)
        {
            return null;
        }

        var subjectKws = string.IsNullOrWhiteSpace(SubjectKeyword) ? null : new[] { SubjectKeyword };
        var senderKws = string.IsNullOrWhiteSpace(SenderKeyword) ? null : new[] { SenderKeyword };

        return new DictionaryRule(SelectedCategory, subjectKeywords: subjectKws, senderKeywords: senderKws);
    }
}