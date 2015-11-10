using UnityEngine;
using System.Collections;

public class MazeMaker : MonoBehaviour {

	public GameObject treePrefab;
	float spawn_range = 22f;
	int max_trees = 10;
	float spawn_height_low = -0.5f;
	float spawn_height_high = 2.0f;

	// Use this for initialization
	void Start () {
		int treeCount = 0;
		while (treeCount < max_trees) {
			Instantiate(treePrefab, new Vector3(Random.Range(-spawn_range, spawn_range), Random.Range(spawn_height_low, spawn_height_high), Random.Range(-spawn_range, spawn_range)), Quaternion.identity);
			treeCount++;
		}
	}
}
