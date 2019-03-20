﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager[] players;
    [SerializeField] private MessagesSpawn messagesSpawn;

    // Start is called before the first frame update
    void Start()
    {
        messagesSpawn.players = new Transform[players.Length];
        for(int i=0;i<players.Length;i++)
        {
            players[i].SetPlayerNumber(i);  //TODO replace with player selection
            players[i].CreateRecieverQueue(players.Length);
            messagesSpawn.players[i] = players[i].transform;
        }

        
        for (int i = 0; i < System.Enum.GetValues(typeof(CustomColors.Colors)).Length; i++)
        {
            messagesSpawn.SpawnMessage((CustomColors.Colors)i);
        }
    }
    
}
