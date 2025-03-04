using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject guardPrefab;
    public Transform spawnPosition;
    public GameObject mainPlayer;

    public int amountOfGuards;
   // private List <GameObjects> guardList;  fix
    public Transform[] newPoints;
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject); //Keeps the GameManager across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if(SceneManager.GetActiveScene().name == "Hallway With Guard");
        {
            //SpawnGuard();
        }
    }
   public void PlayGame()
    {
        SceneManager.LoadScene("Hallway With Guard");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    
    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

   /* public void SpawnGuard()
    {
        List <GameObject> guardList = new List <GameObject>()
        {
            GameObject tempObject = Instantiate(guardPrefab, spawnPosition.position, Quaternion.identity);
        }
        //GameObject tempObject = Instantiate(guardPrefab, spawnPosition.position, Quaternion.identity);

        PathFinding path = tempObject.GetComponent<PathFinding>();
        EnemyVision vision = tempObject.GetComponent<EnemyVision>();



        if(path != null)
        {
            path.player = mainPlayer;
            path.waypoints  = newPoints;
        }

        if(vision != null)
        {
            vision.Player = mainPlayer;
        }
    }*/
}
