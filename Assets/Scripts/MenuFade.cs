using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;
using UnityEngine.SceneManagement;

public class MenuFade : MonoBehaviour
{

    public float lerpTime = 0;
    public float lerpTimeStop = 1;
    public CanvasRenderer texture;
    public bool done = false;

    // Use this for initialization
    public void Start()
    {

        texture = this.GetComponent<CanvasRenderer>();

        texture.SetAlpha(0);

        lerpTime = 0;

    }

    // Update is called once per frame
    void Update()
    {

        lerpTime += Time.unscaledDeltaTime / 3;

        if (lerpTime >= lerpTimeStop)
        {

            lerpTime = lerpTimeStop;

        }

        texture.SetAlpha(lerpTime);

        if (lerpTime == 1)
        {

            StartCoroutine(Flicker());

        }
    }

    IEnumerator Flicker()
    {

        while (lerpTime == 1)
        {

            yield return new WaitForSeconds(UnityEngine.Random.Range(.3f, .7f));

            done = true;

            if (done == true)
            {
                texture.SetAlpha(UnityEngine.Random.Range(.7f, 1));
                done = false;
            }

        }

    }

    public void OnPointerEnter()
    {
        this.GetComponent<Text>().font = (Font)Resources.Load("Amatic-Bold");
        this.GetComponent<Text>().fontStyle = FontStyle.Bold;
    }

    public void OnPointerExit()
    {
        this.GetComponent<Text>().font = (Font)Resources.Load("AmaticSC-Regular");
        this.GetComponent<Text>().fontStyle = FontStyle.Normal;
    }

}
