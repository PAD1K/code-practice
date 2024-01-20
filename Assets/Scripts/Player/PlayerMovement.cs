using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float RunSpeed => runSpeed;
    public CharacterController2D _controller;
    public Animator _animator;
    public float runSpeed = 40f;
    private bool _isAcceleration = false;
    
    [SerializeField] private uint _maxStamina = 100;
    [SerializeField] private uint _minStamina = 0;
    [SerializeField] private Bar _staminaBar;
    [SerializeField] private float _staminaDelta = 0.1f;
    [SerializeField] private float _nextRestoreStaminaTime = 0f;
    [SerializeField] private uint _dashDecreaseValue = 10;
    [SerializeField] private uint _attackDecreaseValue = 20;
    private PlayerInputs _input;
    private float _horizontalMove = 0f;
    private bool _isjump = false;
    private bool _crouch = false;
    private bool _isDash = false;
    private uint _currentStamina;

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

        _currentStamina = _maxStamina;
        _staminaBar.SetMaxValue(_currentStamina);
        _staminaBar.SetMinValue(_minStamina);
        _staminaBar.SetValue(_currentStamina);
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
        Move();
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

    private void Move()
    {
        Vector2 moveVector = _input.Player.WASD.ReadValue<Vector2>();

        _horizontalMove = moveVector.x * runSpeed;
        _animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));

        float accelerationState = _input.Player.Acceleration.ReadValue<float>();
        if (accelerationState == 1f && _currentStamina > 0 && moveVector != new Vector2(0f, 0f)) 
        {
            _isAcceleration = true;
            _currentStamina--;
            _staminaBar.SetMinValue(_currentStamina);
        }
        else 
        {
            _isAcceleration = false;

            if (Time.time >= _nextRestoreStaminaTime && _currentStamina < 100 && accelerationState == 0f)
            {
                _currentStamina++;
                _staminaBar.SetValue(_currentStamina);
                _nextRestoreStaminaTime = Time.time + _staminaDelta;
            }
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

    private void Jump() 
    {
        _isjump = true;
        _animator.SetBool("IsJumping", true);
    }

    private void Dash() 
    {
        if(_currentStamina >= _dashDecreaseValue)
        {
            _isDash = true;
            _currentStamina -= _dashDecreaseValue;
            _staminaBar.SetValue(_currentStamina);
        }
    }

    public void DecreaseStamina()
    {
        if(_currentStamina >= _attackDecreaseValue)
        {
            _currentStamina -= _attackDecreaseValue;
            _staminaBar.SetValue(_currentStamina);
        }
    }
}