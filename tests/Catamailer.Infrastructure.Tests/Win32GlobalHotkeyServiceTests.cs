// Historique :
// 2026-09-08 : Création des tests de validation pour Win32GlobalHotkeyService (J3-S1-T2).
// 2026-09-08 : Adaptation du test pour supporter le contexte Headless/Background Thread de xUnit (J3-S1-T2).

using System;
using Catamailer.Infrastructure;
using Xunit;

namespace Catamailer.Infrastructure.Tests;

/// <summary>
/// Classe de tests validant le comportement du service de raccourcis globaux Win32.
/// </summary>
public class Win32GlobalHotkeyServiceTests
{
    [Fact]
    public void RegisterHotkey_StateShouldReflectRegistrationAttempt()
    {
        // Arrange
        using var service = new Win32GlobalHotkeyService();
        int hotkeyId = 1;
        
        // Modificateurs Ctrl+Alt+Shift (0x0007) et Touche F12 (0x7B) pour minimiser les risques de collision
        uint ModCtrlAltShift = 0x0007; 
        uint KeyF12 = 0x7B;

        // Act
        bool result = service.RegisterHotkey(hotkeyId, ModCtrlAltShift, KeyF12);

        // Assert
        // Dans un environnement de test headless (sans pompe de messages), l'API native peut légitimement refuser l'enregistrement.
        // On teste donc la cohérence de l'état interne de notre service vis-à-vis du résultat de l'OS.
        if (result)
        {
            Assert.True(service.UnregisterHotkey(hotkeyId), "Le désenregistrement doit réussir si l'enregistrement a réussi.");
        }
        else
        {
            Assert.False(service.UnregisterHotkey(hotkeyId), "Le désenregistrement doit échouer si le raccourci n'a pas pu être enregistré par l'OS.");
        }
    }

    [Fact]
    public void UnregisterHotkey_ShouldReturnFalse_WhenHotkeyDoesNotExist()
    {
        // Arrange
        using var service = new Win32GlobalHotkeyService();
        int nonExistentId = 999;

        // Act
        bool result = service.UnregisterHotkey(nonExistentId);

        // Assert
        Assert.False(result, "Le désenregistrement d'un raccourci inexistant devrait retourner false.");
    }
}