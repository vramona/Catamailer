// Historique :
// 2026-09-08 : Bouchon (Stub) initial pour les tests TDD (J3-S1-T2).
// 2026-09-08 : Implémentation P/Invoke de RegisterHotKey et UnregisterHotKey (J3-S1-T2).
// 2026-09-08 : Ajout du Subclassing Win32 pour intercepter WM_HOTKEY sur la fenêtre MAUI (J3-S1-T2).

using System;
using System.Collections.Concurrent;
using System.Runtime.InteropServices;
using Catamailer.Domain;

namespace Catamailer.Infrastructure;

/// <summary>
/// Implémentation Win32 du service de raccourcis globaux via P/Invoke (user32.dll et comctl32.dll).
/// </summary>
public class Win32GlobalHotkeyService : IGlobalHotkeyService
{
    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

    [DllImport("comctl32.dll", SetLastError = true)]
    private static extern bool SetWindowSubclass(IntPtr hWnd, SubclassProc pfnSubclass, nuint uIdSubclass, IntPtr dwRefData);

    [DllImport("comctl32.dll", SetLastError = true)]
    private static extern IntPtr DefSubclassProc(IntPtr hWnd, uint uMsg, UIntPtr wParam, IntPtr lParam);

    private delegate IntPtr SubclassProc(IntPtr hWnd, uint uMsg, UIntPtr wParam, IntPtr lParam, nuint uIdSubclass, IntPtr dwRefData);

    private const uint WM_HOTKEY = 0x0312;
    
    private readonly ConcurrentBag<int> _registeredHotkeys = new();
    private bool _disposed;
    private IntPtr _hwnd = IntPtr.Zero;
    private SubclassProc? _subclassDelegate; // Conservé au niveau de l'instance pour éviter la collecte par le Garbage Collector

    public event EventHandler<int>? HotkeyPressed;

    /// <summary>
    /// Initialise le hook de la boucle de messages native.
    /// Doit être appelé une fois la fenêtre WinUI 3 créée.
    /// </summary>
    /// <param name="hwnd">Le handle (HWND) de la fenêtre native.</param>
    public void Initialize(IntPtr hwnd)
    {
        if (_hwnd != IntPtr.Zero) return;
        
        _hwnd = hwnd;
        _subclassDelegate = WindowProc;
        SetWindowSubclass(_hwnd, _subclassDelegate, 1, IntPtr.Zero);
    }

    public bool RegisterHotkey(int id, uint modifiers, uint key)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(Win32GlobalHotkeyService));

        // Si _hwnd est IntPtr.Zero (ex: en Test Unitaire), le raccourci s'enregistre sur le thread courant.
        bool success = RegisterHotKey(_hwnd, id, modifiers, key);
        if (success)
        {
            _registeredHotkeys.Add(id);
        }
        return success;
    }

    public bool UnregisterHotkey(int id)
    {
        if (_disposed) throw new ObjectDisposedException(nameof(Win32GlobalHotkeyService));
        
        return UnregisterHotKey(_hwnd, id);
    }

    /// <summary>
    /// Procédure de fenêtre détournée pour écouter spécifiquement WM_HOTKEY.
    /// </summary>
    private IntPtr WindowProc(IntPtr hWnd, uint uMsg, UIntPtr wParam, IntPtr lParam, nuint uIdSubclass, IntPtr dwRefData)
    {
        if (uMsg == WM_HOTKEY)
        {
            int id = (int)wParam.ToUInt32();
            HotkeyPressed?.Invoke(this, id);
        }
        
        return DefSubclassProc(hWnd, uMsg, wParam, lParam);
    }

    public void Dispose()
    {
        if (_disposed) return;

        foreach (var id in _registeredHotkeys)
        {
            UnregisterHotKey(_hwnd, id);
        }
        
        _registeredHotkeys.Clear();
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    ~Win32GlobalHotkeyService()
    {
        Dispose();
    }
}