using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenuController : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject settingsPanel;

    [Header("Scene")]
    [SerializeField] private string mainMenuSceneName = "MainMenu"; //TODO - Serve un enum per ogni Scena - da applicare a tutti i casi

    private bool isPaused;
    private bool isSettingsOpen;

    private void Start()
    {
        Time.timeScale = 1f;

        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);

        isPaused = false;
        isSettingsOpen = false;

        SetGameplayCursor();
    }

    private void Update()
    {
        if (Keyboard.current == null ||
            !Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            return;
        }

        if (isSettingsOpen)
        {
            ReturnToPause();
        }
        else if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Pause()
    {
        isPaused = true;
        isSettingsOpen = false;

        Time.timeScale = 0f;

        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        isPaused = false;
        isSettingsOpen = false;

        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);

        Time.timeScale = 1f;

        SetGameplayCursor();
    }

    public void OpenSettings()
    {
        isSettingsOpen = true;

        pausePanel.SetActive(false);
        settingsPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReturnToPause()
    {
        isSettingsOpen = false;

        settingsPanel.SetActive(false);
        pausePanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ExitToMainMenu()
    {
        Time.timeScale = 1f;

        isPaused = false;
        isSettingsOpen = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadScene(mainMenuSceneName);
    }

    private void SetGameplayCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}