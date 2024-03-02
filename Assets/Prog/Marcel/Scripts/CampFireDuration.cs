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
    
    
    // Start is called before the first frame update
    void Start()
    {
        this.TimeLeft = this.Duration;
    }

    private void FixedUpdate()
    {
        if (!this.HeatSource.activeSelf) return;
        
        if (this.TimeLeft <= 0)
        {
            this.HeatSource.SetActive(false);
            this.ParticleSystem.SetActive(false);
            return;
        }
        
        this.TimeLeft -= Time.fixedDeltaTime;
    }
}
