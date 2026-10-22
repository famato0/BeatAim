using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    // Sensibilità 1 nelle impostazioni = il valore di prima
    private const float BaseFactor = 0.1f;

    private float sensitivity;
    private float yaw;
    private float pitch;

    private void Start()
    {
        sensitivity = SettingsManager.Current.sensitivity * BaseFactor;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector2 delta = Mouse.current.delta.ReadValue();

        yaw += delta.x * sensitivity;
        pitch -= delta.y * sensitivity;
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }
}