using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IInteractable
{
    public GameObject Item;
   // public AudioSource mcGuffinSound;
    public bool newBattery = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Interact()
    {
        Debug.Log("New battery");
        newBattery = true;
        Item.SetActive(false);
    }
}

