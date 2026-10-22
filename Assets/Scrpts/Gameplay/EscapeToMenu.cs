using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class EscapeToMenu : MonoBehaviour
{
    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            SceneManager.LoadScene(SceneNames.MainMenu);
    }
}