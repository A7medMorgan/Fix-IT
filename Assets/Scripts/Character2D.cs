using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character2D : MonoBehaviour
{
	[SerializeField] private float JumpForce = 400f;                          // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Collider2D CrouchDisableCollider;                // A collider that will be disabled when crouching
	[Header("Events")]
	[Space]

	public UnityEvent OnLandEvent;

	[System.Serializable]
	public class BoolEvent : UnityEvent<bool> { }
	
	public BoolEvent OnCrouchEvent;
	private bool _wasCrouching = false;


	private Rigidbody2D _Rigidbody2D;

	const float C_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool _Grounded;            // Whether or not the player is grounded.
	const float C_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private bool _FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 _Velocity = Vector3.zero;
	private void Awake()
	{
		_Rigidbody2D = this.GetComponent<Rigidbody2D>();
		if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();

		if (OnCrouchEvent == null)
			OnCrouchEvent = new BoolEvent();
	}
	// Start is called before the first frame update
	void Start()
    {

	}

    // Update is called once per frame
    void Update()
    {
		
	}

	private void FixedUpdate()
	{
		bool Was_ground = _Grounded;
		_Grounded = false;
		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] collider = Physics2D.OverlapCircleAll(GroundCheck.position, C_GroundedRadius, WhatIsGround);
		for (int i = 0; i < collider.Length; i++)
		{
			if (collider[i].gameObject != gameObject)
			{
				_Grounded = true;
				if (!Was_ground)
					OnLandEvent.Invoke();
			}
		}
	}

	public void Move_Horizontal(float speed,bool crouch)
	{
		if (!crouch)// If crouching, check to see if the character can stand up
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(CeilingCheck.position, C_CeilingRadius, WhatIsGround))
			{
				crouch = true;
			}
		}
		//Debug.Log(crouch);
		// check the rigidebody has been enabled
			if (_Rigidbody2D != null)
		{
			if (AirControl || _Grounded) //only control the player if grounded or airControl is turned on
			{
				if (crouch) // If crouching
				{
					if (!_wasCrouching)
					{
						_wasCrouching = true;
						OnCrouchEvent.Invoke(true);
					}
					// Reduce the speed by the crouchSpeed multiplier
					speed *= CrouchSpeed;

					// check the collider is detect
					if (CrouchDisableCollider != null) CrouchDisableCollider.enabled = false; // disable it
				}
				else
				{
					// check the collider is detect
					if (CrouchDisableCollider != null) CrouchDisableCollider.enabled = true; // enable it
					if (_wasCrouching)
					{
						_wasCrouching = false;
						OnCrouchEvent.Invoke(false);
					}
				}
				// If the input is moving the player right and the player is facing left...
				if (speed > 0 && !_FacingRight) Flib();
				// Otherwise if the input is moving the player left and the player is facing right...
				else if (speed < 0 && _FacingRight) Flib();
				// move the player using rigidbody2d velocity
				Vector3 target_velocity = new Vector2(speed, _Rigidbody2D.velocity.y);
				_Rigidbody2D.velocity = Vector3.SmoothDamp(_Rigidbody2D.velocity, target_velocity, ref _Velocity, MovementSmoothing);
			}
		}
		else{
			transform.position += new Vector3(speed, 0, 0);
		}
		
	}
	// If the player should jump...
	public void Jumb()
	{
		if (_Grounded)
		{
			// Add a vertical force to the player.
			_Rigidbody2D.AddForce(new Vector2(0f, JumpForce));
			_Grounded = false;
		}
	}

		
	private void Flib()
	{
		// Switch the way the player is labelled as facing.
		_FacingRight =!_FacingRight;
		//change the rotation sacle by rotate the y axis 180 degre
		Quaternion rotation = transform.rotation;
		// to make sure it`s not goning to wrong direction
		if (_FacingRight) rotation.y = 0;
		else rotation.y = 180;
		
		transform.rotation = rotation;
	}
	public void _Flip()
	{

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
