using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import SceneManager

public class ReloadScene : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) // Use OnTriggerEnter for 3D collisions
    {
        if (other.CompareTag("Player")) // Check if the colliding object is the player
        {
            ReloadGame();
        }
    }

    void ReloadGame()
    {
        SceneManager.LoadScene("GameOver"); // Reload current scene
    }
}
