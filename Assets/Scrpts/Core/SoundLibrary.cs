using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SoundLibrary
{
    public const string DefaultSoundName = "default_click.mp3";

    // Suoni distribuiti col gioco
    public static string DefaultFolder =>
        Path.Combine(Application.streamingAssetsPath, "Lib", "sounds");

    // Suoni aggiunti dall'utente
    public static string UserFolder =>
        Path.Combine(Application.persistentDataPath, "sounds");

    // Nomi di tutti i suoni disponibili (default + utente)
    public static List<string> ListSoundNames()
    {
        List<string> names = new List<string>();
        AddSounds(DefaultFolder, names);
        AddSounds(UserFolder, names);
        return names;
    }

    // Percorso completo di un suono. Se non esiste, ripiega sul default.
    public static string ResolvePath(string soundName)
    {
        if (string.IsNullOrEmpty(soundName))
            soundName = DefaultSoundName;

        string userPath = Path.Combine(UserFolder, soundName);
        if (File.Exists(userPath)) return userPath;

        string defaultPath = Path.Combine(DefaultFolder, soundName);
        if (File.Exists(defaultPath)) return defaultPath;

        return Path.Combine(DefaultFolder, DefaultSoundName);
    }

    // Copia un file audio scelto dall'utente nella libreria e ritorna il nome salvato
    public static string Import(string sourcePath)
    {
        Directory.CreateDirectory(UserFolder);

        string name = UniqueName(Path.GetFileName(sourcePath));
        File.Copy(sourcePath, Path.Combine(UserFolder, name));
        return name;
    }

    private static string UniqueName(string fileName)
    {
        string baseName = Path.GetFileNameWithoutExtension(fileName);
        string extension = Path.GetExtension(fileName);

        string candidate = fileName;
        int counter = 1;

        while (NameExists(candidate))
            candidate = $"{baseName}_{counter++}{extension}";

        return candidate;
    }

    private static bool NameExists(string name)
    {
        return File.Exists(Path.Combine(UserFolder, name)) ||
               File.Exists(Path.Combine(DefaultFolder, name));
    }

    private static void AddSounds(string folder, List<string> names)
    {
        if (!Directory.Exists(folder)) return;

        foreach (string file in Directory.GetFiles(folder))
        {
            string ext = Path.GetExtension(file).ToLowerInvariant();
            if (ext == ".mp3" || ext == ".ogg")
                names.Add(Path.GetFileName(file));
        }
    }
}