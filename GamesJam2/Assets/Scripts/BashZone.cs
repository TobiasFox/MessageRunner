using System.Collections;
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
            players[playerCount++] = other.gameObject;
            Debug.Log("added " + other.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag.Equals("Player"))
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] == other.gameObject)
                {
                    playerCount--;
                    players[i] = null;
                    Debug.Log("remove " + other.name);
                }
            }
        }
    }
}
