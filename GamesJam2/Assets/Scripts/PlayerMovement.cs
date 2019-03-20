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
    private bool moveHorizontal = false;
    private float yValue = 0;
    private BashZone bashArea;
    private bool isStunned = false;
    [SerializeField] private float stunTime = 1f;
    [SerializeField] private float bashSpeed = 350f;

    void Awake()
    {
        bashArea = GetComponentInChildren<BashZone>();

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
        if (isStunned)
        {
            return;
        }

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
            BashPlayers();
        }
    }

    private void BashPlayers()
    {
        Debug.Log("bash from " + playerNumber);

        foreach (var go in bashArea.GetPlayers())
        {
            if (go == null)
            {
                continue;
            }

            var otherRB = go.GetComponent<Rigidbody>();
            var otherPlayerMovement = go.GetComponent<PlayerMovement>();
            if (otherRB != null && otherPlayerMovement != null)
            {
                StartCoroutine(otherPlayerMovement.SetStunned());
                otherRB.AddForce(transform.forward * bashSpeed);
            }
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
            Debug.Log("player: " + playerNumber + "moveHorizontal: " + moveHorizontal);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("HorizontalMovementTrigger"))
        {
            moveHorizontal = !moveHorizontal;
            Debug.Log("player: " + playerNumber + "moveHorizontal: " + moveHorizontal);
        }
    }

    public IEnumerator SetStunned()
    {
        Debug.Log("player " + playerNumber + " is stunned");
        isStunned = true;
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
        Debug.Log("player " + playerNumber + " isn't stunned anymore");
    }
}
