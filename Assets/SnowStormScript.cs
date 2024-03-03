using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowStormScript : MonoBehaviour
{
    [SerializeField] private Transform PlayerTransform;
    
    private AudioSource SnowStormSound;

    private void Awake()
    {
        this.SnowStormSound = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.SnowStormSound.Play();
        
        transform.position = this.PlayerTransform.position;
        transform.rotation = this.PlayerTransform.rotation;
    }                             

    // Update is called once per frame
    void Update()
    {
        transform.position = this.PlayerTransform.position + new Vector3(0, 3, 0);
        transform.rotation = this.PlayerTransform.rotation;
    }
}
