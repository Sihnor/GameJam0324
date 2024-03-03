using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestAudio : MonoBehaviour
{
    private Button TestButton;
    private AudioSource TestAudioSource;
    
    private void Awake()
    {
        this.TestButton = GetComponent<Button>();
        this.TestAudioSource = GetComponent<AudioSource>();
        
        this.TestButton.onClick.AddListener(PlaySound);
    }
    
    private void PlaySound()
    {
        this.TestAudioSource.Play();
        Invoke("StopSound", 3.0f);
    }
    
    private void StopSound()
    {
        this.TestAudioSource.Stop();
    }
    
}
