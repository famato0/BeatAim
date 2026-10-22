using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLook : MonoBehaviour
{
    // Sensibilità 1 nelle impostazioni = il valore di prima.
    private const float BaseFactor = 0.1f;

    private float sensitivity;
    private float yaw;
    private float pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        UpdateSensitivity();
    }

    private void Update()
    {
        // Non ruotare la telecamera durante la pausa.
        if (Time.timeScale == 0f)
            return;

        // Evita errori se il mouse non è disponibile.
        if (Mouse.current == null)
            return;

        // Aggiorna la sensibilità in base alle impostazioni correnti.
        UpdateSensitivity();

        Vector2 delta = Mouse.current.delta.ReadValue();

        yaw += delta.x * sensitivity;
        pitch -= delta.y * sensitivity;

        // Limita la rotazione verticale.
        pitch = Mathf.Clamp(pitch, -89f, 89f);

        transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void UpdateSensitivity()
    {
        sensitivity = SettingsManager.Current.sensitivity * BaseFactor;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}