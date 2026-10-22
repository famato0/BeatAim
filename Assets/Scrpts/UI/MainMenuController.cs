using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject modePanel;
    [SerializeField] private GameObject freeplayPanel;
    [SerializeField] private GameObject settingsPanel;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SettingsManager.ApplyResolution();
        ShowMain();
    }

    public void ShowMain() => Show(mainPanel);
    public void ShowModeSelect() => Show(modePanel);
    public void ShowFreeplay() => Show(freeplayPanel);
    public void ShowSettings() => Show(settingsPanel);

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
        freeplayPanel.SetActive(panel == freeplayPanel);
        settingsPanel.SetActive(panel == settingsPanel);
    }
}