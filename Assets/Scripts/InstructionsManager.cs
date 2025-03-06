using UnityEngine;

public class InstructionsManager : MonoBehaviour
{
    public GameObject instructions; // Assign the instructions UI panel in the Inspector

    private void Start()
    {
        // Activate the instructions panel when the scene loads
        if (instructions != null)
        {
            instructions.SetActive(true);
            // Destroy the instructions panel after 5 seconds
            Destroy(instructions, 3.5f);
        }
        else
        {
            Debug.LogError("Instructions GameObject is not assigned!");
        }
    }
}