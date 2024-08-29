using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerManager : MonoBehaviour
{
    public void StartPowerCoroutine(List<Rigidbody> targetRigidbodies, float duration)
    {
        StartCoroutine(ConstrictYAxisForDuration(targetRigidbodies, duration));
    }

    private IEnumerator ConstrictYAxisForDuration(List<Rigidbody> targetRigidbodies, float duration)
    {
        // Apply the Y-axis constraint
        foreach (Rigidbody rb in targetRigidbodies)
        {
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX | 
                                 RigidbodyConstraints.FreezePositionY | 
                                 RigidbodyConstraints.FreezePositionZ | 
                                 RigidbodyConstraints.FreezeRotation;
            }
        }

        yield return new WaitForSeconds(duration);

        // Release the Y-axis constraint
        foreach (Rigidbody rb in targetRigidbodies)
        {
            if (rb != null)
            {
                rb.constraints = RigidbodyConstraints.FreezePositionX | 
                                 RigidbodyConstraints.FreezePositionZ | 
                                 RigidbodyConstraints.FreezeRotation;
            }
        }

        // Optionally destroy this manager object after it's done
        Destroy(gameObject);
    }
}
