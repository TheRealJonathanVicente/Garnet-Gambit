using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTurn : MonoBehaviour
{
    public float x;
    public float y;
    public float sensitivity = 2.0f;

    public Vector2 turn;
    public Transform player;

    private float min = 80f;
    private float max = 80f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; 
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        TurnScreen();
    }

    void TurnScreen()
    {
        turn.x += Input.GetAxis("Mouse X") * sensitivity;
        turn.y += Input.GetAxis("Mouse Y") * sensitivity;

        turn.y = Mathf.Clamp(turn.y, -min, max); //Clamps y so it stays in min max bounds

        player.rotation = Quaternion.Euler(0,turn.x,0);
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
}
