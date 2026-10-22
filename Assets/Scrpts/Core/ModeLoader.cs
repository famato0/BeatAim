using System.IO;
using UnityEngine;

public static class ModeLoader
{
    // Configurazione scelta nel menu, letta dallo spawner nella scena di gioco
    public static ModeConfig Selected;

    public static ModeConfig LoadPreset(string fileName)
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Lib", "modes", fileName);

        if (!File.Exists(path))
        {
            Debug.LogWarning($"Preset non trovato: {path}");
            return new ModeConfig();
        }

        return JsonUtility.FromJson<ModeConfig>(File.ReadAllText(path));
    }
}