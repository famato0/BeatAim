using System;
using System.IO;
using UnityEngine;

public static class SettingsManager
{
    private static GameSettings current;

    // Copia dell'utente: scrivibile anche nel build
    private static string UserFile =>
        Path.Combine(Application.persistentDataPath, "settings.json");

    // Default distribuiti col gioco
    private static string DefaultFile =>
        Path.Combine(Application.streamingAssetsPath, "Lib", "settings.json");

    public static GameSettings Current
    {
        get
        {
            if (current == null) Load();
            return current;
        }
    }

    public static void Load()
    {
        current = ReadFile(UserFile) ?? ReadFile(DefaultFile) ?? new GameSettings();
        current.sensitivity = Mathf.Clamp(current.sensitivity,
            GameSettings.MinSensitivity, GameSettings.MaxSensitivity);
    }

    public static void Save()
    {
        File.WriteAllText(UserFile, JsonUtility.ToJson(Current, true));
    }

    private static GameSettings ReadFile(string path)
    {
        if (!File.Exists(path)) return null;

        try
        {
            return JsonUtility.FromJson<GameSettings>(File.ReadAllText(path));
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Impossibile leggere {path}: {e.Message}");
            return null;
        }
    }

    public static void ApplyResolution()
    {
        GameSettings s = Current;
        if (s.resolutionWidth <= 0 || s.resolutionHeight <= 0) return;

        FullScreenMode mode = s.fullscreen ? FullScreenMode.FullScreenWindow : FullScreenMode.Windowed;
        Screen.SetResolution(s.resolutionWidth, s.resolutionHeight, mode);
    }
}