﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BashZone : MonoBehaviour
{
    private GameObject[] players = new GameObject[4];
    private int playerCount = 0;

    public GameObject[] GetPlayers()
    {
        return players;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            players[playerCount++] = other.transform.parent.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] == other.transform.parent.gameObject)
                {
                    playerCount--;
                    players[i] = null;
                }
            }
        }
    }
}