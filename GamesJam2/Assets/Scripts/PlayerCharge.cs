using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharge : MonoBehaviour
{
    public bool isChargeable { get; set; }

    [SerializeField] private float waitBeforeCharge;
    [SerializeField] private float chargePerSecond;
    [SerializeField] private AudioClip chargeClip;

    private PlayerManager playerManager;
    private AudioSource audioSource;
    private bool isCharging;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Charge_P"+playerManager.playerNumber) && isChargeable)
        {
            isCharging = true;
            StartCoroutine("Charge");
        }
        else if(Input.GetButtonUp("Charge_P" + playerManager.playerNumber))
        {
            isCharging = false;
            audioSource.loop = false;
            StopCoroutine("Charge");
        }
        else if(!isChargeable)
        {
            audioSource.loop = false;
            isCharging = false;
        }
    }

    private IEnumerator Charge()
    {
        float smootingTime = 1f;
        audioSource.clip = chargeClip;
        audioSource.loop = true;
        audioSource.Play();
        Debug.Log(chargePerSecond * smootingTime);
        yield return new WaitForSeconds(waitBeforeCharge);
        while(isCharging)
        {
            playerManager.energy += chargePerSecond * smootingTime;
            Debug.Log("energy: " + playerManager.energy);
            yield return new WaitForSeconds(smootingTime);
        }
        //audioSource.Stop();
        audioSource.loop = false;
    }
}
