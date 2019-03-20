using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] [Range(1, 4)] private int playerNumber = 1;

    int PlayerNumber
    {
        get
        {
            return playerNumber;
        }
        set
        {
            playerNumber = value;
        }
    }

    private Rigidbody rb;
    private string horizontalInputString;
    private string verticalInputString;
    private string fire1;
    private string fire2;
    private Vector3 originVec3Top;
    private Vector3 originVec3Down;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        horizontalInputString = "Horizontal_P" + playerNumber;
        verticalInputString = "Vertical_P" + playerNumber;
        fire1 = "Fire1_P" + playerNumber;
        fire2 = "Fire2_P" + playerNumber;
        originVec3Top = transform.position;
        originVec3Down = new Vector3(transform.position.x, -5f, transform.position.z);
    }

    void Update()
    {
        float inputX = Input.GetAxis(horizontalInputString);
        float inputZ = Input.GetAxis(verticalInputString);

        var movement = new Vector3(inputX, 0, inputZ);

        rb.velocity = movement * speed;

        if (Input.GetButton(fire1))
        {
            transform.position = originVec3Down;
        }

        if (Input.GetButton(fire2))
        {
            transform.position = originVec3Top;
        }
    }

}
