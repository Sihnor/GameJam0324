using System;
using System.Collections;
using System.Collections.Generic;
using Prog.Marcel.Scripts;
using UnityEngine;

public class Heating : MonoBehaviour, IHeatingUp
{
    [SerializeField] private float CurrentHeat = 0;
    private const float MaxHeat = 100;

    private bool IsHeatingUp = false;

    private void Awake()
    {
        this.CurrentHeat = MaxHeat; 
    }

    public void HeatingUp(float distance, float heatValue = 1)
    {
        this.IsHeatingUp = true;
        
        if (this.CurrentHeat < MaxHeat)
        {
            this.CurrentHeat += heatValue;
        }
    }

    private void FixedUpdate()
    {
        if (this.IsHeatingUp) return;
        
        if (this.CurrentHeat > 0)
        {
            this.CurrentHeat -= 0.1f;
        }
        
    }
}
