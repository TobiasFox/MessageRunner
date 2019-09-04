﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public delegate void Win();
    public static event Win OnWin;

    public int playerNumber { get; private set; }
    public float points { get; set; }

    //[SerializeField] private float maxPoints;
    [SerializeField] private CustomColors customColors;
    [SerializeField] private Image pointsImage;
    [SerializeField] private float maxEnergy;
    [SerializeField] private ParticleSystem energyParticleSystem;
    [SerializeField] private ParticleSystem[] orbitParticles;

    private GameManager gameManager;
    private PlayerMovement playerMovement;
    private int startPlayerLayer;
    private float _energy;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        startPlayerLayer = LayerMask.NameToLayer("Blue");
        gameManager = FindObjectOfType<GameManager>();

        _energy = maxEnergy;
    }

    public void SetPlayerNumber(int number)
    {
        playerNumber = number;
        //playerMovement.PlayerNumber = number;
        Renderer rend = GetComponent<Renderer>();           //color of player
        rend.material.color = customColors.colors[number];
        pointsImage.color = customColors.colors[number];    //color of points
        var particleMain = energyParticleSystem.main;       //color of energy particle system
        particleMain.startColor = customColors.colors[number];
        foreach(ParticleSystem particles in orbitParticles)
        {
            var orbitParticleMain = particles.main;       //color of orbit particle system
            orbitParticleMain.startColor = customColors.colors[number];
        }

        gameObject.layer = startPlayerLayer + number;
    }

    public void AddPoint(float points)
    {
        this.points += points;
        pointsImage.fillAmount = (this.points / gameManager.GetMaxPoints());

        if (this.points >= gameManager.GetMaxPoints())
        {
            OnWin();
        }
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
            if (_energy < 0)
            {
                _energy = 0;
                gameManager.hasPlayerEnergy[gameObject] = false;
                gameManager.CheckGameOver();
            }
            else
            {
                gameManager.hasPlayerEnergy[gameObject] = true;
                if (_energy > maxEnergy)
                {
                    _energy = maxEnergy;
                }
            }
            var particleMain = energyParticleSystem.main;
            particleMain.startColor = new Color(customColors.colors[playerNumber].r, customColors.colors[playerNumber].g, customColors.colors[playerNumber].b, (_energy / maxEnergy));
        }
    }
}