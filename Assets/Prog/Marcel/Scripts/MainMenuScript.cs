using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] private Button StartButton;
    [SerializeField] private Button SettingsButton;
    [SerializeField] private Button ExitButton;

    private void Awake()
    {
        this.StartButton.onClick.AddListener(StartGame);
        this.SettingsButton.onClick.AddListener(OpenSettings);
        this.ExitButton.onClick.AddListener(ExitGame);
    }

    private void StartGame()
    {
        PlayAudio();
        
        SceneLoader.Instance.LoadScene(ESceneIndex.GameScene);
    }
    
    private void OpenSettings()
    {
        PlayAudio();
        
        SceneLoader.Instance.LoadScene(ESceneIndex.SettingsScene);
    }
    
    private void ExitGame()
    {
        PlayAudio();
        
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    
    private void PlayAudio()
    {
        GetComponent<AudioSource>().Play();
    }
}
