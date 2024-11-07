using System.Collections.Generic;
using UnityEngine;
using TMPro; // For TextMeshPro

public class ObjectCounter : MonoBehaviour
{
    public TextMeshPro counterText; // TextMeshPro for displaying the count and list
    public List<GameObject> targetParts; // List of specific parts to count
    private List<string> enteredParts = new List<string>(); // List to track names of entered parts in order
    private HashSet<GameObject> countedParts = new HashSet<GameObject>(); // To store counted parts without duplicates

    private int maxCount = 7; // Maximum count for completion

    void Start()
    {
        // Initialize the enteredParts list with empty entries
        for (int i = 0; i < maxCount; i++)
        {
            enteredParts.Add(""); // Adds empty strings to represent empty slots
        }
        UpdateCounterText();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is in the list of target parts and has not been counted yet
        if (targetParts.Contains(other.gameObject) && !countedParts.Contains(other.gameObject))
        {
            // Add the part to countedParts and update the list
            countedParts.Add(other.gameObject);
            enteredParts[countedParts.Count - 1] = other.gameObject.name; // Add name in order
            UpdateCounterText();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is in the list of counted parts
        if (countedParts.Contains(other.gameObject))
        {
            // Remove the part from countedParts and update the list
            int index = enteredParts.IndexOf(other.gameObject.name);
            if (index != -1)
            {
                enteredParts[index] = ""; // Clear the slot for this object
            }
            countedParts.Remove(other.gameObject);
            UpdateCounterText();
        }
    }

    private void UpdateCounterText()
    {
        // Update the counter text to show the current count and list of objects
        int count = countedParts.Count;

        string displayText = "Count: " + count + "\n";

        // Add each part's name to the display text in order
        for (int i = 0; i < maxCount; i++)
        {
            displayText += (i + 1) + ". " + (enteredParts[i] != "" ? enteredParts[i] : "[ ]") + "\n";
        }

        // Display "Complete" when all parts are counted
        if (count >= maxCount)
        {
            displayText += "Status: Complete";
        }

        counterText.text = displayText;
    }
}
