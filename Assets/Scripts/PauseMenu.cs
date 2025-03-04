using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu; // Assign the pause menu UI panel in the Inspector
    public static bool isPaused; // Track the pause state

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false); // Ensure the pause menu is hidden at the start
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle pause when Escape is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    // Pauses the game and activates the pause menu
    public void PauseGame()
    {
        pauseMenu.SetActive(true); // Activate the pause menu
        Time.timeScale = 0f; // Pause the game
        isPaused = true; // Update the pause state

        // Unlock and show the cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Resumes the game and deactivates the pause menu
    public void ResumeGame()
    {
        pauseMenu.SetActive(false); // Deactivate the pause menu
        Time.timeScale = 1f; // Resume the game
        isPaused = false; // Update the pause state

        // Lock and hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Loads the main menu scene
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Ensure the game is not paused
        SceneManager.LoadScene("MainMenu"); // Load the main menu scene
    }

    // Quits the game
    public void QuitGame()
    {
        Application.Quit(); // Quit the application (works in standalone builds)
    }
}