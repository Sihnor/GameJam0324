using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreenScript : MonoBehaviour
{
    [SerializeField] private GameObject GameOverScreen;
    [SerializeField] private Button MainMenuButton;
    [SerializeField] private Button ExitButton;

    private void Awake()
    {
        this.MainMenuButton.onClick.AddListener(OpenMainMenu);
        this.ExitButton.onClick.AddListener(ExitGame);
    }

    private void Start()
    {
       EventManager.Instance.FOnGameOver += ShowGameOverScreen;
    }

    private void OpenMainMenu()
    {
        PlayAudio();
        
        SceneLoader.Instance.LoadScene(ESceneIndex.MainScene);
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
    
    private void ShowGameOverScreen()
    {
        this.GameOverScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
