using UnityEngine;
using System.Collections;

public class Cat : MonoBehaviour {

	public Transform butterfly;
	Movement moveScript;

	public AudioSource meow;
	public AudioSource win;

	bool pouncing = false;

	float cat_movespeed = 10.0f;
	float frontal_detection_cone = 45f;
	float frontal_detection_range = 30f;
	float catch_range = 6f;
	float chase_speed = 800f;
	float pounce_chance = 0.02f;
	float pounce_speed = 2000f;
	float pounce_duration = 0f;
	float max_pounce_duration = 0.2f;

	// Use this for initialization
	void Start () {
		moveScript = GetComponent<Movement>();
		moveScript.movespeed = cat_movespeed;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//moveScript.movespeed = pouncing ? cat_movespeed * 2 : cat_movespeed;
		//Debug.Log (moveScript.movespeed);
		if (butterfly != null) {
			moveScript.movespeed = pouncing ? cat_movespeed * 3 : cat_movespeed;
			Vector3 directionToButterfly = butterfly.position - transform.position;
			float angle = Vector3.Angle(transform.forward, directionToButterfly);
			if ( angle < frontal_detection_cone) {
				Ray catRay = new Ray(transform.position, directionToButterfly);
				RaycastHit catRayHitInfo = new RaycastHit();
				if (Physics.Raycast(catRay, out catRayHitInfo, 100f)) {
					// cat sees butterfly
					if (catRayHitInfo.collider.tag == "Butterfly") {
						if (!pouncing && Random.value < pounce_chance) {
							pouncing = true;
							//Debug.Log ("pounced");
						}
						if (catRayHitInfo.distance <= catch_range) {
							win.Play();
							Destroy(butterfly.gameObject);
							//Time.timeScale = 0;
						}
						else {
							if (catRayHitInfo.distance < frontal_detection_range) {
								if (!meow.isPlaying) {
									meow.Play();
								}
								GetComponent<Rigidbody>().AddForce(directionToButterfly.normalized * (pouncing ? pounce_speed : chase_speed));
							}
						}
					}
				}
			}
			if (pouncing) {
				pounce_duration += Time.deltaTime;
				if (pounce_duration >= max_pounce_duration) {
					pouncing = false;
					//Debug.Log ("done");
				}
			}
			//increment pounce duration
		}
		else {
			moveScript.movespeed = 0f;
		}

	}
}
