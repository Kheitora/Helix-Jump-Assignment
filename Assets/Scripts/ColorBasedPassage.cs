using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBasedPassage : MonoBehaviour
{
    public string targetTag = "ColorObstacle"; // Tag of the objects that should be checked for color matching
    public string passThroughLayer = "PassThrough"; // Layer name for passable objects

    private Renderer objectRenderer;
    private int defaultLayer;
    private int passThroughLayerIndex;

    void Start(){
        // Get the Renderer of the ball
        objectRenderer = GetComponent<Renderer>();

        // Cache layer indices
        defaultLayer = gameObject.layer;
        passThroughLayerIndex = LayerMask.NameToLayer(passThroughLayer);

        if (objectRenderer == null){
            Debug.LogError("No Renderer found on the object. Please add a Renderer component.");
        }
    }

    void Update(){
        // Find all objects with the target tag
        GameObject[] obstacles = GameObject.FindGameObjectsWithTag(targetTag);

        foreach (GameObject obstacle in obstacles){
            Renderer obstacleRenderer = obstacle.GetComponent<Renderer>();

            // Check if the obstacle has a Renderer
            if (obstacleRenderer != null){
                // If the colors match, change the obstacle's layer to PassThrough
                if (objectRenderer.material.color == obstacleRenderer.material.color){
                    obstacle.layer = passThroughLayerIndex;
                }
                else{
                    obstacle.layer = defaultLayer; // Reset to the default layer if the color doesn't match
                }
            }
        }
    }
}
