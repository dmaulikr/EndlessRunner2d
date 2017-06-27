using System.Collections.Generic;
using UnityEngine;

public class ObstacleRecycle : MonoBehaviour {

	private List<IRecyclable> recyclableObjects;


	void Awake()
    {
		var scripts = GetComponents<MonoBehaviour> ();
		recyclableObjects = new List<IRecyclable> ();

        foreach (var script in scripts)
        {
			if(script is IRecyclable){
				recyclableObjects.Add (script as IRecyclable);
			}
		}
	}

	public void Revive()
    {
		gameObject.SetActive (true);

		foreach (var component in recyclableObjects) {
			component.Revive();
		}
	}

	public void Suspend()
    {
		gameObject.SetActive (false);

		foreach (var component in recyclableObjects) {
			component.Suspend();
		}
	}

}
