using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static event Action<string, float> OnRotate;  // Event to broadcast rotation

    public string wasdTag = "Platform1";  // Tag for objects controlled by WASD
    public string arrowTag = "Platform2";  // Tag for objects controlled by Arrow keys

    void Update()
    {
        HandleWASDInput();
        HandleArrowKeyInput();
    }

    void HandleWASDInput()
    {
        float movement = 0f;

        // WASD Controls
        if (Input.GetKey(KeyCode.A))
        {
            movement = -10f;  // Rotate left
        }
        else if (Input.GetKey(KeyCode.D))
        {
            movement = 10f;   // Rotate right
        }

        if (movement != 0)
        {
            OnRotate?.Invoke(wasdTag, movement);
        }
    }

    void HandleArrowKeyInput()
    {
        float movement = 0f;

        // Arrow Key Controls
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement = -10f;  // Rotate left
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            movement = 10f;   // Rotate right
        }

        if (movement != 0)
        {
            OnRotate?.Invoke(arrowTag, movement);
        }
    }
}
