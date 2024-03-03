using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    public float WindCampfireVolume = 0.5f;
    public float BreathVolume = 0.5f;
    public float StepsVolume = 0.5f;

    public float MouseSenseX = .01f;
    public float MouseSenseY = .01f;
    public float GamepadSenseX = .5f;
    public float GamepadSenseY = .5f;

    private void Awake()
    {
        if (Instance != null) return;
        
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
