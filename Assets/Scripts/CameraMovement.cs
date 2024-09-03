using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;  // The object that the camera should follow
    public Vector3 offset;  // Offset position from the target
    public Vector3 tiltAngle;  // The tilt angle of the camera (e.g., x = -10 for a slight downward tilt)

    private float lowestYPosition;  // The lowest Y position the camera has reached

    void Start(){
        if (target == null){
            Debug.LogError("Target not set for CameraFollowDownOnlyWithTilt script. Please assign a target.");
            return;
        }

        // Set the camera's initial position based on the target's position and the offset
        Vector3 initialPosition = target.position + offset;
        transform.position = initialPosition;

        // Set the camera's rotation to include the desired tilt
        transform.rotation = Quaternion.Euler(tiltAngle);

        // Initialize the lowest Y position with the initial camera position
        lowestYPosition = initialPosition.y;
    }

    void LateUpdate(){
        if (target == null)
            return;

        // Calculate the desired position of the camera based on the target's position and the offset
        Vector3 desiredPosition = target.position + offset;

        // Check if the target's Y position is lower than the current lowest point
        if (desiredPosition.y < lowestYPosition){
            // Update the lowest point
            lowestYPosition = desiredPosition.y;
        }

        // Set the new camera position, only moving down if the target has moved lower
        Vector3 newPosition = new Vector3(desiredPosition.x, lowestYPosition, desiredPosition.z);

        // Update the camera's position instantly
        transform.position = newPosition;

        // Ensure the camera maintains its tilt towards the target
        transform.rotation = Quaternion.Euler(tiltAngle);
    }
}
