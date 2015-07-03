using UnityEngine;
using System.Collections;

public class TimeScript : MonoBehaviour {


	float startTime = 0.0f;
	float currentTime;

	int TimeSec;
	int lastInterval;
	// Use this for initialization
	void Start () {
		startTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		CalculateTime ();
	}

	void CalculateTime()
	{
		currentTime = Time.time + startTime;
		TimeSec = Mathf.CeilToInt (currentTime);
	}

	public bool TimeInterval(int timeInterval)
	{
		bool value = false;
		if(TimeSec % timeInterval == 0)
		{
			if(lastInterval  != TimeSec)
			{
				value = true;
				lastInterval = TimeSec;
			}
			else
			{
				value = false;
			}
		}
		return value;
	}
}
