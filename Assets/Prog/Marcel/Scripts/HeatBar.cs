using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class HeatBar : MonoBehaviour
{
    [SerializeField] private Image HeatBarImage;
    [SerializeField] private List<AudioClip> BreathSounds;

    private Random RandomSound;
    
    private AudioSource BreathSoundPlayer;
    
    private bool Reached75 = false;
    private bool Reached50 = false;
    private bool Reached25 = false;
    
    private bool GameOver = false;

    private void Awake()
    {
        //EventManager.Instance.FOnHeatChange += UpdateHeatBar;
        this.BreathSoundPlayer = GetComponent<AudioSource>();
        
        this.RandomSound = new Random();
    }

    private void Start()
    {
        EventManager.Instance.FOnHeatChange += UpdateHeatBar;
        EventManager.Instance.FOnGameOver += OnGameOver;
    }

    private void UpdateHeatBar(float heat)
    {
        // Sound for under 75 heat
        if (heat > 75 && this.Reached75)
        {
            this.Reached75 = false;
        }
        if (heat < 75 && !this.Reached75)
        {
            PlaySound();
            this.Reached75 = true;
        }
        
        // Sound for under 50 heat
        if (heat > 50 && this.Reached50)
        {
            this.Reached50 = false;
        }
        if (heat < 50 && !this.Reached50)
        {
            PlaySound();
            this.Reached50 = true;
        }
        
        // Sound for under 25 heat
        if (heat > 25 && this.Reached25)
        {
            this.Reached25 = false;
        }
        if (heat < 25 && !this.Reached25)
        {
            PlaySound();
            this.Reached25 = true;
        }
        
        this.HeatBarImage.fillAmount = heat / 100;
    }

    private void FixedUpdate()
    {
        if (this.Reached25 && !this.BreathSoundPlayer.isPlaying) PlaySound();
    }

    private void PlaySound()
    {
        if (this.BreathSoundPlayer.isPlaying || this.GameOver) return;
        
        this.BreathSoundPlayer.clip = this.BreathSounds[this.RandomSound.Next(0, this.BreathSounds.Count)];
        this.BreathSoundPlayer.Play();
    }
    
    private void OnGameOver()
    {
        this.GameOver = true;
    }
}
