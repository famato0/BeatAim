using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject modePanel;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        // Tornando da una partita il cursore è ancora bloccato: lo liberiamo
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SettingsManager.ApplyResolution();
        ShowMain();
    }

    public void ShowMain() => Show(mainPanel);
    public void ShowModeSelect() => Show(modePanel);
    public void ShowSettings() => Show(settingsPanel);

    public void PlayStandard() => SceneManager.LoadScene(SceneNames.Standard);
    public void PlayMusic() => SceneManager.LoadScene(SceneNames.Music);

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void Show(GameObject panel)
    {
        mainPanel.SetActive(panel == mainPanel);
        modePanel.SetActive(panel == modePanel);
        settingsPanel.SetActive(panel == settingsPanel);
    }
}