using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RainbowText : MonoBehaviour
{
    private TextMeshProUGUI textMeshPro;
    public Color[] rainbowColors;

    private void Start()
    {
        // Get the TextMeshProUGUI component attached to this GameObject
        textMeshPro = GetComponent<TextMeshProUGUI>();

        if (textMeshPro != null){
            ApplyRainbowColors();
        }
    }

    private void ApplyRainbowColors()
    {
        string text = textMeshPro.text;
        textMeshPro.text = "";  // Clear the text to rebuild it with colors

        // Loop through each character in the text
        for (int i = 0; i < text.Length; i++){
            // Get the color from the rainbowColors array, cycling through the colors
            Color color = rainbowColors[i % rainbowColors.Length];

            // Add the character with the color tag
            textMeshPro.text += $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>{text[i]}</color>";
        }
    }
}
