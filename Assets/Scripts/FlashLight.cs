using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashLight : MonoBehaviour
{
    public GameObject fLight;

    public Image batteryBar;
    public float batteryLife, maxBatteryLife;
    public float burnCost;
    //public float chargeRate;

    public bool replaceBattery = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && !replaceBattery)
        {
            fLight.SetActive(!fLight.activeSelf);// togglers flashlight, True if active, flash if inactive !starts it on flash maybe?
        }
         if(fLight.activeSelf)
            {
                batteryLife -= burnCost * Time.deltaTime;
                if(batteryLife < 0){
                    batteryLife = 0;
                    replaceBattery = true; 
                }
            }
            batteryBar.fillAmount = batteryLife / maxBatteryLife;
        if(replaceBattery)
        {
            fLight.SetActive(false);
        }
    }
}
