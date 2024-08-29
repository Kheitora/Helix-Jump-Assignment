using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Replay : MonoBehaviour
{
    public Button reloadButton;  // Reference to the button that will reload the game

    void Start()
    {
        // Ensure the button is assigned and add a listener to it
        if (reloadButton != null)
        {
            reloadButton.onClick.AddListener(ReloadCurrentScene);
        }
    }

    void ReloadCurrentScene()
    {
        // Get the currently active scene and reload it
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
