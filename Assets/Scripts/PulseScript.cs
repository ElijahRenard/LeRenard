using UnityEngine;
using System.Collections;

public class PulseScript : MonoBehaviour {
	
	private float fadeValue = 1f;
	private float currentTime = 0f;
	private const float timeItTakesToFade = 0.9f;
	private bool isFading = false;
	private bool isAppearing = false;
	
	Transform _transform;
	Transform _child;
	private SpriteRenderer myRenderer;

	public Sprite _hilite_0;
	public Sprite _hilite_1;
	public Sprite _hilite_2;
	
	void Start() {
		StartFade();
	}
	
	void Awake() {
		_transform = GetComponent<Transform>();
		foreach (Transform _child in transform)
		{
			// Gimme sprite renderer from empty game object
			myRenderer = _child.GetComponent<SpriteRenderer>();
		}
	}
	
	void Update() {


		if(isFading){
			float fadeValue = Random.Range (0.35f,0.85f);
			currentTime += Time.deltaTime;
			if(currentTime <= timeItTakesToFade){
				//fadeValue = 1f - (currentTime / timeItTakesToFade);
				fadeValue = 1f - (currentTime / timeItTakesToFade);
				myRenderer.color = new Color(1f,1f,1f, fadeValue);
				//Debug.Log("isFading value " + fadeValue);
			}else{
				isFading = false;
				StartAppear();
			}
		}
		
		if(isAppearing){
			float fadeValue = Random.Range (0.03f,0.05f);
			Debug.Log("isAppearing value before " + fadeValue);
			currentTime += Time.deltaTime;
			if(currentTime <= timeItTakesToFade) {
				//fadeValue = 1f + (currentTime / timeItTakesToFade);
				fadeValue = (currentTime / timeItTakesToFade);
				myRenderer.color = new Color(1f, 1f, 1f, fadeValue);
				Debug.Log("isAppearing value " + fadeValue);
			}else{
				isAppearing = false;
				StartFade();
			}
		}
	}
	
	private void StartFade() {
		currentTime = 0;
		isFading = true;

	}
	
	private void StartAppear() {
		currentTime = 0;
		isAppearing = true;
		PickSprite();
	}

	private void PickSprite(){
		int choice = Random.Range (0,3);
		switch (choice) {
				case 0: 
						myRenderer.sprite = _hilite_0;
						break;
				case 1: 
						myRenderer.sprite = _hilite_1;
						break;
				case 2:
						myRenderer.sprite = _hilite_2;
						break;
				}
	}
}
