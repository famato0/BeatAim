using System.Collections.Generic;
using System.Globalization;
using System.IO;
using SFB;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [Header("Generale")]
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_InputField sensitivityInput;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullscreenToggle;

    [Header("Suono click")]
    [SerializeField] private TMP_Text clickSoundLabel;

    [Header("Colori")]
    [SerializeField] private ColorField crosshairColor;
    [SerializeField] private ColorField targetColor;
    [SerializeField] private ColorField floorColor;
    [SerializeField] private ColorField wallColor;
    [SerializeField] private ColorField backgroundColor;

    private readonly List<Vector2Int> resolutions = new List<Vector2Int>();
    private string pendingSoundPath = "";

    private void Awake()
    {
        BuildResolutionList();
    }

    private void OnEnable()
    {
        GameSettings s = SettingsManager.Current;

        nameInput.text = s.playerName;
        sensitivityInput.text = FormatSensitivity(s.sensitivity);
        fullscreenToggle.isOn = s.fullscreen;

        int width = s.resolutionWidth > 0 ? s.resolutionWidth : Screen.currentResolution.width;
        int height = s.resolutionHeight > 0 ? s.resolutionHeight : Screen.currentResolution.height;
        int index = resolutions.FindIndex(r => r.x == width && r.y == height);
        resolutionDropdown.SetValueWithoutNotify(Mathf.Max(index, 0));
        resolutionDropdown.RefreshShownValue();

        pendingSoundPath = s.clickSoundPath;
        RefreshSoundLabel();

        crosshairColor.SetColor(s.crosshairColor);
        targetColor.SetColor(s.targetColor);
        floorColor.SetColor(s.floorColor);
        wallColor.SetColor(s.wallColor);
        backgroundColor.SetColor(s.backgroundColor);
    }

    // Collegare su "On End Edit (String)" del campo sensibilità
    public void OnSensitivityEndEdit(string text)
    {
        sensitivityInput.SetTextWithoutNotify(FormatSensitivity(ParseSensitivity(text)));
    }

    public void PickClickSound()
    {
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Choose sound", "", "mp3,ogg", false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            pendingSoundPath = paths[0];
            RefreshSoundLabel();
        }
    }

    public void ResetClickSound()
    {
        pendingSoundPath = "";
        RefreshSoundLabel();
    }

    public void Save()
    {
        GameSettings s = SettingsManager.Current;

        s.playerName = string.IsNullOrWhiteSpace(nameInput.text) ? "Player1" : nameInput.text.Trim();
        s.sensitivity = ParseSensitivity(sensitivityInput.text);
        s.fullscreen = fullscreenToggle.isOn;

        Vector2Int res = resolutions[resolutionDropdown.value];
        s.resolutionWidth = res.x;
        s.resolutionHeight = res.y;

        s.clickSoundPath = pendingSoundPath;

        s.crosshairColor = crosshairColor.Value;
        s.targetColor = targetColor.Value;
        s.floorColor = floorColor.Value;
        s.wallColor = wallColor.Value;
        s.backgroundColor = backgroundColor.Value;

        Target.RefreshAllColors(s.targetColor);

        SettingsManager.Save();
        SettingsManager.ApplyResolution();
    }

    private void RefreshSoundLabel()
    {
        string soundName = string.IsNullOrEmpty(pendingSoundPath)
            ? Path.GetFileName(SoundLibrary.ResolvePath(null))
            : Path.GetFileName(pendingSoundPath);

        clickSoundLabel.text = soundName;
    }

    // Accetta sia "1,5" (italiano) che "1.5"
    private static float ParseSensitivity(string text)
    {
        text = text.Replace(',', '.');

        if (!float.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out float value))
            value = 1f;

        return Mathf.Clamp(value, GameSettings.MinSensitivity, GameSettings.MaxSensitivity);
    }

    private static string FormatSensitivity(float value) =>
        value.ToString("0.##", CultureInfo.InvariantCulture);

    
    private void BuildResolutionList()
    {
        Debug.Log(
            $"SettingsPanel attivo: {gameObject.name} | " +
            $"Scena: {gameObject.scene.name} | " +
            $"Dropdown: {(resolutionDropdown == null ? "NULL" : resolutionDropdown.name)}",
            this
        );

        if (resolutionDropdown == null)
        {
            Debug.LogError(
                $"Resolution Dropdown NULL nell'istanza '{gameObject.name}' " +
                $"della scena '{gameObject.scene.name}'.",
                this
            );
            return;
        }

        resolutions.Clear();
        List<string> options = new List<string>();

        foreach (Resolution r in Screen.resolutions)
        {
            Vector2Int size = new Vector2Int(r.width, r.height);
            if (resolutions.Contains(size)) continue;

            resolutions.Add(size);
            options.Add($"{r.width} x {r.height}");
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
    }

}