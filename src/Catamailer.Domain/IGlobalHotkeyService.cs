// Historique :
// 2026-09-08 : Création de l'interface pour la gestion des raccourcis globaux (J3-S1-T2).

using System;

namespace Catamailer.Domain;

/// <summary>
/// Définit le contrat pour le service de gestion des raccourcis clavier globaux au niveau de l'OS.
/// </summary>
public interface IGlobalHotkeyService : IDisposable
{
    /// <summary>
    /// Déclenché lorsqu'un raccourci global enregistré est pressé.
    /// L'argument entier correspond à l'identifiant (ID) du raccourci.
    /// </summary>
    event EventHandler<int>? HotkeyPressed;

    /// <summary>
    /// Enregistre un nouveau raccourci clavier global.
    /// </summary>
    /// <param name="id">Identifiant unique du raccourci (défini par l'application).</param>
    /// <param name="modifiers">Modificateurs (ex: Alt=1, Ctrl=2, Shift=4, Win=8).</param>
    /// <param name="key">Code de la touche virtuelle (Virtual-Key Code).</param>
    /// <returns>True si l'enregistrement a réussi, sinon False (ex: conflit avec un autre programme).</returns>
    bool RegisterHotkey(int id, uint modifiers, uint key);

    /// <summary>
    /// Désenregistre un raccourci clavier global précédemment enregistré.
    /// </summary>
    /// <param name="id">L'identifiant du raccourci à supprimer.</param>
    /// <returns>True si le désenregistrement a réussi.</returns>
    bool UnregisterHotkey(int id);
}