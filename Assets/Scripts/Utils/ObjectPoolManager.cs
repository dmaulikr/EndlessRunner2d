using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPoolManager {

	private static Dictionary<ObstacleRecycle, ObjectPool> objectPools = new Dictionary<ObstacleRecycle, ObjectPool> ();


	public static GameObject CreateInstance(GameObject prefab, Vector3 pos)
    {
		GameObject instance = null;

		var recycleScript = prefab.GetComponent<ObstacleRecycle>();
		if (recycleScript != null)
        {
			var objectPool = GetObjectPool (recycleScript);
			instance = objectPool.GetNextObstacle(pos).gameObject;
		}
        else
        {
			instance = GameObject.Instantiate (prefab);
			instance.transform.position = pos;
		}
		return instance;
	}

	public static void SuspendInstance(GameObject gameObject)
    {
		var recyleScript = gameObject.GetComponent<ObstacleRecycle> ();

		if (recyleScript != null){
			recyleScript.Suspend ();
		}
        else {
			GameObject.Destroy(gameObject);
		}
	}

	private static ObjectPool GetObjectPool(ObstacleRecycle item)
    {
		ObjectPool objectPool = null;

		if (objectPools.ContainsKey(item)) {
			objectPool = objectPools[item];
		}
        else {
			var poolContainer = new GameObject(item.gameObject.name + "ObjectPool");
			objectPool = poolContainer.AddComponent<ObjectPool>();
			objectPool.objectPrefab = item;

            objectPools.Add(item, objectPool);
		}
		return objectPool;
	}

}
