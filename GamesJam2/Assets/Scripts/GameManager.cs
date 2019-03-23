﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{


    private static GameObject INSTANCE;

    [SerializeField] private GameObject[] players = new GameObject[4];
    private int[] choosenColors = new int[4];
    [SerializeField] private MessagesSpawn messagesSpawn;
    [SerializeField] private float maxPoints;

    private float[] playerScores=new float[4];
    private int[] playerColors = new int[4];

    private void OnEnable()
    {
        PlayerManager.OnWin += CollectPlayerScores;
    }
    private void OnDisable()
    {
        PlayerManager.OnWin -= CollectPlayerScores;
    }

    private void Awake()
    {
        if (INSTANCE == null)
        {
            INSTANCE = gameObject;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    public void SetPlayer(int player, int colorNr)
    {
        choosenColors[player] = colorNr;
    }

    public void StartGame()
    {
        messagesSpawn = GameObject.Find("MessageSpawnPositions")?.GetComponent<MessagesSpawn>();
        var playerGO = GameObject.Find("Players");

        if (playerGO != null)
        {
            for (int i = 0; i < playerGO.transform.childCount; i++)
            {
                players[i] = playerGO.transform.GetChild(i).gameObject;
            }
        }

        messagesSpawn.players = new Transform[players.Length];

        for (int i = 0; i < choosenColors.Length; i++)
        {
            players[i].GetComponent<PlayerManager>().SetPlayerNumber(choosenColors[i]);
            players[i].GetComponent<PlayerMessageSystem>().CreateRecieverQueue(players.Length);
            messagesSpawn.players[i] = players[i].transform;
            messagesSpawn.SpawnMessage((CustomColors.Colors)choosenColors[i]);
        }

        //for (int i = 0; i < System.Enum.GetValues(typeof(CustomColors.Colors)).Length; i++)
        //{
        //    messagesSpawn.SpawnMessage((CustomColors.Colors)i);
        //}

        //for (int i = 0; i < players.Length; i++)
        //{
        //    players[i].GetComponent<PlayerManager>().SetPlayerNumber(i);  //TODO replace with player selection
        //    players[i].GetComponent<PlayerMessageSystem>().CreateRecieverQueue(players.Length);
        //    messagesSpawn.players[i] = players[i].transform;
        //}


        //for (int i = 0; i < System.Enum.GetValues(typeof(CustomColors.Colors)).Length; i++)
        //{
        //    messagesSpawn.SpawnMessage((CustomColors.Colors)i);
        //}
    }

    private void CollectPlayerScores()
    {
        for(int i=0;i<players.Length;i++)
        {
            PlayerManager currentPlayerManager = players[i].GetComponent<PlayerManager>();
            playerScores[i] = currentPlayerManager.points/maxPoints;
            playerColors[i] = currentPlayerManager.playerNumber;
        }

        SceneManager.LoadScene("GameOver");
    }

    public GameObject[] GetPlayers()
    {
        return players;
    }
    public int[] GetChosenColors()
    {
        return choosenColors;
    }
    public float[] GetScores()
    {
        return playerScores;
    }

    public float GetMaxPoints()
    {
        return maxPoints;
    }
}
