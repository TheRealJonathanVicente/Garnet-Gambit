using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTurn : MonoBehaviour
{
    [Header("Sensitivity Settings")]
    public float mouseSensitivity = 2.0f;
    public float controllerSensitivity = 100f; // Controller needs a higher value for smooth movement

    public Transform player;

    private PlayerControls controls;
    private Vector2 lookInput;
    private float xRotation = 0f;

    private float min = 80f;
    private float max = 80f;

    void Awake()
    {
        controls = new PlayerControls();

        // Assign camera look input (Mouse & Right Stick)
        controls.Gameplay.Look.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Look.canceled += ctx => lookInput = Vector2.zero;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        TurnScreen();
    }

    void TurnScreen()
    {
        float mouseX, mouseY;

        if (Gamepad.current != null && Gamepad.current.rightStick.ReadValue().magnitude > 0)
        {
            // If using a controller, apply controller sensitivity
            mouseX = lookInput.x * controllerSensitivity * Time.deltaTime;
            mouseY = lookInput.y * controllerSensitivity * Time.deltaTime;
        }
        else
        {
            // If using a mouse, apply mouse sensitivity
            mouseX = lookInput.x * mouseSensitivity;
            mouseY = lookInput.y * mouseSensitivity;
        }

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -min, max); // Limit vertical rotation

        player.rotation *= Quaternion.Euler(0, mouseX, 0);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
