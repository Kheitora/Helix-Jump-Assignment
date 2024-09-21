using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public GameObject canvasElement1;
    public GameObject canvasElement2;

    public void EnableLockingObjects(string playerTag){
        if(playerTag == "Player1"){
            canvasElement1.SetActive(true);
        }

        if(playerTag == "Player2"){
            canvasElement2.SetActive(true);
        }   
    }

    public void DisableLockingObjects(string playerTag){
        if(playerTag == "Player1"){
            canvasElement1.SetActive(false);
        }

        if(playerTag == "Player2"){
            canvasElement2.SetActive(false);
        }   
    }
}
