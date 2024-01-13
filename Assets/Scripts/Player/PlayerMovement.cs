using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float RunSpeed => runSpeed;
    public CharacterController2D _controller;
    public Animator _animator;
    public float runSpeed = 40f;
    private bool _isAcceleration = false;
    
    private PlayerInputs _input;
    private float _horizontalMove = 0f;
    private bool _isjump = false;
    private bool _crouch = false;
    private bool _isDash = false;

    public void onCrouch(bool state) 
    {
        _animator.SetBool("IsCrouching", state);
    }

    public void OnLandig() 
    {
        _isjump = false;
        _animator.SetBool("IsJumping", _isjump);
    }

    private void Awake() 
    {
        _input = new PlayerInputs();

        _input.Player.Jump.performed += context => Jump();
        _input.Player.Dash.performed += context => Dash();

        CharacterController2D.OnLandEvent += OnLandig;
        CharacterController2D.OnCrouchEvent += onCrouch;
    }

    private void OnEnable() 
    {
        _input.Enable();
    }

    private void OnDisable() 
    {
        _input.Disable();
    }

    // Update is called once per frame
    private void Update() 
    {
        Vector2 moveVector = _input.Player.WASD.ReadValue<Vector2>();

        _horizontalMove = moveVector.x * runSpeed;
        _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));

        if (_input.Player.Acceleration.ReadValue<float>() == 1f) 
        {
            _isAcceleration = true;
        } 
        else {
            _isAcceleration = false;
        }

        if (moveVector.y < 0) 
        {
            _crouch = true;
            _animator.SetBool("IsCrouching", _crouch);
        } 
        else 
        {
            _crouch = false;
            _animator.SetBool("IsCrouching", _crouch);
        }
    }

    private void FixedUpdate() {
        if (_isDash)
        {
            _isDash = false;
            _controller.Dash();
        }

        _controller.Move(_horizontalMove * Time.fixedDeltaTime, _crouch, _isjump, _isAcceleration);
        _isjump = false;
    }

    private void Jump() 
    {
        _isjump = true;
        _animator.SetBool("IsJumping", true);
    }

    private void Dash() 
    {
        _isDash = true;
    }
}