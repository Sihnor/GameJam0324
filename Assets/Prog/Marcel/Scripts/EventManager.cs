using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
    
    public event Action<float> FOnHeatChange;
    public event Action FOnGameOver; 

    private void Awake()
    {
        if (Instance != null) return;
        
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void OnHeadChange(float heat)
    {
        FOnHeatChange?.Invoke(heat);
    }
    
    public void OnGameOver()
    {
        FOnGameOver?.Invoke();
    }
}
