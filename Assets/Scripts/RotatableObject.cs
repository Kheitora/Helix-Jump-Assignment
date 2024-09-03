using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    public string tagToRotate;  // Set this in the Inspector to either "PlatformWASD" or "PlatformArrow"
    public float rotationSpeed = 100f;  // Adjust the rotation speed
    public bool invertRotation = false;  // Set this in the Inspector to invert rotation for specific objects

    void OnEnable()
    {
        // Subscribe to the correct event from the InputHandler script
        InputHandler.OnRotate += RotateObject;
    }

    void OnDisable()
    {
        // Unsubscribe from the event when the object is disabled
        InputHandler.OnRotate -= RotateObject;
    }

    void RotateObject(string targetTag, float movement)
    {
        if (targetTag == tagToRotate)
        {
            // Invert rotation if needed
            float direction = invertRotation ? 1 : -1;
            transform.Rotate(Vector3.up, direction * movement * rotationSpeed * Time.deltaTime);
        }
    }
}
