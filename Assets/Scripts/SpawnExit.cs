using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnExit : MonoBehaviour
{
    public GameObject Exit;
    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("winCon value: " + McGuffin.winCon);

        if(McGuffin.winCon < 6)
        {
           Exit.SetActive(false);  
        }
        else
        {
            Exit.SetActive(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Load the "Win" scene
            Debug.Log("YopU Win");
            SceneManager.LoadScene("Win");
        }
    }
}
