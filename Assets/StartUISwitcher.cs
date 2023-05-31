using UnityEngine;

public class StartUISwitcher : MonoBehaviour
{
    public GameObject startUI; // Reference to the start UI game object
    public GameObject settingsUI; // Reference to the settings UI game object

    private bool isStartUIActive = true; // Flag to keep track of the current active UI

    private void Start()
    {
        // Ensure only the start UI is initially active
        startUI.SetActive(true);
        settingsUI.SetActive(false);
    }

    public void ToggleUIState()
    {
        isStartUIActive = !isStartUIActive; // Toggle the UI flag

        // Enable and disable the appropriate UI game objects
        startUI.SetActive(isStartUIActive);
        settingsUI.SetActive(!isStartUIActive);
    }
}
