using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownTimer : MonoBehaviour
{
    public TextMeshProUGUI countdownText;  // Reference to the TextMeshProUGUI component for countdown
    public List<FadeOutText> fadeOutText;  // List of FadeOutText to be faded out
    public int startCount = 5;             // Countdown starting value
    public List<Rigidbody> ballRigidbodies;  // List of ball objects with Rigidbody components
    public List<Image> fadeOutImages; // List of image components
    public Image countdownImage;

    private void Start()
    {
        // Constrain Y-axis for all balls and disable spinning at the start
        SetYConstraint(true);

        // Start the countdown coroutine
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        int currentCount = startCount;

        while (currentCount > 0)
        {
            // Update the TextMeshPro object with the current count
            countdownText.text = currentCount.ToString();

            // If the countdown reaches 3, trigger the fade-out on all objects
            if (currentCount == 3)
            {
                foreach (FadeOutText fadeOut in fadeOutText)
                {
                    if (fadeOut != null)
                    {
                        fadeOut.StartFadeOut();
                    }
                }
                foreach (Image image in fadeOutImages)
                {
                    if (image != null)
                    {
                        StartCoroutine(FadeOutImage(image));
                    }
                }
            }

            // Wait for 1 second
            yield return new WaitForSeconds(1f);

            // Decrease the count
            currentCount--;
        }

        // After the countdown reaches 1, update the text to "Start"
        countdownImage.gameObject.SetActive(false);
        countdownText.text = "Start";

        // Lift the Y-axis constraint, allowing balls to fall
        SetYConstraint(false);

        yield return new WaitForSeconds(0.3f);

        countdownText.gameObject.SetActive(false);
    }

    private IEnumerator FadeOutImage(Image image)
    {
        float duration = 2f;  // Duration of the fade-out
        float elapsedTime = 0f;
        Color originalColor = image.color;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / duration);
            image.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        // Ensure the image is completely transparent at the end
        image.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
    }

    private void SetYConstraint(bool constrain)
    {
        foreach (Rigidbody rb in ballRigidbodies)
        {
            if (rb != null)
            {
                if (constrain)
                {
                    // Freeze position on the Y-axis while preserving X and Z constraints
                    rb.constraints = RigidbodyConstraints.FreezePositionX | 
                                     RigidbodyConstraints.FreezePositionY | 
                                     RigidbodyConstraints.FreezePositionZ | 
                                     RigidbodyConstraints.FreezeRotation;
                }
                else
                {
                    // Keep X and Z position constraints, but remove the Y-axis constraint
                    rb.constraints = RigidbodyConstraints.FreezePositionX | 
                                     RigidbodyConstraints.FreezePositionZ | 
                                     RigidbodyConstraints.FreezeRotation;
                }
            }
        }
    }
}
