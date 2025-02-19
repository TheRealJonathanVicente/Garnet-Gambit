using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 moveDirection;
    public Rigidbody rb;
    public Camera playerCam;
    public Transform flashLight;
    
    public LayerMask groundLayer;

    public float speed = 5f;
    public float sprintSpeed = 7f;
    public float jumpForce = 10f;
    public float groundDistance = 0.2f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        
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

        if(Input.GetKey(KeyCode.LeftShift))
        {
            rb.MovePosition(transform.position + moveDirectionRelCam * sprintSpeed * Time.fixedDeltaTime);
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

}
