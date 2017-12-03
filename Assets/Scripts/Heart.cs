using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart : MonoBehaviour
{
    enum ScaleState
    {
        INCR,
        DECR
    }

    public Image heartImage;

	public float secondaryMaxScaleFactor = 0.8f;


    float currentScale = 1f;
    float curMaxScale;
    float primaryMaxScale;
    float secondaryMaxScale;
    bool secondaryBeat;
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
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (curScaleState == ScaleState.INCR)
        {
            currentScale += curSpeed * Time.deltaTime;
            if (currentScale >= curMaxScale)
            {
                currentScale = curMaxScale;
                curScaleState = ScaleState.DECR;
                secondaryBeat ^= true;
                curMaxScale = secondaryBeat ? secondaryMaxScale : primaryMaxScale;
            }
        }
        else if (curScaleState == ScaleState.DECR)
        {
            currentScale -= curSpeed * Time.deltaTime;
            if (currentScale <= curMinScale)
            {
                currentScale = curMinScale;
                curScaleState = ScaleState.INCR;
            }
        }

        heartImage.GetComponent<RectTransform>().localScale = new Vector3(currentScale, currentScale);
    }

    public void SetBpm(float bpmPercent)
    {
		curMinScale = normalMinScale + (panicMinScale - normalMinScale) * bpmPercent / 100f;
        curSpeed = normalSpeed + (panicSpeed - normalSpeed) * bpmPercent / 100f;

		primaryMaxScale = normalMaxScale + (panicMaxScale - normalMaxScale) * bpmPercent / 100f;
		
		secondaryMaxScale = Mathf.Max(primaryMaxScale * secondaryMaxScaleFactor, curMinScale);
    }
}
