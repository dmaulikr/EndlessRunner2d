using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPlacer : MonoBehaviour {

    private Transform floorTransform;

    private void Awake()
    {
        floorTransform = GetComponent<Transform>();
    }

    void Start ()
    {
        PositionFloor();   
	}

    void PositionFloor()
    {
        float floorHeight = floorTransform.localScale.y;

        Vector3 pos = floorTransform.transform.position;
        pos.x = 0;
        pos.y = -((Screen.height / CameraScaler.pixelScale) / 2) + (floorHeight / 2);

        floorTransform.transform.position = pos;
    }

}
