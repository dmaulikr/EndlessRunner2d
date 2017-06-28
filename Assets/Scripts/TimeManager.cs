using UnityEngine;
using System.Collections;
using System;

public class TimeManager : MonoBehaviour {

    public static bool Paused
    {
        get { return Time.timeScale == 0; }
    }

	public void ChangeTimeScaleTo(float value, float duration){

		if (Time.timeScale == 0)
			Time.timeScale = 0.1f;

		StartCoroutine(FadeTimeScaleTo (value, duration));
	}

	IEnumerator FadeTimeScaleTo(float value, float time){

		for (float t = 0f; t < 1; t += Time.deltaTime / time) {

			Time.timeScale = Mathf.Lerp(Time.timeScale, value, t);

			if(Mathf.Abs(value - Time.timeScale) < .01f){
				Time.timeScale = value;
				yield return null;
			}
			yield return null;
		}
	}

    public static void PauseTime()
    {
        Time.timeScale = 0;
    }

    public static float GetBestTime()
    {
        return PlayerPrefs.GetFloat("BestTime");
    }

    public static string FormatTimeForHUD(float value)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(value);
        return string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
    }

}
