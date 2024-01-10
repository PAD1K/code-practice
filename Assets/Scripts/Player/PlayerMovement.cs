using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{
    public float RunSpeed => runSpeed;
    public CharacterController2D controller;
    public Animator animator;
    public float runSpeed = 40f;

    private PlayerInputs _input;

    private float _horizontalMove = 0f;
    private bool _jump = false;
    private bool _crouch = false;

    public void onCrouch(bool state) 
    {
        animator.SetBool("IsCrouching", state);
    }

    public void OnLandig() 
    {
        _jump = false;
        animator.SetBool("IsJumping", _jump);
    }

    private void Awake() 
    {
        _input = new PlayerInputs();

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
        animator.SetFloat("Speed", Mathf.Abs(_horizontalMove));
        
        if (moveVector.y > 0) 
        {
            _jump = true;
            animator.SetBool("IsJumping", true);
        } 
        else if (moveVector.y < 0) 
        {
            _crouch = true;
            animator.SetBool("IsCrouching", _crouch);
        } 
        else 
        {
            _crouch = false;
            animator.SetBool("IsCrouching", _crouch);
        }
    }

    private void FixedUpdate() {
        controller.Move(_horizontalMove * Time.fixedDeltaTime, _crouch, _jump);
    }
}