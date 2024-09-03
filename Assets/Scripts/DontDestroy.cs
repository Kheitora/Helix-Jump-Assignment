using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private void Awake()
    {
        // Ensure this GameObject is not destroyed when loading a new scene
        DontDestroyOnLoad(gameObject);
        
        // If a duplicate of this object exists, destroy it to prevent multiple instances
        if (FindObjectsOfType<DontDestroy>().Length > 1){
            Destroy(gameObject);
        }
    }
}
