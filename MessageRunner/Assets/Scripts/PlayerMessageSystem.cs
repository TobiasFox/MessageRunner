using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMessageSystem : MonoBehaviour
{

    public bool isCarryingMessage { get; private set; }
    public int messageColor { get; private set; }

    [SerializeField] private MessagesSpawn messagesSpawn;
    [SerializeField] private CustomColors customColors;
    [SerializeField] private GameObject messageSymbol;
    [SerializeField] private float pointsForDelivery;
    [SerializeField] private AudioClip pickUpSound, deliverSound;

    private PlayerManager playerManager;
    private List<int> recieverQueue;
    private int queueCounter;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Awake()
    {
        playerManager = GetComponent<PlayerManager>();
        audioSource = GetComponent<AudioSource>();
    }

    public void CreateRecieverQueue(int playerCount)
    {
        recieverQueue = new List<int>();
        int randNum = 0;
        int tryCounter = 0;
        for (int i = 0; i < playerCount - 1; i++)
        {
            do
            {
                randNum = Random.Range(0, playerCount);
                tryCounter++;
            } while ((recieverQueue.Contains(randNum) || randNum == playerManager.playerNumber) && tryCounter < 100);

            recieverQueue.Add(randNum);
        }

    }

    public void PickUpMessage(Vector3 message)
    {
        audioSource.clip = pickUpSound;
        audioSource.Play();
        isCarryingMessage = true;
        messagesSpawn.FreePosition(message);
        messageSymbol.SetActive(true);
        Renderer rend = messageSymbol.GetComponent<Renderer>();
        messageColor = (int)DetermineReciever();
        rend.material.color = customColors.colors[messageColor];
    }

    private CustomColors.Colors DetermineReciever()
    {
        queueCounter = (queueCounter + 1) % recieverQueue.Count;
        return (CustomColors.Colors)recieverQueue[queueCounter];
    }

    public void DeliverMessage()
    {
        audioSource.clip = deliverSound;
        audioSource.Play();
        isCarryingMessage = false;
        playerManager.AddPoint(pointsForDelivery);
        messageSymbol.SetActive(false);
        messagesSpawn.SpawnMessage((CustomColors.Colors)playerManager.playerNumber);
    }
}
