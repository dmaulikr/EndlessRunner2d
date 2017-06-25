using UnityEngine;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour {
	
	public float spawnDelay = 2.0f;
    public Vector2 spawnDelayRange = new Vector2(1, 2);

    public bool isActive = true;
	
    public GameObject[] obstaclePrefabs;


    void Start()
    {
		ResetSpawnDelay ();
		StartCoroutine (GenerateObstacles ());
	}

	IEnumerator GenerateObstacles(){

		yield return new WaitForSeconds (spawnDelay);

		if (isActive)
        {
			GameObjectUtil.Instantiate(obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)], transform.position);
			ResetSpawnDelay();
		}
		StartCoroutine (GenerateObstacles ());
	}

	void ResetSpawnDelay()
    {
		spawnDelay = Random.Range (spawnDelayRange.x, spawnDelayRange.y);
	}

}
