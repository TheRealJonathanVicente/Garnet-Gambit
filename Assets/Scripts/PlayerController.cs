using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Vector3 moveDirection; //might need to make public

    

    [Header ("Hud Settings")]
    public Image staminaBar;
    public float stamina, maxStamina;
    public float runCost;
    public float chargeRate;
    private Coroutine recharge;
    
    [Header ("Movement Settings")]
    public float speed = 5f;
    public float sprintSpeed = 7f;
    public float jumpForce = 10f;

    [Header ("References")]
    public Rigidbody rb;
    public Camera playerCam;
    public Transform flashLight;
    public TextMeshProUGUI speedText;
    public AudioSource playerAudioSource; 

    private Vector3 lastPosition;

    [Header ("Ground settings")]
    public LayerMask groundLayer;
    public float groundDistance = 0.2f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        lastPosition = rb.position;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        RotateFlashlight();
        
        if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
        {
            Jump(); // When player jumps, if they let go they stop mid air, they could keep moving forward until they hit the ground
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

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;
        forward.y = 0;
        right.y = 0;
        forward = forward.normalized;
        right = right.normalized;

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        Vector3 forwardVertInput = verticalInput * forward;
        Vector3 rightHoriInput = horizontalInput * right;
        
        Vector3 moveDirectionRelCam = forwardVertInput + rightHoriInput;

        rb.MovePosition(transform.position + moveDirectionRelCam * speed * Time.fixedDeltaTime);

        if(moveDirectionRelCam.magnitude > 0)
        {
            if(!playerAudioSource.isPlaying)
            {
                playerAudioSource.Play();
            }
        }
        else
        {
            if(playerAudioSource.isPlaying)
            {
                playerAudioSource.Stop();
            }
        }
        

        if(Input.GetKey(KeyCode.LeftShift) && stamina > 0)
        {
            rb.MovePosition(transform.position + moveDirectionRelCam * sprintSpeed * Time.fixedDeltaTime);

            stamina -= runCost * Time.deltaTime; //scales stamina - runcost porpotionally over time, every 1 second runCost will be substracted
            if(stamina < 0){
                stamina = 0;
            }
            staminaBar.fillAmount = stamina / maxStamina;

            if(recharge != null){
                StopCoroutine(recharge);
            }
            recharge = StartCoroutine(RechargeStamina());
        }
    }
     void RotateFlashlight()
    {
        if (flashLight != null && playerCam != null)
        {
            // Match flashlight rotation with camera rotation
            flashLight.rotation = Quaternion.Euler(playerCam.transform.eulerAngles.x, playerCam.transform.eulerAngles.y, 0);
        }
    }

    void Jump()
    {
        Debug.Log("Is jumping");
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }
    
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundDistance, groundLayer);
    }

    private IEnumerator RechargeStamina(){
        yield return new WaitForSeconds(1f);
        while(stamina < maxStamina)
        {
            stamina += chargeRate / 10f;
            if(stamina > maxStamina){
                stamina = maxStamina;
            }
            staminaBar.fillAmount = stamina / maxStamina;
            yield return new WaitForSeconds(.1f);
        }
    }

}
