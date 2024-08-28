using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlatforms : MonoBehaviour
{
    public GameObject prefab;  // The default prefab to spawn
    public GameObject triggerPrefab;  // The trigger prefab to spawn every 5 prefabs
    public GameObject finalPrefab;  // The prefab to spawn at the end
    public Vector3 startPosition = new Vector3(0, 0, 0);  // The starting position for the first prefab
    public int numberOfPrefabs = 20;  // The number of prefabs to spawn
    public float yPositionDecrement = 2f;  // The decrement in Y position for each subsequent prefab
    public Vector3 spawnPositionOffset = new Vector3(0, -2, 0); // The Y position offset for each spawn

    private float previousYRotation = 0f;  // Track the Y rotation of the last spawned prefab

    void Start()
    {
        for (int i = 0; i < numberOfPrefabs; i++)
        {
            // Calculate the position for the current prefab
            Vector3 spawnPosition = startPosition + (i * spawnPositionOffset);

            float randomYRotation;
            do
            {
                // Randomly choose a Y rotation in increments of 30 degrees
                randomYRotation = Random.Range(0, 12) * 30f;  // 12 possible values: 0, 30, 60, ..., 330

                // Check if the new rotation is within 60 degrees of the previous rotation
            } while (Mathf.Abs(Mathf.DeltaAngle(previousYRotation, randomYRotation)) > 60f);

            // Update the previous Y rotation to the current one
            previousYRotation = randomYRotation;

            // Create a new rotation based on the calculated Y rotation
            Quaternion spawnRotation = Quaternion.Euler(0, randomYRotation, 0);

            // Determine whether to instantiate the regular prefab or the trigger prefab every 5th instance
            if ((i + 1) % 5 == 0)  // Every 5th prefab (e.g., 5th, 10th, 15th...)
            {
                Instantiate(triggerPrefab, spawnPosition, spawnRotation);
            }
            else
            {
                Instantiate(prefab, spawnPosition, spawnRotation);
            }
        }

        // Spawn the final prefab at the end
        Vector3 finalSpawnPosition = startPosition + (numberOfPrefabs * spawnPositionOffset); // Position after the last prefab
        Quaternion finalRotation = Quaternion.identity; // No rotation or you can customize if needed
        Instantiate(finalPrefab, finalSpawnPosition, finalRotation);
    }
}
