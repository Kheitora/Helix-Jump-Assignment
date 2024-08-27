using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    public GameObject prefab;  // The prefab to spawn
    public Vector3 startPosition = new Vector3(0, 0, 0);  // The starting position for the first prefab
    public int numberOfPrefabs = 20;  // The number of prefabs to spawn
    public float yPositionDecrement = 2f;  // The decrement in Y position for each subsequent prefab
    public Vector3 spawnPositionOffset = new Vector3(0, -2, 0); // The Y position offset for each spawn

    void Start(){
        for (int i = 0; i < numberOfPrefabs; i++){
            // Calculate the position for the current prefab
            Vector3 spawnPosition = startPosition + (i * spawnPositionOffset);

            // Randomly choose a Y rotation in increments of 30 degrees
            float randomYRotation = Random.Range(0, 12) * 30f;  // 12 possible values: 0, 30, 60, ..., 330

            // Create a new rotation based on the random Y rotation
            Quaternion spawnRotation = Quaternion.Euler(0, randomYRotation, 0);

            // Instantiate the prefab at the calculated position and rotation
            Instantiate(prefab, spawnPosition, spawnRotation);
        }
    }
}
