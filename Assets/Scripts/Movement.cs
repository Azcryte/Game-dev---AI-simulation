using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float movespeed;

	Rigidbody rbody;
	float ray_length = 4.0f;
	float side_ray_adjust = 1.0f;

	// Use this for initialization
	void Start () {
		rbody = GetComponent<Rigidbody>();
	}

	void FixedUpdate() {
		//Debug.Log (movespeed);
		rbody.velocity = transform.forward * movespeed + Physics.gravity;
		Ray moveRay_forward = new Ray(transform.position, transform.forward);
		Ray moveRay_right= new Ray(transform.position, transform.right);
		Ray moveRay_left = new Ray(transform.position, -transform.right);
		if (Physics.Raycast(moveRay_forward, ray_length)) {
			float rand = Random.value;
			//turn around
			if (rand < 0.15f) {
				transform.Rotate (new Vector3(0f, 180f, 0f));
			}
			//turn logic
			else {
				//check sides
				if (Physics.Raycast (moveRay_right, ray_length + side_ray_adjust)) {
					transform.Rotate(new Vector3(0f, -90f, 0f));
				}
				else if (Physics.Raycast (moveRay_left, ray_length + side_ray_adjust)) {
					transform.Rotate(new Vector3(0f, 90f, 0f));
				}	
				else {
					if (rand < 0.575f) {
						transform.Rotate(new Vector3(0f, 90f, 0f)); 
					}
					else {
						transform.Rotate(new Vector3(0f, -90f, 0f));
					}
			}
			}
		}
	}
}
