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
        
        EventManager.Instance.FOnGameOver += ShowGameOverScreen;
    }
    
    private void OpenMainMenu()
    {
        SceneLoader.Instance.LoadScene(ESceneIndex.MainScene);
    }
    
    private void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
    
    private void ShowGameOverScreen()
    {
        this.GameOverScreen.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
