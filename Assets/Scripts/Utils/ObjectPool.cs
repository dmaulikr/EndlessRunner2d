using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

	public ObstacleRecycle objectPrefab;

	private List<ObstacleRecycle> obstaclePool = new List<ObstacleRecycle>();


	public ObstacleRecycle GetNextObstacle(Vector3 pos)
    {
		ObstacleRecycle obstacle = null;

		foreach (var item in obstaclePool)
        {
			if(item.gameObject.activeSelf != true){
				obstacle = item;
				obstacle.transform.position = pos;
			}
		}

		if(obstacle == null)
			obstacle = CreateNewObstacle(pos);

		obstacle.Revive();

		return obstacle;
	}

    private ObstacleRecycle CreateNewObstacle(Vector3 pos)
    {
        var obstacle = GameObject.Instantiate(objectPrefab);
        obstacle.transform.position = pos;
        obstacle.transform.parent = transform;

        obstaclePool.Add(obstacle);

        return obstacle;
    }

}
