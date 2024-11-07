using UnityEngine;
using TMPro;

public class ShowObjectNameOnDrag : MonoBehaviour
{
    public TextMeshPro textDisplay;
    private Vector3 lastPosition;
    private bool isDragging = false;
    private bool textShown = false; // Track if the text is already shown

    void Start()
    {
        lastPosition = transform.position;
        if (textDisplay != null)
        {
            textDisplay.gameObject.SetActive(false); // Hide text initially
        }
    }

    void Update()
    {
        // Check if the object has moved from the last position
        if (transform.position != lastPosition)
        {
            if (!isDragging)
            {
                ShowTextOnce(gameObject.name); // Show text only once when dragging starts
                isDragging = true;
            }
        }
        else if (isDragging)
        {
            // Stop dragging and hide text when movement stops
            HideText();
            isDragging = false;
            textShown = false; // Reset the text shown flag for the next drag
        }

        // Update last position for the next frame
        lastPosition = transform.position;
    }

    private void ShowTextOnce(string message)
    {
        if (textDisplay != null && !textShown) // Only show if not already shown
        {
            textDisplay.text = message;
            textDisplay.gameObject.SetActive(true); // Display text
            textShown = true; // Set flag to prevent repeated showing
        }
    }

    private void HideText()
    {
        if (textDisplay != null)
        {
            textDisplay.gameObject.SetActive(false); // Hide text
        }
    }
}
