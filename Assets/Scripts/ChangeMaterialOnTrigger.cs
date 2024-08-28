using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialOnTrigger : MonoBehaviour
{
    public Material[] materials; // List of all materials to choose from
    private Renderer objectRenderer; // Renderer of the object

    private int oldIndex = 0;
    private int newIndex;
    private int excludeValue;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        objectRenderer.material = materials[oldIndex];
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Entered Trigger with tag: " + other.gameObject.tag);
        Debug.Log("Entered Trigger with tag: " + other.transform.root.gameObject.tag);

        int min = 0;
        int max = materials.Length;
        excludeValue = oldIndex;

        int newIndex = RandomRangeExclude(min, max, excludeValue);
        Debug.Log("Random value (excluding " + excludeValue + "): " + newIndex);


        if (other.gameObject.tag == "Trigger"){
            objectRenderer.material = materials[newIndex];
            Debug.Log("Changing material to " + materials[newIndex]);
            oldIndex = newIndex;
        }
    }

    int RandomRangeExclude(int min, int max, int excludeValue)
    {
        int randomValue = Random.Range(min, max - 1); // Generate a random number from min to max-1

        if (randomValue >= excludeValue)
        {
            randomValue += 1; // Adjust the random number to skip over the excludeValue
        }

        return randomValue;
    }
}
