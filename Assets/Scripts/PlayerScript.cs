using UnityEngine;
using System.Collections;


public class PlayerScript : MonoBehaviour
{
	//public static GameObject _fox;

	// movement config
	public float gravity = -15f;
	public float runSpeed = 1f;
	public float groundDamping = 20f; // how fast do we change direction? higher means faster, low number simulates sliding on ice (delay after keyboard input)
	public float inAirDamping = 5f;

	[HideInInspector]
	public float rawMovementDirection = 1;
	[HideInInspector]
	public float normalizedHorizontalSpeed = 0;

	CharacterController2D _controller;
	Animator _animator;
	public RaycastHit2D lastControllerColliderHit;

	[HideInInspector]
	public Vector3 velocity;

	[HideInInspector]
	// timer for 2nd idle anim
	public bool isIdle = false;
	//[HideInInspector]
	public float timeToIdle = 6.0f; // 2 seconds
	float currentTime = 0f;

	private bool isMoving;
	public SnowTileScript mySnowscript;

	/// <summary>
	/// Is player moving?
	/// </summary>
	//public bool _isFoxMoving;

	void Awake()
	{
		//_fox = GameObject.Find("PlayerFox");

		_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
		_controller.onControllerCollidedEvent += onControllerCollider;

		// initialization 2nd idle anim
		currentTime = Time.time + timeToIdle;

		//_isFoxMoving = false;
	}

	
	/*void OnTriggerEnter2D(Collider2D collider)
	{
		// Is this a shot?
		PickupCheese cheese = collider.gameObject.GetComponent<PickupCheese>();
		if (cheese != null)
		{
			Debug.Log ("This fox likes cheese");
			// eat the cheese

			// Destroy the cheese object
			// Remember to always target the game object,
			// otherwise you will just remove the script.
			//Destroy(shot.gameObject);
		}
	}*/
	
	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits
		if( hit.normal.y == 1f )
			return;

		// logs any collider hits
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}


	void Update()
	{
		//Debug.Log ("fox x = " + _fox.transform.position.x);
		// grab our current velocity to use as a base for all calculations
		velocity = _controller.velocity;

		if( _controller.isGrounded )
			velocity.y = 0;

		if (Input.GetKey (KeyCode.RightArrow))
		{
			normalizedHorizontalSpeed = 1;
			if (transform.localScale.x < 0f)
				transform.localScale = new Vector3 (-transform.localScale.x, transform.localScale.y, transform.localScale.z);

			if (_controller.isGrounded)
			{
				animState("Walk");
				resetIdle (); // reset 2nd idle anim
				//_isFoxMoving = true;
				FoxMoving();
			}
		}
		else if( Input.GetKey( KeyCode.LeftArrow ) )
		{
			normalizedHorizontalSpeed = -1;
			if( transform.localScale.x > 0f )
				transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );

			if( _controller.isGrounded )
			{
				animState("Walk");
				resetIdle(); // reset 2nd idle anim
				//_isFoxMoving = true;
				FoxMoving();
			}
		}
		else if (Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
		{
			isMoving = false;
			FoxStopped();
		}
		else if(isMoving == false)
		{
			normalizedHorizontalSpeed = 0;
			animState("Idle");
			//_isFoxMoving = false;
			//FoxIdle();
		}

		if( Input.GetKeyDown( KeyCode.UpArrow ) )
		{
			isMoving = true;
			normalizedHorizontalSpeed = 0;
			//if( transform.localScale.x > 0f )
			//	transform.localScale = new Vector3( -transform.localScale.x, transform.localScale.y, transform.localScale.z );
			
			if( _controller.isGrounded )
			{
				animState("Dive");
				FoxStopped();
				//_isFoxMoving = false;
				//FoxIdle();
			}
		}

		// apply horizontal speed smoothing it
		var smoothedMovementFactor = _controller.isGrounded ? groundDamping : inAirDamping; // how fast do we change direction?
		velocity.x = Mathf.Lerp( velocity.x, normalizedHorizontalSpeed * rawMovementDirection * runSpeed, Time.deltaTime * smoothedMovementFactor );
		
		// apply gravity before moving
		velocity.y += gravity * Time.deltaTime;

		_controller.move( velocity * Time.deltaTime );

	}

	// No idle anims at the moment, just using a default frame.. no wait anim either.
	void checkIdle()
	{
		if(Time.time > currentTime)
		{
			isIdle = true;
			//run your anim here or a seperate function to translate the boolean value
			//animState("Wait");
			//_animator.Play(Animator.StringToHash("Wait"));
			currentTime = Time.time + timeToIdle;

			// logs
			//Debug.Log( "checkIdle() " + isIdle + " " + timeToIdle);
		}
		else
		{
			animState("Idle");
			//_animator.Play( Animator.StringToHash( "Idle" ) );
			// logs
			//Debug.Log( "checkIdle() else " + isIdle );
		}
	}
	void animState(string some)
	{
		_animator.Play( Animator.StringToHash( some ) );
		//Debug.Log (animation.IsPlaying(some));
	
	}
	void resetIdle()
	{
		isIdle = false;
		currentTime = Time.time + timeToIdle;
	}

	void FoxMoving(){
		//SnowTileScript snow = gameObject.GetComponent<SnowTileScript>();
		mySnowscript.SetIsAppearing();
		//Debug.Log ("FoxMoving()");
	}
	void FoxStopped(){
		//SnowTileScript snow = gameObject.GetComponent<SnowTileScript>();
		mySnowscript.SetIsFading();
		//Debug.Log ("FoxStopped()");
	}
}
