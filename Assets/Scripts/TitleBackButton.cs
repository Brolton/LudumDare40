using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleBackButton : MonoBehaviour {

    public bool options = false;
    public bool credits = false;
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

    public GameObject creditstitle;
    public GameObject creditsbox;

    public static AudioSource music;
    public static AudioSource sfx;
    public GameObject optionstitle;
    public GameObject musicVol;
    public GameObject musicVolSlider;
    public GameObject sfxVol;
    public GameObject sfxVolSlider;
    public Slider musicVolSliderControl;
    public Slider sfxVolSliderControl;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		Debug.Log(musicVolSliderControl.value);
        Debug.Log(sfxVolSliderControl.value);
		
        if (options == true)
        {

            optionstitle.SetActive(true);
            musicVol.SetActive(true);
            musicVolSlider.SetActive(true);
            sfxVol.SetActive(true);
            sfxVolSlider.SetActive(true);

            music.volume = musicVolSliderControl.value;
            sfx.volume = sfxVolSliderControl.value;

			Debug.Log (music.volume);
			Debug.Log (sfx.volume);
			// Wwise
			AkSoundEngine.SetRTPCValue("RTPC_Music_Volume", 0);		// Wwise set Music volume
			AkSoundEngine.SetRTPCValue("RTPC_SFX_Volume", 0);       // Wwise set SFX volume

        }

        if (credits == true)
        {

            creditstitle.SetActive(true);
            creditsbox.SetActive(true);


        }

    }

    public void onClick()
    {

        options = false;
        credits = false;

        button1.SetActive(true);
        button2.SetActive(true);
        button3.SetActive(true);
        button4.SetActive(true);
        title.SetActive(true);
        text1.SetActive(true);
        text2.SetActive(true);
        text3.SetActive(true);
        text4.SetActive(true);
        creditstitle.SetActive(false);
        creditsbox.SetActive(false);
        optionstitle.SetActive(false);
        musicVol.SetActive(false);
        musicVolSlider.SetActive(false);
        sfxVol.SetActive(false);
        sfxVolSlider.SetActive(false);
        this.gameObject.SetActive(false);

    }

}
