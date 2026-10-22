using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    private float sensitivity;
    private float yaw;
    private float pitch;

    private void Start()
    {
        sensitivity = SettingsManager.Current.sensitivity;
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