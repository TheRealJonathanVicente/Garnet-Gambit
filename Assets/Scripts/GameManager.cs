using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject guardPrefab;
    public Transform[] spawnPositions; // Array of spawn positions for guards
    public GameObject mainPlayer;
    public GameObject canvas;
    public GameObject musicThing;

    public int amountOfGuards;

    [System.Serializable]
    public class GuardWaypoints
    {
        public Transform[] waypoints; // Waypoints for a single guard
    }

    public List<GuardWaypoints> waypointsList; // List of waypoint sets for each guard

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
             // Keeps the GameManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name == "ArtPrototype")
        {
            SpawnGuards();
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("ArtPrototype");
        DontDestroyOnLoad(canvas);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        
        SceneManager.LoadScene("Credits");
        
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Restart()
    {
        SceneManager.LoadScene("ArtPrototype");
    }
    

    public void SpawnGuards()
    {
        // Ensure the number of guards does not exceed the number of spawn positions
        int guardsToSpawn = Mathf.Min(amountOfGuards, spawnPositions.Length);

        for (int i = 0; i < guardsToSpawn; i++)
        {
            // Spawn a guard at the corresponding spawn position
            GameObject tempObject = Instantiate(guardPrefab, spawnPositions[i].position, Quaternion.identity);

            // Get the PathFinding and EnemyVision components
            PathFinding path = tempObject.GetComponent<PathFinding>();
            EnemyVision vision = tempObject.GetComponent<EnemyVision>();

            // Assign the player and waypoints to the guard
            if (path != null && i < waypointsList.Count) // Ensure there are waypoints for this guard
            {
                path.player = mainPlayer;
                path.waypoints = waypointsList[i].waypoints; // Assign waypoints for this guard
            }

            if (vision != null)
            {
                vision.Player = mainPlayer;
            }
        }
    }

    public void MenuQuit()
    {
      if (SceneManager.GetActiveScene().name == "ArtPrototype"   && Input.GetKeyDown(KeyCode.Escape))
      {
        QuitGame();
      }
    }
}