using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverScript : MonoBehaviour
{
    public TouchInputHandler touchInputHandler;  // Reference to the TouchInputHandler
    public List<TextMeshProUGUI> gameEndTexts;
    public List<Rigidbody> ballRigidbodies;
    public List<Image> images;
    public Button replayButton;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndGame")){
            if (CompareTag("Player1")){
                EndGame("Player 1 Wins!");
            }
            else if (CompareTag("Player2")){
                EndGame("Player 2 Wins!");
            }
        }
    }

    private void EndGame(string message)
    {
        touchInputHandler.enabled = false;  // Disable the touch input handler to stop further rotations
        UpdateText(message);
        ConstrainRigidbodies();
    }

    private void UpdateText(string message)
    {
        foreach (TextMeshProUGUI text in gameEndTexts){
            if (text != null){
                text.text = message;
                text.gameObject.SetActive(true);
            }
        }

        foreach (Image image in images){
            if (image != null){
                image.gameObject.SetActive(true);
            }
        }

        replayButton.gameObject.SetActive(true);
    }

    private void ConstrainRigidbodies()
    {
        foreach (Rigidbody rb in ballRigidbodies){
            if (rb != null){
                rb.constraints = RigidbodyConstraints.FreezePositionY | 
                                 RigidbodyConstraints.FreezePositionX | 
                                 RigidbodyConstraints.FreezePositionZ | 
                                 RigidbodyConstraints.FreezeRotation;
            }
        }
    }
}
