using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {
	
	public static List<GameObject> catList = new List<GameObject>();
	public static List<GameObject> butterflyList = new List<GameObject>();
	public GameObject catPrefab;
	public GameObject butterflyPrefab;

	float max_ray_dist = 300f;

	void Start() {
		foreach (GameObject cat in catList) {
			Destroy(cat.gameObject);
		}
		catList.Clear();
		foreach (GameObject butterfly in butterflyList) {
			Destroy(butterfly.gameObject);
		}
		butterflyList.Clear();
	}

	void Update() {

		if (Input.GetMouseButtonDown(0)) {
			Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit tempRayHit = new RaycastHit();

			if (Physics.Raycast(tempRay, out tempRayHit, max_ray_dist)) {
				if (tempRayHit.collider.tag == "Floor") {
					GameObject tempCat = (GameObject) Instantiate(catPrefab, new Vector3(tempRayHit.point.x, 1f, tempRayHit.point.z), Quaternion.identity);
					catList.Add(tempCat);
				}
			}
		}
		else if (Input.GetMouseButtonDown(1)) {
			Ray tempRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit tempRayHit = new RaycastHit();
			
			if (Physics.Raycast(tempRay, out tempRayHit, max_ray_dist)) {
				if (tempRayHit.collider.tag == "Floor") {
					GameObject tempButterfly = (GameObject) Instantiate(butterflyPrefab, new Vector3(tempRayHit.point.x, 1f, tempRayHit.point.z), Quaternion.identity);
					butterflyList.Add(tempButterfly);
				}
			}
		}
	}
}
