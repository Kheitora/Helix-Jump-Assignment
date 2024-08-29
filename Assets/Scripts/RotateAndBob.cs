using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAndBob : MonoBehaviour
{
    public float rotationSpeed = 30f;  // Speed of rotation along the Y-axis
    public float bobbingAmplitude = 0.5f;  // The amplitude of the up-and-down bobbing motion
    public float bobbingFrequency = 1f;  // The frequency of the bobbing motion

    private Vector3 initialLocalPosition;  // The initial local position of the object

    void Start()
    {
        // Store the initial local position of the object
        initialLocalPosition = transform.localPosition;
    }

    void Update()
    {
        // Rotate the object around its Y-axis
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);

        // Bob the object up and down relative to its initial local position
        float newY = initialLocalPosition.y + Mathf.Sin(Time.time * bobbingFrequency) * bobbingAmplitude;
        transform.localPosition = new Vector3(initialLocalPosition.x, newY, initialLocalPosition.z);
    }
}
