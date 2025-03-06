using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraTurn : MonoBehaviour
{
    [Header("Sensitivity Settings")]
    public float mouseSensitivity = 2.0f;
    public float controllerSensitivity = 100f; // Controller needs a higher value for smooth movement
    public float smoothing = 5f; // Smoothing factor for input

    public Transform player;

    private PlayerControls controls;
    private Vector2 lookInput;
    private Vector2 currentLookInput;
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

    void FixedUpdate()
    {
        SmoothTurnScreen();
    }

    void SmoothTurnScreen()
    {
        float mouseX, mouseY;

        // Check if using controller
        if (Gamepad.current != null && Gamepad.current.rightStick.ReadValue().magnitude > 0)
        {
            // If using a controller, apply controller sensitivity
            mouseX = lookInput.x * controllerSensitivity * Time.fixedDeltaTime;
            mouseY = lookInput.y * controllerSensitivity * Time.fixedDeltaTime;
        }
        else
        {
            // If using a mouse, apply mouse sensitivity
            mouseX = lookInput.x * mouseSensitivity;
            mouseY = lookInput.y * mouseSensitivity;
        }

        // Smooth the input using Lerp for both mouse and controller inputs
        currentLookInput.x = Mathf.Lerp(currentLookInput.x, mouseX, smoothing * Time.fixedDeltaTime);
        currentLookInput.y = Mathf.Lerp(currentLookInput.y, mouseY, smoothing * Time.fixedDeltaTime);

        // Rotate the player body on the Y-axis only (left/right movement)
        player.Rotate(Vector3.up * currentLookInput.x);

        // Rotate the camera on the X-axis (up/down movement)
        xRotation -= currentLookInput.y;
        xRotation = Mathf.Clamp(xRotation, -min, max); // Limit vertical rotation
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
