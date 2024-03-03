using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private List<AudioClip> FootstepSounds;
    private AudioSource FootstepPlayer;
    private Random RandomFootstep = new Random();
    
    private float MouseXSensitivity = 0.1f;
    private float MouseYSensitivity = 0.1f;
    private float ControllerXSensitivity = 1.0f;
    private float ControllerYSensitivity = 1.0f;
    [SerializeField] private float MovementSpeed = 1.0f;

    private InputAction MoveAction;
    private InputAction LookMouseAction;

    private Vector2 MoveDirection;

    [SerializeField] private Transform CameraTransform;

    private Rigidbody PlayerRigidbody;

    private void Awake()
    {
        Animator animator = GetComponent<Animator>();
        animator.enabled = false;
        
        this.MoveAction = GetComponent<PlayerInput>().currentActionMap.FindAction("MoveKeyboard");
        this.LookMouseAction = GetComponent<PlayerInput>().currentActionMap.FindAction("LookMouse");

        this.FootstepPlayer = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.MouseXSensitivity = GameManager.Instance.MouseSenseX;
        this.MouseYSensitivity = GameManager.Instance.MouseSenseY;
        this.ControllerXSensitivity = GameManager.Instance.GamepadSenseX;
        this.ControllerYSensitivity = GameManager.Instance.GamepadSenseY;
        
        this.MoveAction.performed += Move;
        this.LookMouseAction.started += ctx => Look(ctx, this.MouseXSensitivity, this.MouseYSensitivity);
        this.LookMouseAction.started += ctx => Look(ctx, this.ControllerXSensitivity, this.ControllerYSensitivity);

        this.MoveAction.canceled += ctx => this.MoveDirection = Vector2.zero;

        this.PlayerRigidbody = GetComponentInChildren<Rigidbody>();
        
        EventManager.Instance.FOnGameOver += OnGameOver;
    }

    private void Move(InputAction.CallbackContext ctx)
    {
        this.MoveDirection = ctx.ReadValue<Vector2>();
        this.MoveDirection.x *= this.MovementSpeed;
        this.MoveDirection.y *= this.MovementSpeed;
    }
    
    private void PlaySound()
    {
        this.FootstepPlayer.clip = this.FootstepSounds[this.RandomFootstep.Next(0, this.FootstepSounds.Count)];
        this.FootstepPlayer.Play();
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
        
        if (velocity.magnitude > 0.1 && !this.FootstepPlayer.isPlaying)
        {
            this.PlaySound();
        }
        
        this.PlayerRigidbody.AddForce(velocity);
    }
    
    private void OnGameOver()
    {
        this.MoveAction.Disable();
        this.LookMouseAction.Disable();

        Animator animator = GetComponent<Animator>();
        animator.enabled = true;
        animator.SetBool("IsDead", true);
    }
}