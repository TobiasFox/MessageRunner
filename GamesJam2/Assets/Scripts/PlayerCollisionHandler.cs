using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private PoolBehaviour poolBehaviour;


    private PlayerManager playerManager;

    // Start is called before the first frame update
    void Start()
    {
        playerManager=GetComponent<PlayerManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //if(other.tag=="Message")
        //{
        //    Debug.Log(other);
        //    playerManager.PickUpMessage(other.transform.position);
        //    poolBehaviour.ReleaseObject(other.gameObject);
        //}
        
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Message")
        {
            Debug.Log(collision);
            playerManager.PickUpMessage(collision.transform.position);
            poolBehaviour.ReleaseObject(collision.gameObject);
        }

        if (collision.transform.tag == "Player" && playerManager.isCarryingMessage)
        {
            PlayerManager otherPlayerManager = collision.gameObject.GetComponent<PlayerManager>();
            if (otherPlayerManager.playerNumber == playerManager.messageColor)
            {
                playerManager.DeliverMessage();
            }
        }
    }
}
