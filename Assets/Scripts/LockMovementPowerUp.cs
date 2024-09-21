using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockMovementPowerUp : MonoBehaviour
{
    private List<Rigidbody> player1Rigidbodies = new List<Rigidbody>();  // List of Rigidbody components for Player 1
    private List<Rigidbody> player2Rigidbodies = new List<Rigidbody>();  // List of Rigidbody components for Player 2
    private TouchInputHandler touchInputHandler;  // Reference to the TouchInputHandler script
    private CanvasManager canvasManager; //Reference to the CanvasManager script

    private void Start()
    {
        // Find the GameObjects with the Player1 and Player2 tags and get their Rigidbodies
        GameObject[] player1Objects = GameObject.FindGameObjectsWithTag("Player1");
        GameObject[] player2Objects = GameObject.FindGameObjectsWithTag("Player2");

        foreach (GameObject player in player1Objects)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                player1Rigidbodies.Add(rb);
            }
        }

        foreach (GameObject player in player2Objects)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                player2Rigidbodies.Add(rb);
            }
        }

        // Get the reference to the TouchInputHandler script
        touchInputHandler = FindObjectOfType<TouchInputHandler>();
        canvasManager = FindObjectOfType<CanvasManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        GameObject powerManagerObj = new GameObject("PowerManager");
        PowerManager powerManager = powerManagerObj.AddComponent<PowerManager>();

        if (other.CompareTag("Player1"))
        {
            powerManager.StartPowerCoroutine(player2Rigidbodies, 2f);

            // Lock rotation for Platform2
            if (touchInputHandler != null)
            {
                touchInputHandler.LockRotation("Platform2");
                canvasManager.EnableLockingObjects("Player1");
                touchInputHandler.StartUnlockRotationCoroutine("Platform2", "Player1", 2f);
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Player2"))
        {
            powerManager.StartPowerCoroutine(player1Rigidbodies, 2f);

            // Lock rotation for Platform1
            if (touchInputHandler != null)
            {
                touchInputHandler.LockRotation("Platform1");
                canvasManager.EnableLockingObjects("Player2");
                touchInputHandler.StartUnlockRotationCoroutine("Platform1", "Player2", 2f);
            }

            Destroy(gameObject);
        }
    }

    private IEnumerator ConstrictYAxisForDuration(List<Rigidbody> targetRigidbodies, float duration)
    {
        // Apply the Y-axis constraint
        SetYConstraint(targetRigidbodies, true);

        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Release the Y-axis constraint
        SetYConstraint(targetRigidbodies, false);
    }

    private void SetYConstraint(List<Rigidbody> targetRigidbodies, bool constrain)
    {
        foreach (Rigidbody rb in targetRigidbodies)
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
