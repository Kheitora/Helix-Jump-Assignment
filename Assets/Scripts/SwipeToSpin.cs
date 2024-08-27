using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeToSpin : MonoBehaviour
{
    public float rotationSpeed = 100f;  // Speed of rotation based on the swipe or mouse drag

    private Vector2 startTouchPosition;  // Position where the touch or mouse drag started
    private bool isSwiping = false;

    void Update(){
        // Handle touch input for swiping (mobile)
        if (Input.touchCount > 0){
            Touch touch = Input.GetTouch(0);

            switch (touch.phase){
                case TouchPhase.Began:
                    // Record the start position of the touch
                    startTouchPosition = touch.position;
                    isSwiping = true;
                    break;

                case TouchPhase.Moved:
                    if (isSwiping){
                        // Calculate the swipe distance
                        float swipeDistance = touch.position.x - startTouchPosition.x;

                        // Rotate the target object around the Y-axis based on the swipe distance
                        transform.Rotate(Vector3.up, -swipeDistance * rotationSpeed * Time.deltaTime);

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
        if (Input.GetMouseButtonDown(0)){
            // Record the start position of the mouse drag
            startTouchPosition = Input.mousePosition;
            isSwiping = true;
        }
        else if (Input.GetMouseButton(0) && isSwiping){
            // Calculate the drag distance
            float dragDistance = Input.mousePosition.x - startTouchPosition.x;

            // Rotate the target object around the Y-axis based on the drag distance
            transform.Rotate(Vector3.up, -dragDistance * rotationSpeed * Time.deltaTime);

            // Update the start position for the next frame
            startTouchPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0)){
            // End the drag
            isSwiping = false;
        }
    }
}
