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

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;

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

    public void onCrouch(bool state) 
    {
        animator.SetBool("IsCrouching", state);
    }

    public void OnLandig() 
    {
        jump = false;
        animator.SetBool("IsJumping", jump);
    }

    // Update is called once per frame
    private void Update() 
    {
        Vector2 moveVector = _input.Player.WASD.ReadValue<Vector2>();
        horizontalMove = moveVector.x * runSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontalMove));
        
        if (moveVector.y > 0) 
        {
            jump = true;
            animator.SetBool("IsJumping", true);
        } 
        else if (moveVector.y < 0) 
        {
            crouch = true;
            animator.SetBool("IsCrouching", crouch);
        } 
        else 
        {
            crouch = false;
            animator.SetBool("IsCrouching", crouch);
        }
    }

    private void FixedUpdate() 
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
    }
}