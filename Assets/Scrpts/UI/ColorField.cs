
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ColorField : MonoBehaviour
{
    [Header("Color field")]
    [SerializeField] private TMP_InputField hexInput;
    [SerializeField] private Image swatch;

    public Color Value { get; private set; } = Color.white;

    private void Awake()
    {
        hexInput.onEndEdit.AddListener(OnHexEndEdit);
    }

    private void OnEnable()
    {
        if (hexInput != null && swatch != null)
            RefreshFromInput();
    }

    public void SetColor(Color color)
    {
        Value = color;

        if (hexInput != null)
            hexInput.SetTextWithoutNotify(ColorUtility.ToHtmlStringRGB(color));

        RefreshSwatch();
    }

    private void OnHexEndEdit(string text)
    {
        RefreshFromInput();
    }

    private void RefreshFromInput()
    {
        if (hexInput == null || swatch == null)
            return;

        string hex = hexInput.text.Trim();

        if (hex.StartsWith("#"))
            hex = hex.Substring(1);

        if (hex.Length == 6 &&
            ColorUtility.TryParseHtmlString("#" + hex, out Color color))
        {
            Value = color;
            RefreshSwatch();
            hexInput.SetTextWithoutNotify(hex.ToUpperInvariant());
        }
        else
        {
            // Ripristina l'ultimo colore valido se il testo non è valido.
            hexInput.SetTextWithoutNotify(ColorUtility.ToHtmlStringRGB(Value));
            RefreshSwatch();
        }
    }

    private void RefreshSwatch()
    {
        if (swatch != null)
            swatch.color = Value;
    }
}
