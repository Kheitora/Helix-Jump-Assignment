using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactEffect : MonoBehaviour
{
    public float squishAmount = 0.2f;  // How much the sphere squishes on impact
    public float squishDuration = 0.2f; // How quickly it returns to normal size

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;  // Save the original scale of the sphere
    }

    void OnCollisionEnter(Collision collision)
    {
        // Start the squish effect
        StartCoroutine(SquishEffect());
    }

    System.Collections.IEnumerator SquishEffect()
    {
        // Squish the sphere along the axis of the impact
        Vector3 squishScale = originalScale;
        squishScale.y -= squishAmount;
        squishScale.x += squishAmount / 2;
        squishScale.z += squishAmount / 2;

        transform.localScale = squishScale;

        // Wait for a short duration
        yield return new WaitForSeconds(squishDuration);

        // Smoothly return to original size
        float elapsedTime = 0f;
        while (elapsedTime < squishDuration){
            transform.localScale = Vector3.Lerp(squishScale, originalScale, (elapsedTime / squishDuration));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = originalScale;
    }
}
