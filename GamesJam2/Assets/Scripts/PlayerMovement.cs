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
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown(fire1))
        {
            PlaneSwitch planeSwitch = other.GetComponent<PlaneSwitch>();
            if (planeSwitch != null && planeSwitch.GetPlaneSwitchDown() != null)
            {
                var yValue = other.GetComponent<PlaneSwitch>().GetPlaneSwitchDown().position.y;
                var newPosition = new Vector3(transform.position.x, yValue + .5f, transform.position.z);
                Debug.Log("newPosition: " + newPosition);
                transform.position = newPosition;
            }
        }

        if (Input.GetButtonDown(fire2))
        {
            PlaneSwitch planeSwitch = other.GetComponent<PlaneSwitch>();
            if (planeSwitch != null && planeSwitch.GetPlaneSwitchTop() != null)
            {
                var yValue = other.GetComponent<PlaneSwitch>().GetPlaneSwitchTop().position.y;
                var newPosition = new Vector3(transform.position.x, yValue + .5f, transform.position.z);
                Debug.Log("newPosition: " + newPosition);
                transform.position = newPosition;
            }
        }

    }

}
