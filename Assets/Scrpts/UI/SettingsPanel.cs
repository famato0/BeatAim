using System.Collections.Generic;
using System.Globalization;
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

    [Header("Colori")]
    [SerializeField] private ColorField crosshairColor;
    [SerializeField] private ColorField targetColor;
    [SerializeField] private ColorField floorColor;
    [SerializeField] private ColorField wallColor;
    [SerializeField] private ColorField backgroundColor;

    [Header("Sensibilità")]
    [SerializeField] private float minSensitivity = 0.01f;
    [SerializeField] private float maxSensitivity = 100f;

    private readonly List<Vector2Int> resolutions = new List<Vector2Int>();

    private void Awake()
    {
        BuildResolutionList();

        sensitivityInput.contentType = TMP_InputField.ContentType.DecimalNumber;
        sensitivityInput.onEndEdit.AddListener(OnSensitivityEndEdit);
    }

    // Ogni volta che il pannello si apre, mostra i valori salvati
    // (scarta modifiche non salvate).
    private void OnEnable()
    {
        GameSettings s = SettingsManager.Current;

        nameInput.text = s.playerName;
        sensitivityInput.SetTextWithoutNotify(
            s.sensitivity.ToString("F2", CultureInfo.InvariantCulture));

        fullscreenToggle.isOn = s.fullscreen;

        int width = s.resolutionWidth > 0
            ? s.resolutionWidth
            : Screen.currentResolution.width;

        int height = s.resolutionHeight > 0
            ? s.resolutionHeight
            : Screen.currentResolution.height;

        int index = resolutions.FindIndex(
            r => r.x == width && r.y == height);

        resolutionDropdown.SetValueWithoutNotify(Mathf.Max(index, 0));
        resolutionDropdown.RefreshShownValue();

        crosshairColor.SetColor(s.crosshairColor);
        targetColor.SetColor(s.targetColor);
        floorColor.SetColor(s.floorColor);
        wallColor.SetColor(s.wallColor);
        backgroundColor.SetColor(s.backgroundColor);
    }

    private void OnSensitivityEndEdit(string input)
    {
        if (!float.TryParse(
                input,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out float value))
        {
            RestoreSensitivityValue();
            return;
        }

        value = Mathf.Clamp(value, minSensitivity, maxSensitivity);

        sensitivityInput.SetTextWithoutNotify(
            value.ToString("F2", CultureInfo.InvariantCulture));
    }

    private void RestoreSensitivityValue()
    {
        sensitivityInput.SetTextWithoutNotify(
            SettingsManager.Current.sensitivity.ToString(
                "F2", CultureInfo.InvariantCulture));
    }

    public void Save()
    {
        GameSettings s = SettingsManager.Current;

        s.playerName = string.IsNullOrWhiteSpace(nameInput.text)
            ? "Player1"
            : nameInput.text.Trim();

        if (float.TryParse(
                sensitivityInput.text,
                NumberStyles.Float,
                CultureInfo.InvariantCulture,
                out float sensitivity))
        {
            s.sensitivity = Mathf.Clamp(
                sensitivity, minSensitivity, maxSensitivity);
        }
        else
        {
            s.sensitivity = Mathf.Clamp(
                s.sensitivity, minSensitivity, maxSensitivity);
        }

        // Mantiene il campo sincronizzato con il valore effettivamente salvato.
        sensitivityInput.SetTextWithoutNotify(
            s.sensitivity.ToString("F2", CultureInfo.InvariantCulture));

        s.fullscreen = fullscreenToggle.isOn;

        Vector2Int res = resolutions[resolutionDropdown.value];
        s.resolutionWidth = res.x;
        s.resolutionHeight = res.y;

        s.crosshairColor = crosshairColor.Value;
        s.targetColor = targetColor.Value;
        s.floorColor = floorColor.Value;
        s.wallColor = wallColor.Value;
        s.backgroundColor = backgroundColor.Value;

        SettingsManager.Save();
        SettingsManager.ApplyResolution();
    }

    private void BuildResolutionList()
    {
        resolutions.Clear();
        List<string> options = new List<string>();

        foreach (Resolution r in Screen.resolutions)
        {
            Vector2Int size = new Vector2Int(r.width, r.height);

            // Screen.resolutions ripete la stessa size per ogni refresh rate.
            if (resolutions.Contains(size))
                continue;

            resolutions.Add(size);
            options.Add($"{r.width} x {r.height}");
        }

        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
    }
}