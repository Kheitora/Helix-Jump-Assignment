using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeOutText : MonoBehaviour
{
    public float fadeDuration = 2f;  // Duration for the fade-out effect

    private TextMeshProUGUI textMeshPro;
    private Color originalColor;

    private void Start()
    {
        // Get the TextMeshProUGUI component attached to this GameObject
        textMeshPro = GetComponent<TextMeshProUGUI>();
        if (textMeshPro != null)
        {
            originalColor = textMeshPro.color;
        }
    }

    // Public method to start the fade-out process
    public void StartFadeOut()
    {
        if (textMeshPro != null)
        {
            StartCoroutine(FadeOutCoroutine());
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;  // Wait until the next frame
        }

        // Ensure the text is completely faded out at the end
        textMeshPro.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }
}
