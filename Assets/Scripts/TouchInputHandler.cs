using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TouchInputHandler : MonoBehaviour
{
    public static event Action<string, float> OnRotate;  // Event to broadcast rotation

    public string topHalfTag = "Platform1";  // Tag for objects that should rotate when swiping in the top half
    public string bottomHalfTag = "Platform2";  // Tag for objects that should rotate when swiping in the bottom half
    public CanvasManager canvasManager;

    private Dictionary<int, TouchData> activeTouches = new Dictionary<int, TouchData>();
    private Vector2 previousMousePosition;
    private bool isMouseDragging = false;
    private string currentMouseSideTag = "";

    private Dictionary<string, bool> rotationLocks = new Dictionary<string, bool>
    {
        { "Platform1", false },  // Platform1's rotation is not locked by default
        { "Platform2", false }   // Platform2's rotation is not locked by default
    };

    void Update()
    {
        HandleTouches();
        HandleMouseInput();
    }

    void HandleTouches()
    {
        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch touch = Input.GetTouch(i);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    string sideTag = DetermineSide(touch.position.y);
                    activeTouches[touch.fingerId] = new TouchData(touch.fingerId, touch.position, touch.phase, sideTag);
                    break;

                case TouchPhase.Moved:
                    if (activeTouches.ContainsKey(touch.fingerId))
                    {
                        TouchData touchData = activeTouches[touch.fingerId];

                        // Check if the rotation is locked for this platform
                        if (!rotationLocks[touchData.sideTag])
                        {
                            float movement = touch.position.x - touchData.previousPosition.x;
                            OnRotate?.Invoke(touchData.sideTag, movement);
                        }

                        touchData.Update(touch.position, touch.phase);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (activeTouches.ContainsKey(touch.fingerId))
                    {
                        activeTouches.Remove(touch.fingerId);
                    }
                    break;
            }
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0)) // Mouse button pressed
        {
            previousMousePosition = Input.mousePosition;
            isMouseDragging = true;
            currentMouseSideTag = DetermineSide(Input.mousePosition.y);
        }
        else if (Input.GetMouseButton(0) && isMouseDragging) // Mouse is being dragged
        {
            // Check if the rotation is locked for this platform
            if (!rotationLocks[currentMouseSideTag])
            {
                Vector2 currentMousePosition = Input.mousePosition;
                float movement = currentMousePosition.x - previousMousePosition.x;
                OnRotate?.Invoke(currentMouseSideTag, movement);
                previousMousePosition = currentMousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0)) // Mouse button released
        {
            isMouseDragging = false;
        }
    }

    string DetermineSide(float yPosition){
        float halfScreenHeight = Screen.height / 2;
        return yPosition > halfScreenHeight ? topHalfTag : bottomHalfTag;
    }

    public void LockRotation(string platformTag)
    {
        if (rotationLocks.ContainsKey(platformTag))
        {
            rotationLocks[platformTag] = true;
        }
    }

    public void UnlockRotation(string platformTag)
    {
        if (rotationLocks.ContainsKey(platformTag))
        {
            rotationLocks[platformTag] = false;
        }
    }

    public void StartUnlockRotationCoroutine(string platformTag, string playerTag, float delay)
    {
        StartCoroutine(UnlockRotationAfterDelay(platformTag, playerTag, delay));
    }

    private IEnumerator UnlockRotationAfterDelay(string platformTag, string playerTag, float delay)
    {
        yield return new WaitForSeconds(delay);
        UnlockRotation(platformTag);
        canvasManager.DisableLockingObjects(playerTag);
        Debug.Log($"Rotation unlocked for {platformTag} after {delay} seconds");
    }
}

public class TouchData
{
    public int fingerId;
    public Vector2 previousPosition;
    public TouchPhase phase;
    public string sideTag;

    public TouchData(int fingerId, Vector2 startPosition, TouchPhase phase, string sideTag)
    {
        this.fingerId = fingerId;
        this.previousPosition = startPosition;
        this.phase = phase;
        this.sideTag = sideTag;
    }

    public void Update(Vector2 newPosition, TouchPhase newPhase)
    {
        previousPosition = newPosition;
        phase = newPhase;
    }
}
