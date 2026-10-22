
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FreeplayMenu : MonoBehaviour
{
    [SerializeField] private Slider targetsSlider;
    [SerializeField] private TMP_Text targetsLabel;

    [Header("Direzioni")]
    [SerializeField] private Toggle horizontalToggle;
    [SerializeField] private Toggle verticalToggle;
    [SerializeField] private Toggle diagonalToggle;

    [SerializeField] private Slider speedSlider;
    [SerializeField] private TMP_Text speedLabel;
    [SerializeField] private Toggle bounceToggle;
    [SerializeField] private Toggle despawnToggle;
    [SerializeField] private Slider lifetimeSlider;
    [SerializeField] private TMP_Text lifetimeLabel;

    private void Awake()
    {
        targetsSlider.onValueChanged.AddListener(
            v => targetsLabel.text = ((int)v).ToString()
        );

        speedSlider.onValueChanged.AddListener(
            v => speedLabel.text = v.ToString("0.0")
        );

        lifetimeSlider.onValueChanged.AddListener(
            v => lifetimeLabel.text = v.ToString("0.0") + " s"
        );

        despawnToggle.onValueChanged.AddListener(UpdateLifetimeVisibility);
    }

    // Ogni volta che il pannello si apre, carica i valori dal preset.
    private void OnEnable()
    {
        ModeConfig c = ModeLoader.LoadPreset("freeplay.json");

        targetsSlider.value = c.maxTargets;

        horizontalToggle.isOn =
            c.motion.patterns != null &&
            c.motion.patterns.Contains("Horizontal");

        verticalToggle.isOn =
            c.motion.patterns != null &&
            c.motion.patterns.Contains("Vertical");

        diagonalToggle.isOn =
            c.motion.patterns != null &&
            c.motion.patterns.Contains("Diagonal");

        speedSlider.value = c.motion.speed;
        bounceToggle.isOn = c.motion.bounce;

        despawnToggle.isOn = c.despawn.enabled;
        UpdateLifetimeVisibility(despawnToggle.isOn);
        lifetimeSlider.value = c.despawn.lifetime;
    }

    public void StartGame()
    {
        ModeLoader.Selected = new ModeConfig
        {
            name = "Freeplay",
            type = "freeplay",
            maxTargets = (int)targetsSlider.value,

            motion = new MotionConfig
            {
                patterns = GetSelectedPatterns(),
                speed = speedSlider.value,
                bounce = bounceToggle.isOn
            },

            despawn = new DespawnConfig
            {
                enabled = despawnToggle.isOn,
                lifetime = lifetimeSlider.value
            }
        };

        SceneManager.LoadScene(SceneNames.Standard);
    }

    private List<string> GetSelectedPatterns()
    {
        List<string> patterns = new List<string>();

        if (horizontalToggle.isOn)
            patterns.Add("Horizontal");

        if (verticalToggle.isOn)
            patterns.Add("Vertical");

        if (diagonalToggle.isOn)
            patterns.Add("Diagonal");

        return patterns;
    }

    private void UpdateLifetimeVisibility(bool isEnabled)
    {
        lifetimeSlider.gameObject.SetActive(isEnabled);
        lifetimeLabel.gameObject.SetActive(isEnabled);
    }
}
