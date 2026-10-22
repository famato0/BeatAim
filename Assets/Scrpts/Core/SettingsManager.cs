using System;
using System.IO;
using UnityEngine;

public static class SettingsManager
{
    private static GameSettings current;

    private static string FilePath =>
        Path.Combine(Application.persistentDataPath, "settings.json");

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
        try
        {
            if (File.Exists(FilePath))
                current = JsonUtility.FromJson<GameSettings>(File.ReadAllText(FilePath));
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Impossibile leggere le impostazioni: {e.Message}");
        }

        current ??= new GameSettings();
    }

    public static void Save()
    {
        try
        {
            File.WriteAllText(FilePath, JsonUtility.ToJson(Current, true));
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Impossibile salvare le impostazioni: {e.Message}");
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