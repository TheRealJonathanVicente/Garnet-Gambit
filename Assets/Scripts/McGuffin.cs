using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class McGuffin : MonoBehaviour, IInteractable
{
    public GameObject Item;
    public AudioSource mcGuffinSound;
    public static int winCon = 0; 
  
    public void Interact()
    {
        mcGuffinSound.Play();
        Debug.Log("Found the McGuffin!");
        winCon += 1;
        Debug.Log(winCon);
        Item.SetActive(false);
        
    }
}
