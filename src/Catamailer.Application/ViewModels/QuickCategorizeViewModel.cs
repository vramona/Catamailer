// Historique :
// 2026-09-08 : Création du stub pour les tests TDD de l'autocomplétion (J3-S2-T1).
// 2026-09-08 : Implémentation de la logique de recherche (J3-S2-T1).
// 2026-09-08 : Ajout de la propriété et méthode de sélection d'une catégorie (J3-S2-T1).

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catamailer.Domain;

namespace Catamailer.Application.ViewModels;

/// <summary>
/// ViewModel responsable de la logique de l'écran Quick Categorize (Recherche, Tris, Actions).
/// </summary>
public class QuickCategorizeViewModel
{
    private readonly ICategoryRepository _categoryRepository;
    private IEnumerable<CategoryNode> _allCategories = Array.Empty<CategoryNode>();

    /// <summary>
    /// Obtient ou définit le texte de recherche courant.
    /// </summary>
    public string SearchText { get; set; } = string.Empty;

    /// <summary>
    /// Obtient la liste des catégories filtrées selon la recherche.
    /// </summary>
    public IEnumerable<CategoryNode> FilteredCategories { get; private set; } = Array.Empty<CategoryNode>();

    /// <summary>
    /// Obtient la catégorie actuellement sélectionnée par l'utilisateur.
    /// </summary>
    public CategoryNode? SelectedCategory { get; private set; }

    /// <summary>
    /// Initialise une nouvelle instance du <see cref="QuickCategorizeViewModel"/>.
    /// </summary>
    /// <param name="categoryRepository">Le dépôt de catégories.</param>
    public QuickCategorizeViewModel(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
    }

    /// <summary>
    /// Initialise le ViewModel en chargeant l'intégralité des catégories existantes.
    /// </summary>
    public async Task InitializeAsync()
    {
        _allCategories = await _categoryRepository.GetAllAsync();
        FilteredCategories = _allCategories;
        SelectedCategory = null;
        SearchText = string.Empty;
    }

    /// <summary>
    /// Met à jour les résultats filtrés en fonction du texte de recherche fourni.
    /// La recherche ignore la casse et retourne l'intégralité des éléments si le texte est vide.
    /// </summary>
    /// <param name="searchText">Le texte saisi par l'utilisateur.</param>
    public Task UpdateSearchAsync(string searchText)
    {
        SearchText = searchText;

        if (string.IsNullOrWhiteSpace(searchText))
        {
            FilteredCategories = _allCategories;
        }
        else
        {
            FilteredCategories = _allCategories
                .Where(c => c.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        return Task.CompletedTask;
    }

    /// <summary>
    /// Définit la catégorie sélectionnée par l'utilisateur.
    /// </summary>
    /// <param name="category">La catégorie choisie.</param>
    public void SelectCategory(CategoryNode category)
    {
        SelectedCategory = category;
    }
}