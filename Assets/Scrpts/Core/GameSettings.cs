using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
    public const float MinSensitivity = 0.01f;
    public const float MaxSensitivity = 99.99f;

    public string playerName = "Player1";
    public float sensitivity = 1f;

    public int resolutionWidth = 0;
    public int resolutionHeight = 0;
    public bool fullscreen = true;

    public Color crosshairColor = Color.white;
    public Color targetColor = new Color(1f, 0.25f, 0.25f);
    public Color floorColor = new Color(0.15f, 0.15f, 0.15f);
    public Color wallColor = new Color(0.35f, 0.35f, 0.4f);
    public Color backgroundColor = new Color(0.05f, 0.05f, 0.08f);

    public string clickSoundPath = ""; // vuoto = suono predefinito
}