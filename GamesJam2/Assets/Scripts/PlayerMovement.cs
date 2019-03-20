using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private static float EPSILON = 0.0001f;

    public int PlayerNumber
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

    [SerializeField] private float speed = 5;
    [SerializeField] [Range(0, 3)] private int playerNumber = 0;
    private Rigidbody rb;
    private string horizontalInputString;
    private string verticalInputString;
    private string moveDown;
    private string moveUp;
    private string moveUpDown;
    private string fire1;
    private bool moveHorizontal = true;
    private float yValue = 0;


    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        horizontalInputString = "Horizontal_P" + playerNumber;
        verticalInputString = "Vertical_P" + playerNumber;
        moveDown = "MoveDown_P" + playerNumber;
        moveUp = "MoveUp_P" + playerNumber;
        moveUpDown = "MoveUpDown_P0";
        fire1 = "Fire1_P" + playerNumber;
    }

    void Update()
    {
        float inputX = Input.GetAxis(horizontalInputString);
        float inputZ = Input.GetAxis(verticalInputString);

        yValue = moveHorizontal ? GetYValueInput() : 0;

        var movement = new Vector3(inputX, yValue, inputZ);
        rb.velocity = movement * speed;

        if (movement.magnitude > EPSILON)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 1f);
        }

        if (Input.GetButtonDown(fire1))
        {
            Debug.Log("bash" + playerNumber);
        }
    }

    private float GetYValueInput()
    {
        var yValue = 0f;
        if (playerNumber == 0 && Input.GetButton(moveUpDown))
        {
            yValue = Input.GetAxis(moveUpDown);
        }
        else if (Input.GetAxis(moveDown) < 0)
        {
            yValue = Input.GetAxis(moveDown);
        }
        else if (Input.GetAxis(moveUp) > 0)
        {
            yValue = Input.GetAxis(moveUp);
        }
        return yValue;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("HorizontalMovementTrigger"))
        {
            moveHorizontal = !moveHorizontal;
        }
    }
}
