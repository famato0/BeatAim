using System;
using UnityEngine;

[Serializable]
public class GameSettings
{
    public string playerName = "Player1";
    public float sensitivity = 0.5f;

    public int resolutionWidth = 0;   // 0 = usa quella attuale
    public int resolutionHeight = 0;
    public bool fullscreen = true;

    public Color crosshairColor = Color.white;
    public Color targetColor = new Color(1f, 0.25f, 0.25f);
    public Color floorColor = new Color(0.15f, 0.15f, 0.15f);
    public Color wallColor = new Color(0.35f, 0.35f, 0.4f);
    public Color backgroundColor = new Color(0.05f, 0.05f, 0.08f);
}