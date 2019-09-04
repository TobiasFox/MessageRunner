﻿using System;
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

    public bool EnableInput { get; set; } = true;

    [SerializeField] private float speed = 5;
    [SerializeField] [Range(0, 3)] private int playerNumber = 0;
    [SerializeField] private float stunTime = 1f;
    [SerializeField] private float bashSpeed = 350f;
    [SerializeField] private float looseEnergyPerStep;
    [SerializeField] private float looseEnergyPerBash;
    [SerializeField] private AudioClip bashSoundEffect;
    [SerializeField] private Camera mainCamera;

    private Rigidbody rb;
    private AudioSource audioSource;
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

    private Quaternion viewingDirection;
    private PlayerManager playerManager;

    void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        bashArea = GetComponentInChildren<BashZone>();
        audioSource = GetComponent<AudioSource>();

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
        if (!EnableInput)
        {
            StopMovement();
            return;
        }

        if (isStunned)
        {
            return;
        }
        else if (playerManager.energy <= 0)
        {
            transform.rotation = viewingDirection;
            rb.velocity = Vector3.zero;
            return;
        }

        float inputX = Input.GetAxis(horizontalInputString);
        float inputZ = Input.GetAxis(verticalInputString);

        yValue = GetYValueInput();

        float verticalPush = 1f;
        var movement2 = new Vector3(mainCamera.transform.forward.x * verticalPush * inputZ + mainCamera.transform.right.x * inputX,
            yValue,
            mainCamera.transform.forward.x * verticalPush * inputZ + mainCamera.transform.right.x * inputZ);

        if (movement2.magnitude > EPSILON)
        {
            rb.velocity = movement2 * speed;
            viewingDirection = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement2), 1f);
            transform.rotation = viewingDirection;
            playerManager.energy -= looseEnergyPerStep;
        }
        else
        {
            StopMovement();
        }

        if (Input.GetButtonDown(fire1))
        {
            BashPlayers();
        }
    }

    private void StopMovement()
    {
        transform.rotation = viewingDirection;
        rb.velocity = Vector3.zero;
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
            playerManager.energy -= looseEnergyPerBash;
            audioSource.clip = bashSoundEffect;
            audioSource.Play();
        }
    }

    private float GetYValueInput()
    {
        //Debug.Log("moveUp " + Input.GetAxis(moveUp) + ", moveDown: " + Input.GetAxis(moveDown));
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

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.tag.Equals("HorizontalMovementTrigger"))
    //    {
    //        moveHorizontal = true;
    //        Debug.Log("player: " + playerNumber + "moveHorizontal: " + moveHorizontal);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag.Equals("HorizontalMovementTrigger"))
    //    {
    //        moveHorizontal = false;
    //        Debug.Log("player: " + playerNumber + "moveHorizontal: " + moveHorizontal);
    //    }
    //}

    public IEnumerator SetStunned()
    {
        Debug.Log("player " + playerNumber + " is stunned");
        isStunned = true;
        yield return new WaitForSeconds(stunTime);
        isStunned = false;
        Debug.Log("player " + playerNumber + " isn't stunned anymore");
    }

    public int GetPlayerNumber()
    {
        return playerNumber;
    }
}