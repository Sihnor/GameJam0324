using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float MouseXSensitivity = 1.0f;
    [SerializeField] private float MouseYSensitivity = 1.0f;
    [SerializeField] private float ControllerXSensitivity = 1.0f;
    [SerializeField] private float ControllerYSensitivity = 1.0f;
    [SerializeField] private float MovementSpeed = 1.0f;

    private InputAction MoveAction;
    private InputAction LookMouseAction;

    private Vector2 MoveDirection;

    [SerializeField] private Transform CameraTransform;

    private Rigidbody PlayerRigidbody;

    private void Awake()
    {
        this.MoveAction = GetComponent<PlayerInput>().currentActionMap.FindAction("MoveKeyboard");
        this.LookMouseAction = GetComponent<PlayerInput>().currentActionMap.FindAction("LookMouse");


        Cursor.lockState = CursorLockMode.Locked;

    }

    // Start is called before the first frame update
    void Start()
    {
        this.MoveAction.performed += Move;
        this.LookMouseAction.started += ctx => Look(ctx, this.MouseXSensitivity, this.MouseYSensitivity);
        this.LookMouseAction.started += ctx => Look(ctx, this.ControllerXSensitivity, this.ControllerYSensitivity);

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
        look.y *= ySensitivity * -1;
        transform.Rotate(Vector3.up, look.x);

        float eulerX = this.CameraTransform.eulerAngles.x;
        
        // Clamp the camera rotation
        if (this.CameraTransform.eulerAngles.x <= 70.1 && this.CameraTransform.eulerAngles.x + look.y > 70 )
        {
            eulerX = 70;
        }
        else if (this.CameraTransform.eulerAngles.x >= 289.9 && this.CameraTransform.eulerAngles.x + look.y < 360 - 70)
        {
            eulerX = 360 - 70;
        }else
        {
            eulerX += look.y;
        }
        
        this.CameraTransform.rotation = Quaternion.Euler(eulerX, this.CameraTransform.eulerAngles.y, 0);
    }

    private void FixedUpdate()
    {
        Vector3 velocity = this.MoveDirection.x * transform.right + this.MoveDirection.y * transform.forward;

        this.PlayerRigidbody.AddForce(velocity);
        Debug.Log(velocity);
    }
}