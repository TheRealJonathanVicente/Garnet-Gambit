using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Vector3 moveDirection;
    public Rigidbody rb;
    public Camera playerCam;

    public float speed = 5f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void FixedUpdate()
    {
        
    }

    void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput);

        moveDirection = playerCam.transform.TransformDirection(moveDirection);
        moveDirection.y = 0;

        moveDirection.Normalize();

        transform.Translate(moveDirection * speed * Time.deltaTime, Space.Self);
    }

}
