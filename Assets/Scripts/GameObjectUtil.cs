using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectUtil {

	private static Dictionary<ObstacleRecycle, ObstaclePool> pools = new Dictionary<ObstacleRecycle, ObstaclePool> ();

	public static GameObject Instantiate(GameObject prefab, Vector3 pos){
		GameObject instance = null;

		var recycledScript = prefab.GetComponent<ObstacleRecycle> ();
		if (recycledScript != null) {
			var pool = GetObjectPool (recycledScript);
			instance = pool.GetNextObstacle (pos).gameObject;
		} else {

			instance = GameObject.Instantiate (prefab);
			instance.transform.position = pos;
		}
		return instance;
	}

	public static void Destroy(GameObject gameObject){

		var recyleGameObject = gameObject.GetComponent<ObstacleRecycle> ();

		if (recyleGameObject != null) {
			recyleGameObject.Suspend ();
		} else {
			GameObject.Destroy (gameObject);
		}
	}

	private static ObstaclePool GetObjectPool(ObstacleRecycle reference){
		ObstaclePool pool = null;

		if (pools.ContainsKey (reference)) {
			pool = pools [reference];
		} else {
			var poolContainer = new GameObject(reference.gameObject.name + "ObjectPool");
			pool = poolContainer.AddComponent<ObstaclePool>();
			pool.obstaclePrefab = reference;
			pools.Add (reference, pool);
		}

		return pool;
	}

}
