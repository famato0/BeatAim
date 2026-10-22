using UnityEngine;
using UnityEngine.UI;

public class ThemeApplier : MonoBehaviour
{
    [SerializeField] private Renderer floor;
    [SerializeField] private Renderer wall;
    [SerializeField] private Image crosshair;
    [SerializeField] private Camera cam;

    private void Start()
    {
        GameSettings s = SettingsManager.Current;

        floor.material.color = s.floorColor;
        wall.material.color = s.wallColor;
        crosshair.color = s.crosshairColor;

        cam.clearFlags = CameraClearFlags.SolidColor;
        cam.backgroundColor = s.backgroundColor;
    }
}