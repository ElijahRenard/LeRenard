using UnityEngine;
using System.Collections;

public class TriggerScript : MonoBehaviour 
{
	// Config
	//CharacterController2D _controller;
	//Animator _animator;
	//public string _anim = "TreeBreakable";
	//GameObject _triggerTree;

	// Use this for initialization
	void Awake ()
	{
		//_animator = GetComponent<Animator>();
		//_controller = GetComponent<CharacterController2D>();

		//_controller.onControllerCollidedEvent += onControllerCollider;

		// Enable everything
		// -- Collider
		//collider2D.enabled = true;
		//_triggerTree = GameObject.Find("BreakableTree_Trigger");
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log (other);
	}

}
