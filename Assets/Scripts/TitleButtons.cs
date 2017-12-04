using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtons : MonoBehaviour {

    public GameObject button;
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject button5;
    public GameObject title;
    public GameObject text1;
    public GameObject text2;
    public GameObject text3;
    public GameObject text4;
    public GameObject text5;
    public GameObject controls;

    // Use this for initialization
    void Start () { 

        button.SetActive(false);
		AkSoundEngine.PostEvent ("STOP_heartsAndFireflies", gameObject);	// Wwise stop MUSIC to avoid duplicate music
		AkSoundEngine.SetRTPCValue ("RTPC_HeartBeat", 0);					// Wwise set heartbeat to 0
		AkSoundEngine.PostEvent ("PLAY_heartsAndFireflies", gameObject);	// Wwise play MUSIC
		AkSoundEngine.PostEvent("PLAY_Lantern", gameObject);				// Wwise play lantern fire sound
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void OnClickPlay()
    {

        SceneManager.LoadSceneAsync("GamePlay");
		AkSoundEngine.PostEvent ("PLAY_game_START", gameObject);			// Wwise play gameStart


    }

    public void OnClickOptions()
    {
            
	    button.GetComponent<TitleBackButton>().options = true;
            button.SetActive(true);
            button1.SetActive(false);
            button3.SetActive(false);
            button4.SetActive(false);
            title.SetActive(false);
            text1.SetActive(false);
            text2.SetActive(false);
            text3.SetActive(false);
            text4.SetActive(false);
        controls.SetActive(false);

        button.GetComponent<TitleBackButton>().options = true;

            button2.SetActive(false);

    }

    public void OnClickCredits()
    {

        button.SetActive(true);
        button1.SetActive(false);
        button2.SetActive(false);
        button4.SetActive(false);
        title.SetActive(false);
        text1.SetActive(false);
        text2.SetActive(false);
        text3.SetActive(false);
        text4.SetActive(false);
        controls.SetActive(false);

        button.GetComponent<TitleBackButton>().credits = true;

        button3.SetActive(false);

    }

    public void OnClickExit()
    {

        Application.Quit();

    }

}
