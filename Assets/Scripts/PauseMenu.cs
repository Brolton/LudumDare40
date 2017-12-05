using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour {

    public GameObject title;
    public GameObject resume;
    public GameObject resumetext;
    public GameObject bg;
    bool paused = false;

	// Use this for initialization
	void Start () {

	title.SetActive(false);
        resume.SetActive(false);
        resumetext.SetActive(false);
        bg.SetActive(false);
        Time.timeScale = 1;
        

    }
	
	// Update is called once per frame
	void Update () {
		
        if (paused == true)
        {
            
            Time.timeScale = 0;
            title.SetActive(true);
            resume.SetActive(true);
            resumetext.SetActive(true);
            bg.SetActive(true);

        } else
        {
            
            Time.timeScale = 1;
            title.SetActive(false);
            resume.SetActive(false);
            resumetext.SetActive(false);
            bg.SetActive(false);

        }

        if (Input.GetKey(KeyCode.Escape))
        {

            paused = true;

        }

	}

    public void Resume()
    {

        paused = false;

    }

}
