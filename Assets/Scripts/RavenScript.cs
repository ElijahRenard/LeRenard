using UnityEngine;
using System.Collections;
//Added namespace
using System.Collections.Generic;

// This below is an attribute that ensures a CC is attached to the object and cannot be removed
[RequireComponent (typeof (CharacterController))]
public class RavenScript : MonoBehaviour {
	
	
	// Awake: Here you setup the component you are on right now (the "this" object)
	// Start: Here you setup things that depend on other components.
	
	// 1. All awake are executed
	// 2. All start are executed
	// 3. all go into update
	
	// movement config
	//public float gravity = -15f;
	//public float speed = 1f;
	//public float groundDamping = 20f; // how fast do we change direction? higher means faster, low number simulates sliding on ice (delay after keyboard input)
	//public float inAirDamping = 5f;

	//  Component references
	//private MoveScript moveScript;

	private Vector2 positionTarget;

	Animator _animator;
	CharacterController2D _controller;
	Transform _transform;
	public RaycastHit2D lastControllerColliderHit;
	
	Vector3 moveDirection;

	public static GameObject _fox;
	

	private bool change; 
	private Vector3 target;

	// Variables AI Pathfinder
	public float range = 1f;
	Dictionary<int, Transform> waypoint = new Dictionary<int, Transform>();
	int index;
	public string strTag;


	void Awake()
	{
		//_animator = GetComponent<Animator>();
		_controller = GetComponent<CharacterController2D>();
		_transform = GetComponent<Transform>();
		_controller.onControllerCollidedEvent += onControllerCollider;
		
		//maxDistance = 1f - 0.14f;
		_fox = GameObject.Find("PlayerFox");
		
		// Retrieve scripts to disable when not spawned
		//moveScript = GetComponent<MoveScript>();
	}
	// Use this for initialization
	void Start ()
	{
		//target = GetTarget();
		// InvokeRepeating("YourFunctionName", delayForFirstCall, timeBetweenCalls)
		InvokeRepeating ("NewTarget",0.01f,1f);

		// AI Pathfinder
		index = 0;
		
		//Added codes
		if (string.IsNullOrEmpty(strTag))
			Debug.LogError("No waypoint tag given");
		//range = 4;
		GameObject[] gos = GameObject.FindGameObjectsWithTag(strTag);
		foreach (GameObject go in gos)
		{
			WaypointScript script = go.GetComponent<WaypointScript>();
			waypoint.Add(script.index, go.transform);

			//Debug.Log (waypoint[script.index].position);
		}
	}

	void Update()
	{
		//Debug.Log ((_transform.position - waypoint[index].position).sqrMagnitude);

		//Debug.Log (waypoint[index].position);

		//if ((_transform.position - waypoint[index].position).sqrMagnitude < range)

	}

	void Move(Transform target)
	{
		//Debug.Log (target);

		_controller.transform.position = target.position;

		// Movement
		//moveDirection = _transform.forward;
		//moveDirection = _transform.right;
		//moveDirection *= speed;
		//moveDirection.y -= gravity * Time.deltaTime;
		//_controller.transform.Translate(moveDirection * Time.deltaTime);
		//_controller.Move(moveDirection * Time.deltaTime);
		//Rotation
		//var newRotation = Quaternion.LookRotation(target.position - _transform.position).eulerAngles;
		//var angles = _transform.rotation.eulerAngles;
		//_transform.rotation = Quaternion.Euler(angles.x,
		 //                                      Mathf.SmoothDampAngle(angles.y, newRotation.y, ref velocity, minTime, maxRotSpeed), angles.z);
	}

	Vector3 GetTarget(){
		int choice = Random.Range (0,3);
		//return waypoint[choice].position;
		return waypoint[index].position;
	}

	void NewTarget()
	{
		int choice = Random.Range (0,3);
		switch(choice){
		case 0: 
			change = true;
			break;
		case 1: 
			change = false;
			break;
		case 2:
			if(_controller.transform.position != waypoint[index].position)
			{
				Move(waypoint[index]);
				NextIndex();
			}
			break;
		} 
	}

	void NextIndex()
	{
		if (++index == waypoint.Count) index = 0;
	}

	/*void Update ()
	{
		target = GetTarget();
		//if(change)
		//	target = GetTarget ();
		
		if(Vector3.Distance(_transform.position,target)<range)
		{
			Move();
		//	animation.CrossFade("walk");
		}//else animation.CrossFade ("idle");

		Debug.Log ("fox " + target + " crow " + _transform.position + " range " + range + " distance = " + Vector3.Distance(_transform.position,target));
	}
	Vector3 GetTarget()
	{
		//right	Shorthand for writing Vector3(1, 0, 0)
		//return new Vector3(Random.Range (0,2),0,0);
		Vector3 fox = _fox.transform.position;
		return fox;
	}
	*/
	/*void Move()
	{
		moveDirection = _transform.right;
		//moveDirection *= speed;
		//moveDirection.y -= gravity;
		_controller.transform.Translate(moveDirection * Time.deltaTime);
	}*/

	void onControllerCollider( RaycastHit2D hit )
	{
		// bail out on plain old ground hits
		if( hit.normal.y == 1f )
			return;
		
		// logs any collider hits
		//Debug.Log( "flags: " + _controller.collisionState + ", hit.normal: " + hit.normal );
	}
	

	
	// Update is called once per frame
	/*void Update ()
	{
		//Debug.Log ("lil fox at " + _fox.transform.position.x);
		CheckDistance ();
	}

	// Check distance between crow and fox
	void CheckDistance()
	{
		//Debug.Log ("fox " + _fox.transform.position.x + " crow " + _controller.transform.position.x + " distance " + maxDistance);
		float crow_x = _controller.transform.position.x;
		float fox_x = _fox.transform.position.x;

		float distance = crow_x - fox_x;
		//Debug.Log ("distance " + distance + " " + crow_x + " " + fox_x + " maxDistance " + maxDistance);

		//Vector3 foxy = _fox.transform.position;

		//Debug.Log (foxy);

		if(distance <= maxDistance)
		{
			JumpAway(crow_x);
		}
	}

	void JumpAway(float some)
	{
		Debug.Log ("JumpAway called");

		// Define a target?
		if (positionTarget == Vector2.zero)
		{
			// Get a point on the screen, convert to world
			Vector2 randomPoint = new Vector2(Random.Range(0f, 0.2f), Random.Range(0f, 0.2f));

			positionTarget = Camera.main.ViewportToWorldPoint(randomPoint);
		}
		
		// Are we at the target? If so, find a new one
		//if (collider2D.OverlapPoint(positionTarget))
		//{
			// Reset, will be set at the next frame
		//	positionTarget = Vector2.zero;
		//}
		
		// Go to the point
		Vector3 direction = ((Vector3)positionTarget - this.transform.position);
		
		// Remember to use the move script
		//moveScript.direction = Vector3.Normalize(direction);

		//float jump_x = some + 0.2f;
		//_controller.transform.position = Vector3(-1, 0, 0);
		// Move the object to (0, 0, 0)
		//transform.position = Vector3(0, 0, 0);

		// 1 - Designer variables
		
		/// <summary>
		/// Object speed
		/// </summary>
		 Vector2 speed = new Vector2(-1, 1);
		
		/// <summary>
		/// Moving direction
		/// </summary>
		// Vector2 direction = new Vector2(-1, 0);

			// 2 - Movement
			Vector3 movement = new Vector3(
				speed.x * direction.x,
				speed.y * direction.y,
				0);
			
			movement *= Time.deltaTime;
			transform.Translate(movement);
	
	}*/
}
