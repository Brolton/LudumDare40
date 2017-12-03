using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour 
{
	enum ScaleState {
		INCR,
		DECR
	}

	float currentScale = 1f;
	float curMaxScale;
	float curMinScale;
	float curSpeed; // scale per second
	ScaleState curScaleState = ScaleState.INCR;

	public float normalMaxScale = 1.1f;
	public float normalMinScale = 0.9f;
	public float normalSpeed = 0.5f; // scale per second

	public float panicMaxScale = 1.2f;
	public float panicMinScale = 0.8f;
	public float panicSpeed = 1.0f; // scale per second

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (curScaleState == ScaleState.INCR) {
			currentScale += curSpeed * Time.deltaTime;
			if (currentScale >= curMaxScale) {
				currentScale = curMaxScale;
				curScaleState = ScaleState.DECR;
			}
		}
		else if (curScaleState == ScaleState.DECR) {
			currentScale -= curSpeed * Time.deltaTime;
			if (currentScale <= curMinScale) {
				currentScale = curMinScale;
				curScaleState = ScaleState.INCR;
			}
		}

		GetComponent<RectTransform> ().localScale = new Vector3 (currentScale, currentScale);
	}

	public void SetBpm(float bpmPercent) 
	{
		curMaxScale = normalMaxScale + (panicMaxScale - normalMaxScale) * bpmPercent / 100f;
		curMinScale = normalMinScale + (panicMinScale - normalMinScale) * bpmPercent / 100f;
		curSpeed = normalSpeed + (panicSpeed - normalSpeed) * bpmPercent / 100f;
	}
}
