using UnityEngine;
using System.Collections;

public class WaypointScript : MonoBehaviour {

	public int index;
	public float radius = 0.5f;

	bool used;

	public bool GetUsed(){
		return used;
	}
	public void SetUsed(bool b){
		used = b;
	}

	void OnDrawGizmosSelected() {
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, radius);
	}
}