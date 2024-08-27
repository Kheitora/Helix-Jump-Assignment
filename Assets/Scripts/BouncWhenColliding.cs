using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncWhenColliding : MonoBehaviour
{
    public string targetTag; // Tag of the object that this object should bounce off
    public float bounceForce = 10f; // The force with which the object will bounce upwards

    private Rigidbody rb;

    void Start(){
        // Get the Rigidbody component of the object
        rb = GetComponent<Rigidbody>();

        // Ensure the object has a Rigidbody component
        if (rb == null){
            Debug.LogError("No Rigidbody found on the object. Please add a Rigidbody component.");
        }
    }

    void OnCollisionEnter(Collision collision){
        // Check if the collided object has the specified target tag
        if (collision.gameObject.CompareTag(targetTag)){
            // Ensure the object is always moving upwards by setting the velocity directly
            Vector3 newVelocity = rb.velocity;
            newVelocity.y = bounceForce; // Set the Y velocity to the bounce force
            rb.velocity = newVelocity;
        }
    }
}
