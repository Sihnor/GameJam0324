using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitSceneScript : MonoBehaviour
{
    [SerializeField] private ESceneIndex SceneToLoad;
    
    // Start is called before the first frame update
    void Start()
    {
        SceneLoader.Instance.LoadScene(this.SceneToLoad);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
