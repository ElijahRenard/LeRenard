using UnityEngine;
using System.Collections;

public class PickupCheese : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D (Collider2D other)
	{
		//Debug.Log (other + " " + other.tag);
		// If the player enters the trigger zone...
		if(other.tag == "Player")
		{
			// Get a reference to the player health script.
			//PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
			
			// Increasse the player's health by the health bonus but clamp it at 100.
			//playerHealth.health += healthBonus;
			//playerHealth.health = Mathf.Clamp(playerHealth.health, 0f, 100f);
			
			// Update the health bar.
			//playerHealth.UpdateHealthBar();
			
			// Trigger a new delivery.
			//pickupSpawner.StartCoroutine(pickupSpawner.DeliverPickup());
			
			// Play the collection sound.
			//AudioSource.PlayClipAtPoint(collect,transform.position);
			
			// Destroy the crate.
			//Destroy(transform.root.gameObject);

			Debug.Log ("Yummy!");

			//// Is this a shot?
			//PickupCheese cheese = Collider2D.gameObject.GetComponent<PickupCheese>();
			//if (cheese != null)
			//{
				Debug.Log ("This fox likes cheese");
				// eat the cheese
			Destroy(gameObject);	

				// Destroy the cheese object
				// Remember to always target the game object,
				// otherwise you will just remove the script.
				//Destroy(shot.gameObject);
			//}
		}
		// Otherwise if the crate hits the ground...
		//else if(other.tag == "ground" && !landed)
		//{
			// ... set the Land animator trigger parameter.
			//anim.SetTrigger("Land");
			
			//transform.parent = null;
			//gameObject.AddComponent<Rigidbody2D>();
			//landed = true;	
		//}
	}
}
