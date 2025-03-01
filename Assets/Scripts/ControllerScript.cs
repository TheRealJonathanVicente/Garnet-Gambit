using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerScript : MonoBehaviour
{
    PlayerControls controls; 
    FlashLight light;
    
    void Awake()
    {
        light = GetComponent<FlashLight>();
        controls = new PlayerControls();

        controls.Gameplay.Flashlight.performed += ctx => light.ToggleFlash();
    }
    
    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }
}
