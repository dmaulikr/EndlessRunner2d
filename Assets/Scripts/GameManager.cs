using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameManager : MonoBehaviour {

	public GameObject playerPrefab;
	public Text continueText;
	public Text scoreText;

	private float timeElapsed = 0f;
	private float bestTime = 0f;
	private float blinkTime = 0f;
	private bool blink;
	private bool gameStarted;
	private TimeManager timeManager;
	private GameObject player;
	private GameObject floor;
	private ObstacleSpawner spawner;
	private bool beatBestTime;

	void Awake()
    {
		floor = GameObject.Find ("Foreground");
		spawner = GameObject.Find ("Spawner").GetComponent<ObstacleSpawner> ();
		timeManager = GetComponent<TimeManager> ();
	}

	void Start ()
    {
        PositionFloor();

		spawner.isActive = false;

        TimeManager.PauseTime();
        bestTime = TimeManager.GetBestTime();

		continueText.text = "PRESS ANY BUTTON TO START";
	}

    void PositionFloor()
    {
        float floorHeight = floor.transform.localScale.y;

        Vector3 pos = floor.transform.position;
        pos.x = 0;
        pos.y = -((Screen.height / CameraScaler.pixelScale) / 2) + (floorHeight / 2);

        floor.transform.position = pos;
    }
	
	void Update ()
    {
		if (!gameStarted && TimeManager.Paused) {

			if(Input.anyKeyDown){
				timeManager.ChangeTimeScaleTo(1, 1f);
				ResetGame();
			}
		}
        BlinkHUDText();
        DisplayHUD();

        timeElapsed += Time.deltaTime;
	}

    void DisplayHUD()
    {
        if (!gameStarted)
        {
            continueText.canvasRenderer.SetAlpha(blink ? 0 : 1);
            string textColor = beatBestTime ? "#FF0" : "#FFF";

            scoreText.text = "TIME: " + TimeManager.FormatTimeForHUD(timeElapsed) +
                "\n<color=" + textColor + ">" + "BEST: " + TimeManager.FormatTimeForHUD(bestTime) + "</color>";
        }
        else
        {
            scoreText.text = "TIME: " + TimeManager.FormatTimeForHUD(timeElapsed);
        }
    }

    void BlinkHUDText()
    {
        blinkTime++;
        if(blinkTime % 40 == 0)
        {
            blink = !blink;
        }
    }

	void OnPlayerKilled(){
		spawner.isActive = false;

		var playerDestroyScript = player.GetComponent<ObstacleSuspendOffscreen> ();
		playerDestroyScript.Killed -= OnPlayerKilled;

		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		timeManager.ChangeTimeScaleTo (0, 5.5f);
		gameStarted = false;

		continueText.text = "PRESS ANY BUTTON TO RESTART";

		if (timeElapsed > bestTime) {
			bestTime = timeElapsed;
			PlayerPrefs.SetFloat("BestTime", bestTime);
			beatBestTime = true;
		}
	}

	void ResetGame(){
		spawner.isActive = true;

		player = ObjectPoolManager.CreateInstance(playerPrefab, new Vector3(0, (Screen.height/CameraScaler.pixelScale)/2 + 100, 0));

		var playerDestroyScript = player.GetComponent<ObstacleSuspendOffscreen> ();
		playerDestroyScript.Killed += OnPlayerKilled;

		gameStarted = true;

		continueText.canvasRenderer.SetAlpha(0);

		timeElapsed = 0;
		beatBestTime = false;
	}
}
