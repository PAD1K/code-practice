using System;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.Events;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 400f;							// Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;			// Amount of maxSpeed applied to crouching movement. 1 = 100%
	[SerializeField] private bool m_AirControl = false;							// Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;							// A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;							// A position marking where to check for ceilings
	[SerializeField] private Collider2D m_CrouchDisableCollider;				// A collider that will be disabled when crouching
	[SerializeField] private float _dashForce = 4000f;
	[SerializeField] private float _accelerateForce = 2f;
	[SerializeField] Collider2D _boxCollider;
    [SerializeField] Collider2D _circleCollider;
    private Vector2 _boxOriginalOffset;
    private Vector2 _circleOriginalOffset;
    [SerializeField] private float _jumpForce = 2f;
	[SerializeField] private SurfaceSlider _slider;
 33-falling-from-corner


	[SerializeField] private float _maxVelocityX;
	[SerializeField] private float _maxVelocityY;

 main
	const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 m_Velocity = Vector3.zero;
	private byte _maxCountOfJump = 2;

	private byte _countOfJumps = 2;

	public delegate void LandHandler ();
	public static event LandHandler OnLandEvent;
	public delegate void CrouchHandler(bool state);
	public static event CrouchHandler OnCrouchEvent;
	private bool m_wasCrouching = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();

		_boxOriginalOffset = _boxCollider.offset;
        _circleOriginalOffset = _circleCollider.offset;
	}

	private void FixedUpdate()
	{
		bool wasGrounded = m_Grounded;

		//Чтобы персонаж считался приземленным, если он сделал хотя бы один прыжок. 
		if (_countOfJumps < _maxCountOfJump)
		{
			wasGrounded = false; 
		}

		if (_countOfJumps == 0)
		{
			m_Grounded = false;
		}

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
				if (!wasGrounded) 
				{
					_countOfJumps = _maxCountOfJump;
					OnLandEvent?.Invoke();
					
					_boxCollider.offset = _boxOriginalOffset;
					_circleCollider.offset = _circleOriginalOffset;
				}
			}
		}
	}

	public void Move(float move, bool crouch, bool jump, bool isAcceleration)
	{
		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (m_Grounded || m_AirControl)
		{
			// If crouching
			if (crouch)
			{
				if (!m_wasCrouching)
				{
					m_wasCrouching = true;
					OnCrouchEvent.Invoke(true);
				}

				// Reduce the speed by the crouchSpeed multiplier
				move *= isAcceleration ? m_CrouchSpeed * _accelerateForce : m_CrouchSpeed;

				// Disable one of the colliders when crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = false;
			} else
			{
				// Enable the collider when not crouching
				if (m_CrouchDisableCollider != null)
					m_CrouchDisableCollider.enabled = true;

				if (m_wasCrouching)
				{
					m_wasCrouching = false;
					OnCrouchEvent.Invoke(false);
				}
			}

			// Move the character by finding the target velocity
			move = isAcceleration ? move * 10 * _accelerateForce : move * 10;
			Vector3 targetVelocity = new Vector2(move, m_Rigidbody2D.velocity.y);
			m_Rigidbody2D.velocity = targetVelocity;

			Vector2 directionAlongSurface = _slider.Project(targetVelocity);

			targetVelocity.Normalize();
			directionAlongSurface.Normalize();

			float dot = Vector3.Dot(targetVelocity, directionAlongSurface);
			float epsilon = 0.0001f;

			Debug.DrawRay(transform.position, Vector3.down * 3f, Color.green);
			RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.down, 3f, _groundLayer);

			// Если на наклонной поверхности, но не в прыжке
			if(!(Mathf.Abs(dot - 1) < epsilon) 
				&& !(Mathf.Abs(dot + 1) < epsilon) 
				&& _countOfJumps == _maxCountOfJump 
				&& hit.distance != 0
			)
			{
				m_Rigidbody2D.AddForce(Physics2D.gravity * m_Rigidbody2D.mass * _gravityForce);
			}

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}
		// If the player should jump...
		if (m_Grounded && jump)
		{
			_countOfJumps--;

			// Add a vertical force to the player.
			m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));

			// Чтобы персонаж не получал лишнее ускорение при прыжке на склонах
			m_Rigidbody2D.velocity = new Vector2(0, 0);

			_boxCollider.offset = new Vector2(_boxOriginalOffset.x, _boxOriginalOffset.y + _jumpForce);
			_circleCollider.offset = new Vector2(_circleOriginalOffset.x, _circleOriginalOffset.y + _jumpForce);
		}
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	public void Dash() {
		float direction = m_FacingRight ? 1 : -1;

		m_Rigidbody2D.AddForce(new Vector2(_dashForce * direction, 0f));
	}
}
