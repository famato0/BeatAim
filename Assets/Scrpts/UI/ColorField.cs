using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorField : MonoBehaviour
{
    [SerializeField] private TMP_InputField hexInput;
    [SerializeField] private Image swatch;

    public Color Value { get; private set; } = Color.white;

    private void Awake()
    {
        hexInput.onEndEdit.AddListener(OnEndEdit);
    }

    public void SetColor(Color color)
    {
        Value = color;
        swatch.color = color;
        hexInput.SetTextWithoutNotify("#" + ColorUtility.ToHtmlStringRGB(color));
    }

    private void OnEndEdit(string text)
    {
        if (!text.StartsWith("#")) text = "#" + text;

        if (ColorUtility.TryParseHtmlString(text, out Color parsed))
            SetColor(parsed);
        else
            SetColor(Value); // input non valido: ripristina l'ultimo colore buono
    }
}