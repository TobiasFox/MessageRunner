using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private PoolBehaviour poolBehaviour;


    private PlayerMessageSystem playerMessageSystem;
    private PlayerCharge playerCharge;

    // Start is called before the first frame update
    void Start()
    {
        playerMessageSystem=GetComponent<PlayerMessageSystem>();
        playerCharge = GetComponent<PlayerCharge>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Message")
        {
            Debug.Log(collision);
            playerMessageSystem.PickUpMessage(collision.transform.position);
            poolBehaviour.ReleaseObject(collision.gameObject);
        }

        if (collision.transform.tag == "Player" && playerMessageSystem.isCarryingMessage)
        {
            PlayerManager otherPlayerManager = collision.gameObject.GetComponent<PlayerManager>();
            if (otherPlayerManager.playerNumber == playerMessageSystem.messageColor)
            {
                playerMessageSystem.DeliverMessage();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="PowerStation")
        {
            playerCharge.isChargeable = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "PowerStation")
        {
            playerCharge.isChargeable = false;
        }
    }
}
