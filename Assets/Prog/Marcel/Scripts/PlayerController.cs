using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
     private InputAction MoveAction;
     private InputAction LookMouseAction;
     
     private Vector2 MoveDirection;

     private float KeyboardXSensitivity = 1.0f;
     private float KeyboardYSensitivity = 1.0f;
     private float ControllerXSensitivity = 1.0f;
     private float ControllerYSensitivity = 1.0f;
     [SerializeField] private float MovementSpeed = 1.0f;
     
     private Rigidbody PlayerRigidbody;
    
    private void Awake()
    {
         this.MoveAction = GetComponent<PlayerInput>().currentActionMap.FindAction("MoveKeyboard");
         this.LookMouseAction = GetComponent<PlayerInput>().currentActionMap.FindAction("LookMouse");
    }

    // Start is called before the first frame update
    void Start()
    {
        this.MoveAction.performed += Move;
        this.LookMouseAction.performed += ctx => Look(ctx, this.KeyboardXSensitivity, this.KeyboardYSensitivity);
        
        this.MoveAction.canceled += ctx => this.MoveDirection = Vector2.zero;
        
        this.PlayerRigidbody = GetComponentInChildren<Rigidbody>();
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        this.MoveDirection = ctx.ReadValue<Vector2>();
        this.MoveDirection.x *= this.MovementSpeed;
        this.MoveDirection.y *= this.MovementSpeed;
    }
    
    private void Look(InputAction.CallbackContext ctx, float xSensitivity, float ySensitivity)
    {
        Vector2 look = ctx.ReadValue<Vector2>();
        look.x *= xSensitivity;
        look.y *= ySensitivity;
        
        Debug.Log("Look: " + look);
    }

    private void FixedUpdate()
    {
        Vector3 velocity = this.MoveDirection.x * transform.forward + this.MoveDirection.y * transform.right;
        
        this.PlayerRigidbody.AddForce(velocity);
    }
}
