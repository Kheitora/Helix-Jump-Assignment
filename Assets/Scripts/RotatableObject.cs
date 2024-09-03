using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    public string tagToRotate;  // Set this in the Inspector to either "Platform1" or "Platform2"
    public float rotationSpeed = 0.1f;
    public bool invertRotation = false;  // Set this in the Inspector to invert rotation for specific objects

    void OnEnable()
    {
        TouchInputHandler.OnRotate += RotateObject;  // Subscribe to the event
    }

    void OnDisable()
    {
        TouchInputHandler.OnRotate -= RotateObject;  // Unsubscribe from the event
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
