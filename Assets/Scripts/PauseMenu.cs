using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject title;
    public GameObject resume;
    public GameObject resumetext;
    public GameObject bg;
    public GameObject mainmenu;
    public GameObject mainmenutext;

    bool _paused = false;
    bool paused
    {
        get { return _paused; }
        set
        {
            _paused = value;
            title.SetActive(value);
            resume.SetActive(value);
            resumetext.SetActive(value);
            resumetext.GetComponent<MenuFade>().Start();
            mainmenutext.GetComponent<MenuFade>().Start();
            bg.SetActive(value);
            mainmenu.SetActive(value);
            mainmenutext.SetActive(value);
            Time.timeScale = value ? 0 : 1;
        }
    }

    // Use this for initialization
    void Start()
    {

        paused = false;

        foreach (var item in new GameObject[] { resumetext, mainmenutext })
        {
            Localiser.LocaliseUIElement(item.GetComponent<Text>());
            item.SetActive(false);
        }


    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            paused ^= true;

        }

    }

    public void Resume()
    {

        paused = false;


    }

}
