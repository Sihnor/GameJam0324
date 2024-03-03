using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFireDuration : MonoBehaviour
{
    [SerializeField] private GameObject HeatSource;
    [SerializeField] private GameObject ParticleSystem;
    [SerializeField] private float Duration = 10.0f;
    private float TimeLeft;
    private bool IsActivated;

    private AudioSource FireSound;

    private void Awake()
    {
        this.FireSound = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        Activate();
    }

    public void Activate()
    {
        if (this.IsActivated) return;

        this.TimeLeft = this.Duration;
        this.IsActivated = true;
        this.HeatSource.SetActive(true);
        this.ParticleSystem.SetActive(true);
        this.FireSound.Play();
        //this.FireSound.volume = 1.0f;
    }

    private void FixedUpdate()
    {
        if (!this.IsActivated) return;
        if (!this.HeatSource.activeSelf) return;

        if (this.TimeLeft <= 2)
        {
            FadeFireSound();
        }
        
        if (this.TimeLeft <= 0)
        {
            Deactivate();
            return;
        }

        this.TimeLeft -= Time.fixedDeltaTime;
    }
    
    private void FadeFireSound()
    {
        if (this.FireSound.volume > 0)
        {
            this.FireSound.volume -= Time.fixedDeltaTime;
        }
    }
    
    private void Deactivate()
    {
        this.IsActivated = false;
        this.HeatSource.SetActive(false);
        this.ParticleSystem.SetActive(false);
        this.FireSound.Stop();
    }
}