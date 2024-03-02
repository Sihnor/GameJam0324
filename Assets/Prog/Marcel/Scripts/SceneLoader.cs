using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum ESceneIndex
{
    MainScene = 0,
    SettingsScene = 1,
    GameScene = 2,
    GameOverScreen = 3
}

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance;

    private void Awake()
    {
        if (Instance != null) return;
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void LoadScene(ESceneIndex scene)
    {
        SceneManager.LoadScene(scene.ToString(), LoadSceneMode.Single);
    }
}
