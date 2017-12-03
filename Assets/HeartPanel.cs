using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartPanel : MonoBehaviour {

	public Heart heart;
	public Slider slider;

	float curBpm = -1;

	// Use this for initialization
	void Start () {
		SetHeartbeat (0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SetHeartbeat (float heartbeat)
	{
		if (curBpm == heartbeat)
			return;

		curBpm = heartbeat;
		slider.value = heartbeat;
		heart.SetBpm (heartbeat);
	}
}
