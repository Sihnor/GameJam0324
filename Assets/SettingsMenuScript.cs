using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuScript : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton1))
        {
            SceneLoader.Instance.LoadScene(ESceneIndex.MainScene);
        }
    }
}
