using System;
using System.Collections;
using System.Collections.Generic;
using Prog.Marcel.Scripts;
using UnityEngine;

public class Heating : MonoBehaviour, IHeatingUp
{
    private float CurrentHeat = 0;
    private const float MaxHeat = 100;

    private bool IsHeatingUp = false;
    private float HeatTimer = 0;

    private void Awake()
    {
        this.CurrentHeat = MaxHeat; 
    }

    public void HeatingUp(float distance, float heatValue = 1)
    {
        if (!this.IsHeatingUp) StartCoroutine(StopHeatingUp());
        
        this.HeatTimer = 0;
        this.IsHeatingUp = true;
        
        if (this.CurrentHeat < MaxHeat)
        {
            this.CurrentHeat += heatValue;
        }
    }

    private void FixedUpdate()
    {
        if (!this.IsHeatingUp)
        {
            this.CurrentHeat -= 0.1f;
        }
    }
    
    private IEnumerator StopHeatingUp()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();
            
            this.HeatTimer += Time.fixedDeltaTime;
            
            if (this.HeatTimer > 2)
            {
                break;
            }
        }
        
        this.IsHeatingUp = false;
        yield return null;
    }
}
