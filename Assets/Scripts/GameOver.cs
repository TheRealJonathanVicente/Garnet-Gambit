using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("MainLevel");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
