using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public int playerNumber { get; private set; }
    public float points { get; set; }

    [SerializeField] private CustomColors customColors;
    [SerializeField] private float maxPoints;
    [SerializeField] private Image pointsImage;
    [SerializeField] private float maxEnergy;

    private PlayerMovement playerMovement;
    private int startPlayerLayer;
    private ParticleSystem energyParticleSystem;
    private float _energy;

    private void Awake()
    {
        playerMovement=GetComponent<PlayerMovement>();
        energyParticleSystem = GetComponentInChildren<ParticleSystem>();
        startPlayerLayer = LayerMask.NameToLayer("Blue");

        _energy = maxEnergy;
    }
    
    public void SetPlayerNumber(int number)
    {
        playerNumber = number;
        playerMovement.PlayerNumber = number;
        Renderer rend = GetComponent<Renderer>();           //color of player
        rend.material.color = customColors.colors[number];
        pointsImage.color = customColors.colors[number];    //color of points
        var particleMain = energyParticleSystem.main;       //color of energy particle system
        particleMain.startColor = customColors.colors[number];
        
        gameObject.layer = startPlayerLayer + number;
    }

    public void AddPoint(float points)
    {
        this.points += points;
        pointsImage.fillAmount = (this.points / maxPoints);
    }

    public float energy
    {
        get
        {
            return _energy;
        }
        set
        {
            _energy = value;
            if (_energy < 0) _energy = 0;
            else if (_energy > maxEnergy) _energy = maxEnergy;

            Debug.Log(_energy / maxEnergy);
            Debug.Log(255*(_energy / maxEnergy));
            var particleMain = energyParticleSystem.main;
            particleMain.startColor = new Color(customColors.colors[playerNumber].r, customColors.colors[playerNumber].g, customColors.colors[playerNumber].b, (_energy/maxEnergy));
            //particleMain.startColor = new Color(customColors.colors[playerNumber].r, customColors.colors[playerNumber].g, customColors.colors[playerNumber].b, 0.5f);
            Debug.Log("a: " + particleMain.startColor.color.a);
        }
    }
}
