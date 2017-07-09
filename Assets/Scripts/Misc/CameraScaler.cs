using UnityEngine;

public class CameraScaler : MonoBehaviour {

	public static float pixelScale = 1f;

	public Vector2 nativeResolution = new Vector2 (240, 160);

	void Awake ()
    {
		var camera = GetComponent<Camera> ();

        SetCameraSize(camera);
	}

    void SetCameraSize(Camera camera)
    {
        if (camera.orthographic)
        {
            pixelScale = Screen.height / nativeResolution.y;
            camera.orthographicSize = (Screen.height / 2.0f) / pixelScale;
        }
    }
}
