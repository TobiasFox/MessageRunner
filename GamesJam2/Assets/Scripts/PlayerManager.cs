using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public int playerNumber { get; private set; }
    public bool isCarryingMessage { get; private set; }
    public int messageColor { get; private set; }
    public float points { get; set; }

    [SerializeField] private MessagesSpawn messagesSpawn;
    [SerializeField] private CustomColors customColors;
    [SerializeField] private GameObject messageSymbol;
    [SerializeField] private float pointsForDelivery;

    private PlayerMovement playerMovement;
    private List<int> recieverQueue;
    private int queueCounter;
    private int startPlayerLayer;

    private void Awake()
    {
        playerMovement=GetComponent<PlayerMovement>();
        startPlayerLayer = LayerMask.NameToLayer("Blue");
    }

    public void CreateRecieverQueue(int playerCount)
    {
        recieverQueue = new List<int>();
        int randNum = 0;
        int tryCounter = 0;
        for(int i=0;i<playerCount-1;i++)
        {
            do
            {
                randNum = Random.Range(0, playerCount);
                tryCounter++;
            } while ((recieverQueue.Contains(randNum) || randNum==playerNumber) && tryCounter<100);

            recieverQueue.Add(randNum);
        }
        
    }

    public void SetPlayerNumber(int number)
    {
        Renderer rend = GetComponent<Renderer>();
        rend.material.color = customColors.colors[number];
        playerMovement.PlayerNumber = number;
        playerNumber = number;

        gameObject.layer = startPlayerLayer + number;
    }

    public void PickUpMessage(Vector3 message)
    {
        isCarryingMessage = true;
        messagesSpawn.FreePosition(message);
        messageSymbol.SetActive(true);
        Renderer rend = messageSymbol.GetComponent<Renderer>();
        messageColor = (int)DetermineReciever();
        rend.material.color = customColors.colors[messageColor];
    }

    private CustomColors.Colors DetermineReciever()
    {
        queueCounter=(queueCounter+1)%recieverQueue.Count;
        return (CustomColors.Colors)recieverQueue[queueCounter];
    }

    public void DeliverMessage()
    {
        isCarryingMessage = false;
        points += pointsForDelivery;
        messageSymbol.SetActive(false);
        messagesSpawn.SpawnMessage((CustomColors.Colors)playerNumber);
    }
}
