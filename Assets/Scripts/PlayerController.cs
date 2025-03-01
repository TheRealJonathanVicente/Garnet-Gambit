using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveDirection;
    private PlayerControls controls; // New Input System
    
    [Header("Hud Settings")]
    public Image staminaBar;
    public float stamina, maxStamina;
    public float runCost;
    public float chargeRate;
    private Coroutine recharge;

    [Header("Movement Settings")]
    public float speed = 5f;
    public float sprintSpeed = 7f;
    public float jumpForce = 10f;

    [Header("References")]
    public Rigidbody rb;
    public Camera playerCam;
    public Transform flashLight;
    public TextMeshProUGUI speedText;
    public AudioSource playerAudioSource;

    private Vector3 lastPosition;
    private Vector2 movementInput; // Stores movement direction from Input System
    private bool isJumping;
    private bool isSprinting;

    [Header("Ground settings")]
    public LayerMask groundLayer;
    public float groundDistance = 0.2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new PlayerControls(); // Initialize PlayerControls

        // Assign movement input
        controls.Gameplay.Movement.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => movementInput = Vector2.zero;

        // Jump input
        controls.Gameplay.Jump.performed += ctx => isJumping = true;
        controls.Gameplay.Jump.canceled += ctx => isJumping = false;

        // Sprint input (Works for Left Shift & Controller LT/L2)
        controls.Gameplay.Sprint.performed += ctx => isSprinting = true;
        controls.Gameplay.Sprint.canceled += ctx => isSprinting = false;
    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    void Update()
    {
        RotateFlashlight();
        if (IsGrounded() && isJumping)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        Move();

        float tempSpeed = ((rb.position - lastPosition).magnitude) / Time.fixedDeltaTime;
        lastPosition = rb.position;
        speedText.text = "Speed: " + tempSpeed.ToString("F2");
    }

    void Move()
    {
        float horizontalInput = movementInput.x;
        float verticalInput = movementInput.y;

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        Vector3 moveDirectionRelCam = (forward * verticalInput + right * horizontalInput).normalized;

        float currentSpeed = isSprinting && stamina > 0 ? sprintSpeed : speed;
        rb.MovePosition(transform.position + moveDirectionRelCam * currentSpeed * Time.fixedDeltaTime);

        if (moveDirectionRelCam.magnitude > 0)
        {
            if (!playerAudioSource.isPlaying) playerAudioSource.Play();
        }
        else
        {
            if (playerAudioSource.isPlaying) playerAudioSource.Stop();
        }

        // Sprint logic
        if (isSprinting && stamina > 0)
        {
            stamina -= runCost * Time.deltaTime;
            if (stamina < 0) stamina = 0;
            staminaBar.fillAmount = stamina / maxStamina;

            if (recharge != null) StopCoroutine(recharge);
            recharge = StartCoroutine(RechargeStamina());
        }
    }

    void RotateFlashlight()
    {
        if (flashLight != null && playerCam != null)
        {
            flashLight.rotation = Quaternion.Euler(playerCam.transform.eulerAngles.x, playerCam.transform.eulerAngles.y, 0);
        }
    }

    void Jump()
    {
        Debug.Log("Jumping");
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDistance, groundLayer);
    }

    private IEnumerator RechargeStamina()
    {
        yield return new WaitForSeconds(1f);
        while (stamina < maxStamina)
        {
            stamina += chargeRate / 10f;
            if (stamina > maxStamina) stamina = maxStamina;
            staminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
