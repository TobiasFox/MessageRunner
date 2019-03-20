using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] [Range(1, 4)] private int playerNumber;

    private Rigidbody rb;
    private string horizontalInputString;
    private string verticalInputString;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        horizontalInputString = "Horizontal_P" + playerNumber;
        verticalInputString = "Vertical_P" + playerNumber;
    }

    void Update()
    {
        float inputX = Input.GetAxis(horizontalInputString);
        float inputZ = Input.GetAxis(verticalInputString);

        var movement = new Vector3(inputX, 0, inputZ);

        rb.velocity = movement * speed;
    }
}
