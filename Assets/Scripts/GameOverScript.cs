using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    public List<TextMeshProUGUI> gameEndTexts;  // List of TextMeshProUGUI objects
    public List<Rigidbody> ballRigidbodies;     // List of associated Rigidbody components
    public List<SwipeToSpin> swipeToSpinScripts; // List of SwipeToSpin scripts to disable

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object we collided with has the tag "EndGame"
        if (other.CompareTag("EndGame"))
        {
            // Populate the swipeToSpinScripts list now that all objects should be spawned
            PopulateSwipeToSpinList();

            // Check if this ball has the tag "Player 1"
            if (CompareTag("Player1"))
            {
                Debug.Log("Player 1 won");
                UpdateText("Player 1 Wins!");
                ConstrainRigidbodies();
                DisableSwipeToSpin();
            }
            // Check if this ball has the tag "Player 2"
            else if (CompareTag("Player2"))
            {
                Debug.Log("Player 2 won");
                UpdateText("Player 2 Wins!");
                ConstrainRigidbodies();
                DisableSwipeToSpin();
            }
        }
    }

    private void UpdateText(string message)
    {
        foreach (TextMeshProUGUI text in gameEndTexts)
        {
            if (text != null)
            {
                text.text = message;
                text.gameObject.SetActive(true);
            }
        }
    }

    private void ConstrainRigidbodies()
    {
        foreach (Rigidbody rb in ballRigidbodies)
        {
            if (rb != null)
            {
                // Constrain the Y-axis position while keeping X and Z constraints
                rb.constraints = RigidbodyConstraints.FreezePositionY | 
                                 RigidbodyConstraints.FreezePositionX | 
                                 RigidbodyConstraints.FreezePositionZ | 
                                 RigidbodyConstraints.FreezeRotation;
            }
        }
    }

    private void DisableSwipeToSpin()
    {
        foreach (SwipeToSpin swipeScript in swipeToSpinScripts)
        {
            if (swipeScript != null)
            {
                swipeScript.canRotate = false;  // Disable rotation
            }
        }
    }

    private void PopulateSwipeToSpinList()
    {
        // Convert the array returned by FindObjectsOfType to a List
        swipeToSpinScripts = new List<SwipeToSpin>(FindObjectsOfType<SwipeToSpin>());
    }
}
