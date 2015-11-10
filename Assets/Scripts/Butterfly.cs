using UnityEngine;
using System.Collections;

public class Butterfly : MonoBehaviour {
	
	Movement moveScript;

	float butterfly_movespeed = 13f;
	float frontal_detection_range = 15f;
	float frontal_detection_cone = 45f;
	float circular_detection_range = 10f;
	float panic_speed = 1100f;
	float check_cornered_distance = 10f;

	void Start () {
		moveScript = GetComponent<Movement>();
		moveScript.movespeed = butterfly_movespeed;
	}

	void FixedUpdate() {
		foreach (GameObject cat in GameManager.catList) {
			Vector3 directionToCat = cat.transform.position - transform.position;
			//if ( Vector3.Angle(transform.forward, directionToCat) < 180f) {
			Ray butterflyRay = new Ray(transform.position, directionToCat);
			RaycastHit butterflyRayHitInfo = new RaycastHit();
			if (Physics.Raycast(butterflyRay, out butterflyRayHitInfo, 100f)) {
				if (butterflyRayHitInfo.collider.tag == "Cat") {
					Ray ray_corner_check = new Ray(transform.position, -transform.forward);
					// if cat is in front && within front detection range
					//     if not cornered
					//          turn around
					if ( Vector3.Angle(transform.forward, directionToCat) < frontal_detection_cone && butterflyRayHitInfo.distance < frontal_detection_range ) {
						if (!Physics.Raycast(ray_corner_check, check_cornered_distance)) {
							transform.Rotate(0f, 180f, 0f);
						}
					}
					// if cat is within circular detection range
					//     panic
					if ( butterflyRayHitInfo.distance < circular_detection_range) {
						GetComponent<Rigidbody>().AddForce(-directionToCat.normalized * panic_speed);
					}
				}
				//}
			}
		}
	}

	void OnDestroy() {
		GameManager.butterflyList.Remove(this.gameObject);
	}
}
