using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeToSpin : MonoBehaviour
{
public float rotationSpeed = 0.1f;  // Speed of rotation based on the swipe or mouse drag
    public string topHalfTag = "Platform1";  // Tag for objects that should rotate when swiping in the top half
    public string bottomHalfTag = "Platform2";  // Tag for objects that should rotate when swiping in the bottom half
    public bool canRotate = true;  // Controls whether rotation is allowed

    private Vector2 startTouchPosition;  // Position where the touch or mouse drag started
    private bool isSwiping = false;

    void Update()
    {
        if (!canRotate) return;  // Exit if rotation is not allowed

        // Screen height is divided into two halves
        float screenHeight = Screen.height;
        float halfScreenHeight = screenHeight / 2;

        // Handle touch input for swiping (mobile)
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Determine if the swipe is in the top or bottom half
            bool isTopHalf = touch.position.y > halfScreenHeight;

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    // Record the start position of the touch
                    startTouchPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    if (isSwiping)
                    {
                        // Calculate the swipe distance
                        float swipeDistance = touch.position.x - startTouchPosition.x;

                        // Determine the tag based on the swipe location
                        string targetTag = isTopHalf ? topHalfTag : bottomHalfTag;

                        // Get all objects with the specified tag
                        GameObject[] objectsToRotate = GameObject.FindGameObjectsWithTag(targetTag);

                        foreach (GameObject obj in objectsToRotate)
                        {
                            // Rotate the object around the Y-axis
                            obj.transform.Rotate(Vector3.up, -swipeDistance * rotationSpeed * Time.deltaTime);
                        }

                        // Update the start position for the next frame
                        startTouchPosition = touch.position;
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    // End the swipe
                    isSwiping = false;
                    break;
            }
        }

        // Handle mouse input for dragging (PC)
        if (Input.GetMouseButtonDown(0))
        {
            // Record the start position of the mouse drag
            startTouchPosition = Input.mousePosition;
            isSwiping = true;
        }
        else if (Input.GetMouseButton(0) && isSwiping)
        {
            // Calculate the drag distance
            float dragDistance = Input.mousePosition.x - startTouchPosition.x;

            // Determine if the drag is in the top or bottom half
            bool isTopHalf = Input.mousePosition.y > halfScreenHeight;

            // Determine the tag based on the drag location
            string targetTag = isTopHalf ? topHalfTag : bottomHalfTag;

            // Get all objects with the specified tag
            GameObject[] objectsToRotate = GameObject.FindGameObjectsWithTag(targetTag);

            foreach (GameObject obj in objectsToRotate)
            {
                // Rotate the object around the Y-axis
                obj.transform.Rotate(Vector3.up, -dragDistance * rotationSpeed * Time.deltaTime);
            }

            // Update the start position for the next frame
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // End the drag
            isSwiping = false;
        }
    }
}




// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class SwipeToSpin : MonoBehaviour
// {
//     public float rotationSpeed = 100f;  // Speed of rotation based on the swipe or mouse drag
//     public string topHalfTag = "Platform1";  // Tag for objects in the top half
//     public string bottomHalfTag = "Platform2";  // Tag for objects in the bottom half

//     private Vector2 startTouchPosition;  // Position where the touch or mouse drag started
//     private bool isSwiping = false;

//     void Update()
//     {
//         // Screen height is divided into two halves
//         float screenHeight = Screen.height;
//         float halfScreenHeight = screenHeight / 2;

//         // Handle touch input for swiping (mobile)
//         if (Input.touchCount > 0)
//         {
//             Touch touch = Input.GetTouch(0);

//             // Determine if the swipe is in the top or bottom half
//             bool isTopHalf = touch.position.y > halfScreenHeight;
//             Debug.Log("Touch detected in " + (isTopHalf ? "top" : "bottom") + " half of the screen.");

//             switch (touch.phase)
//             {
//                 case TouchPhase.Began:
//                     // Record the start position of the touch
//                     startTouchPosition = touch.position;
//                     isSwiping = true;
//                     break;

//                 case TouchPhase.Moved:
//                     if (isSwiping)
//                     {
//                         // Calculate the swipe distance
//                         float swipeDistance = touch.position.x - startTouchPosition.x;
//                         Debug.Log("Swipe distance: " + swipeDistance);

//                         if (isTopHalf)
//                         {
//                             RotateObjectsWithTag(topHalfTag, -swipeDistance);
//                         }
//                         else
//                         {
//                             RotateObjectsWithTag(bottomHalfTag, -swipeDistance);
//                         }

//                         // Update the start position for the next frame
//                         startTouchPosition = touch.position;
//                     }
//                     break;

//                 case TouchPhase.Ended:
//                 case TouchPhase.Canceled:
//                     // End the swipe
//                     isSwiping = false;
//                     break;
//             }
//         }

//         // Handle mouse input for dragging (PC)
//         if (Input.GetMouseButtonDown(0))
//         {
//             // Record the start position of the mouse drag
//             startTouchPosition = Input.mousePosition;
//             isSwiping = true;
//         }
//         else if (Input.GetMouseButton(0) && isSwiping)
//         {
//             // Calculate the drag distance
//             float dragDistance = Input.mousePosition.x - startTouchPosition.x;

//             // Determine if the drag is in the top or bottom half
//             bool isTopHalf = Input.mousePosition.y > halfScreenHeight;
//             Debug.Log("Mouse drag detected in " + (isTopHalf ? "top" : "bottom") + " half of the screen.");

//             if (isTopHalf)
//             {
//                 RotateObjectsWithTag(topHalfTag, -dragDistance);
//             }
//             else
//             {
//                 RotateObjectsWithTag(bottomHalfTag, -dragDistance);
//             }

//             // Update the start position for the next frame
//             startTouchPosition = Input.mousePosition;
//         }
//         else if (Input.GetMouseButtonUp(0))
//         {
//             // End the drag
//             isSwiping = false;
//         }
//     }

//     void RotateObjectsWithTag(string tag, float rotationAmount)
//     {
//         GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);
//         if (objectsWithTag.Length > 0)
//         {
//             foreach (GameObject obj in objectsWithTag)
//             {
//                 Debug.Log("Rotating object: " + obj.name);
//                 obj.transform.Rotate(Vector3.up, rotationAmount * rotationSpeed * Time.deltaTime);
//             }
//         }
//         else
//         {
//             Debug.LogWarning("No objects found with the tag: " + tag);
//         }
//     }
// }
