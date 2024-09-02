using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatableObject : MonoBehaviour
{
    public string tagToRotate;
    public float rotationSpeed = 0.1f;

    void OnEnable()
    {
        TouchInputHandler.OnRotate += RotateObject;
    }

    void OnDisable()
    {
        TouchInputHandler.OnRotate -= RotateObject;
    }

    void RotateObject(string targetTag, float movement)
    {
        if (targetTag == tagToRotate)
        {
            transform.Rotate(Vector3.up, -movement * rotationSpeed * Time.deltaTime);
        }
    }
}
