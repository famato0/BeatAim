using TMPro;
using UnityEngine;

public class ScoreHUD : MonoBehaviour
{
    [SerializeField] private ScoreManager score;
    [SerializeField] private TMP_Text text;

    private void OnEnable()
    {
        score.Changed += Refresh;
        Refresh();
    }

    private void OnDisable()
    {
        score.Changed -= Refresh;
    }

    private void Refresh()
    {
        text.text =
            $"<b>{SettingsManager.Current.playerName}</b>\n" +
            $"Hit: {score.Hits}\n" +
            $"Shots: {score.Shots}\n" +
            $"Accuracy: {score.Accuracy:F1}%\n" +
            $"Expired: {score.Expired}";
    }
}