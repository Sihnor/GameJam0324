using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFireDuration : MonoBehaviour
{
    [SerializeField] private GameObject heatSource;
    [SerializeField] private float duration = 10.0f;
    private float timeLeft;
    
    
    // Start is called before the first frame update
    void Start()
    {
        this.timeLeft = this.duration;
    }

    private void FixedUpdate()
    {
        if (!this.heatSource.activeSelf) return;
        
        if (this.timeLeft <= 0)
        {
            this.heatSource.SetActive(false);
            return;
        }
        
        this.timeLeft -= Time.fixedDeltaTime;
    }
}
