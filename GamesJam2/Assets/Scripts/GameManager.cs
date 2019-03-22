using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameObject INSTANCE;

    [SerializeField] private GameObject[] players = new GameObject[4];
    private Color[] choosenColors = new Color[4];
    [SerializeField] private MessagesSpawn messagesSpawn;


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

    public void SetPlayer(int player, Color color)
    {
        choosenColors[player] = color;
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
            players[i].GetComponent<PlayerManager>().SetPlayerNumber(i);
            players[i].GetComponent<PlayerMessageSystem>().CreateRecieverQueue(players.Length);
            messagesSpawn.players[i] = players[i].transform;
            messagesSpawn.SpawnMessage((CustomColors.Colors)i);
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

}
