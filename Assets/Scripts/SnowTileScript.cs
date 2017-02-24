using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Moon highlights in snowy tiles
/// When player moves only.
/// </summary>
public class SnowTileScript : MonoBehaviour {

	//--------------------------------
	// 1 - Designer variables
	//--------------------------------

	Transform _transform;
	Transform _allChildren;
	Transform _child;
	Transform _childchild;
	public List<SpriteRenderer> a_renderers;
	private int length_a_renderers;
	
	/// <summary>
	/// Gimme highlight sprites
	/// </summary>
	public Sprite _hilite_0;
	public Sprite _hilite_1;
	public Sprite _hilite_2;
	public Sprite _hilite_3;
	public List<Sprite> a_hilites;
	private int length_a_hilites;
	private SpriteRenderer myRenderer;
	private Sprite tile_snow_hilite;
	
	private float fadeValue = 1f;
	private float currentTime = 0f;
	private const float timeItTakesToFade = 0.5f;
	private bool isFading = false;
	private bool isAppearing = false;
	private bool isShining = false;

	private int prevChoice;
	public List<int> a_choice;
	private int length_a_choice;

	void Awake() {

	
		//Transform[] _allChildren = GetComponentsInChildren<Transform>();
		_allChildren = GetComponent<Transform> ();
		foreach (Transform _child in _allChildren)
		{
			//_child = GetComponentInChildren<Transform>();
			foreach(Transform _childchild in _child)
			{
				myRenderer = _childchild.GetComponent<SpriteRenderer>();
				a_renderers.Add(myRenderer);

				//Debug.Log (a_renderers.Count);
			}
			// Gimme sprite renderer from empty game object
			//myRenderer = _child.GetComponent<SpriteRenderer>();
			//Debug.Log(myRenderer); // tile_snow
			//myRenderer
				//Component.GetComponentInChildren();
			//Debug.Log (a_renderers);
		}

		//Debug.Log(a_renderers[0]);

		/*Transform[] _allChildren = GetComponentsInChildren<Transform>();
		foreach (Transform _child in _allChildren) {
			//Debug.Log(_child.GetComponent<SpriteRenderer>());
			myRenderer = _child.GetComponent<SpriteRenderer>();
			//Debug.Log(myRenderer);
			//a_renderers.Add(myRenderer);
			//Debug.Log(a_renderers);
		}*/

		/// <summary>
		/// Note however, that results from GetComponentsInChildren will also include that 
		/// component from the object itself. So the name is slightly misleading - it 
		/// should be thought of as "Get Components from Self And Children"!
		/// </summary>

		//_transform = GetComponent<Transform>();
		//foreach (Transform _child in transform)
		//{
			// Gimme sprite renderer from empty game object
		//	myRenderer = _child.GetComponent<SpriteRenderer>();
		//}

		/*_transform = GetComponent<Transform>();
		foreach (Transform _child in transform)
		{
		// Gimme sprite renderer from empty game object
			myRenderer = _child.GetComponent<SpriteRenderer>();
		}*/


		a_hilites.Add (_hilite_0);
		a_hilites.Add (_hilite_1);
		a_hilites.Add (_hilite_2);
		a_hilites.Add (_hilite_3);

		a_choice.Add (0);
		a_choice.Add (1);
		a_choice.Add (2);
		a_choice.Add (3);
		a_choice.Add (4);
		a_choice.Add (5);
		a_choice.Add (6);
		a_choice.Add (7);
		a_choice.Add (8);
		a_choice.Add (9);
		a_choice.Add (10);
		a_choice.Add (11);
		a_choice.Add (12);
		a_choice.Add (13);
		a_choice.Add (14);
	}
	


	void Start() {
		//StartFade();

		a_renderers.Capacity = 15;
		length_a_renderers = a_renderers.Count;
		a_choice.Capacity = 15;
		length_a_choice = a_choice.Count;

		length_a_hilites = a_hilites.Count;

		StartAppear();
	}
	
	void Update() {
		

		if(isFading){
			//Debug.Log("isFading");

			for(int index = 0; index < length_a_renderers; index++)
			{
				float fadeValue = Random.Range (0f,1f);
				currentTime += Time.deltaTime;
				if(currentTime <= timeItTakesToFade){
					fadeValue = 1f - (currentTime / timeItTakesToFade);
					a_renderers[index].color = new Color(1f,1f,1f, fadeValue);
				}else{
					isFading = false;
					isShining = false;
					PickSprite();
				}
			}
		}
		
		if(isAppearing){
			//Debug.Log("isAppearing");

			for(int index = 0; index < length_a_renderers; index++)
			{
				float fadeValue = Random.Range (0f,1f);
				currentTime += Time.deltaTime;
				if(currentTime <= timeItTakesToFade) {
					fadeValue = (currentTime / timeItTakesToFade);
					a_renderers[index].color = new Color(1f,1f,1f, fadeValue);
				}else{
					isAppearing = false;
					
					StartFade();
				}
			}
		}
	}
	
	private void StartFade() {
		currentTime = 0;
		isFading = true;
	}
	
	private void StartAppear() {
		PickSprite();
		currentTime = 0;
		isAppearing = true;
		//Debug.Log ("StartAppear()"); // Good.
	}
	
	private void PickSprite(){
		int index;
		for(index = 0; index < length_a_renderers; index++){
			int choice = Random.Range (0,length_a_hilites);
			// Ensure a different highlight is assigned.
			if(choice == a_choice[index])
			{
				choice = (choice+1) % length_a_hilites;
			}
			//Debug.Log (choice + " - " + length_a_hilites);
			a_renderers[index].sprite = a_hilites[choice];
			a_choice[index] = choice;
		}
		//Debug.Log ("PickSprite()"); // Good.
	}

	public void SetIsFading(){
		isFading = true;
	}
	public void SetIsAppearing(){
		if (!isShining) {
			StartAppear();
			isShining = true;
		}
	}
}