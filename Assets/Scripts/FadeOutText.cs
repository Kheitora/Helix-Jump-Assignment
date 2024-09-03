using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeOutText : MonoBehaviour
{
    public float fadeDuration = 2f;  // Duration for the fade-out effect

    private TextMeshProUGUI textMeshPro;
    private Color32[] newVertexColors;

    private void Start()
    {
        // Get the TextMeshProUGUI component attached to this GameObject
        textMeshPro = GetComponent<TextMeshProUGUI>();
    }

    // Public method to start the fade-out process
    public void StartFadeOut()
    {
        if (textMeshPro != null){
            StartCoroutine(FadeOutCoroutine());
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;

        TMP_TextInfo textInfo = textMeshPro.textInfo;
        int characterCount = textInfo.characterCount;

        while (elapsedTime < fadeDuration){
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            // Update vertex colors
            for (int i = 0; i < characterCount; i++){
                if (textInfo.characterInfo[i].isVisible){
                    int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                    int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                    newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                    byte alphaByte = (byte)(alpha * 255);

                    newVertexColors[vertexIndex + 0].a = alphaByte;
                    newVertexColors[vertexIndex + 1].a = alphaByte;
                    newVertexColors[vertexIndex + 2].a = alphaByte;
                    newVertexColors[vertexIndex + 3].a = alphaByte;
                }
            }

            // Update the mesh with the new alpha values
            for (int i = 0; i < textInfo.meshInfo.Length; i++){
                textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;
                textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
            }

            yield return null;  // Wait until the next frame
        }

        // Ensure the text is completely faded out at the end
        for (int i = 0; i < characterCount; i++){
            if (textInfo.characterInfo[i].isVisible){
                int vertexIndex = textInfo.characterInfo[i].vertexIndex;
                int materialIndex = textInfo.characterInfo[i].materialReferenceIndex;

                newVertexColors = textInfo.meshInfo[materialIndex].colors32;

                newVertexColors[vertexIndex + 0].a = 0;
                newVertexColors[vertexIndex + 1].a = 0;
                newVertexColors[vertexIndex + 2].a = 0;
                newVertexColors[vertexIndex + 3].a = 0;
            }
        }

        // Update the mesh with the final alpha values
        for (int i = 0; i < textInfo.meshInfo.Length; i++){
            textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;
            textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
