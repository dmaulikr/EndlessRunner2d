using UnityEngine;

public class ObstacleSuspendOffscreen : MonoBehaviour {

	public delegate void OnKill();
	public event OnKill Killed;

    private bool isOffscreen;
    public float offset = 16f;
	private float offscreenThresholdX = 0;

    private Rigidbody2D rigidbody2d;


	void Awake()
    {
		rigidbody2d = GetComponent<Rigidbody2D> ();
	}

	void Start ()
    {
		offscreenThresholdX = (Screen.width / CameraScaler.pixelScale) / 2 + offset;
	}
	
	void Update ()
    {

		var positionX = transform.position.x;
		var directionX = rigidbody2d.velocity.x;

        isOffscreen = CheckIfOffscreen(positionX, directionX);

		if (isOffscreen) {
			Kill();
		}

	}

    private bool CheckIfOffscreen( float xposition, float xdirection)
    {
        bool offscreen = false;
        if (Mathf.Abs(xposition) > offscreenThresholdX)
        {
            if (xdirection < 0 && xposition < -offscreenThresholdX){
                offscreen = true;
            }
            else if (xdirection > 0 && xposition > offscreenThresholdX){
                offscreen = false;
            }
        }
        else
        {
            offscreen = false;
        }
        return offscreen;
    }


	public void Kill()
    {
		isOffscreen = false;
		GameObjectUtil.Destroy (gameObject);

		if (Killed != null) {
			Killed();
		}
	}
}
