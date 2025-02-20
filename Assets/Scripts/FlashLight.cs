using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLight : MonoBehaviour
{
    public GameObject fLight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            fLight.SetActive(!fLight.activeSelf);// togglers flashlight, True if active, flash if inactive !starts it on flash maybe?
        }
    }
}
