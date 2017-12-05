using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score { get; private set; }
    public List<int> highScores = new List<int>();

    public GameObject gameOverText;
    public GameObject scoreText;
    string formatText;

    public int scoresSaved = 20; // to be changed, once the UI is implemented

    public const int FireflyDtScore = 100;

    public GameObject buttonTryAgain;
    public GameObject buttonMainMenu;

    public GameObject textTryAgain;
    public GameObject textMainMenu;

    void Start()
    {
        foreach (var item in new GameObject[] { gameOverText, scoreText, textTryAgain, textMainMenu })
        {
            Localiser.LocaliseUIElement(item.GetComponent<Text>());
            item.SetActive(false);
        }
        formatText = scoreText.GetComponent<Text>().text;
        buttonTryAgain.SetActive(false);
        buttonMainMenu.SetActive(false);
    }

    void Update()
    {

    }

    public void GameOver()
    {
        gameOverText.SetActive(true);
        scoreText.SetActive(true);
        buttonTryAgain.SetActive(true);
        buttonMainMenu.SetActive(true);
        textTryAgain.SetActive(true);
        textMainMenu.SetActive(true);

        var textComponent = scoreText.GetComponent<Text>();
        textComponent.text = string.Format(formatText, score);
        highScores.Add(score);
        highScores.Sort((a, b) => b - a);
        if (highScores.Count > scoresSaved)
        {
            highScores.RemoveAt(scoresSaved);
        }
        score = 0;
        // Effect?
        // Game Over screen

        AkSoundEngine.PostEvent("PLAY_death", gameObject);                  // Wwise play death sound
        AkSoundEngine.PostEvent("STOP_heartbeat", gameObject);              // Wwise stop heartbeat sound
        AkSoundEngine.PostEvent("STOP_lantern", gameObject);                // Wwise stop lantern sound
        AkSoundEngine.PostEvent("STOP_heartsAndFireflies", gameObject); // Wwise stop MUSIC
        AkSoundEngine.PostEvent("STOP_monster_ALL", gameObject);			// Wwise stop all Monsters
    }

    public void PickUpFirefly() { score += FireflyDtScore; }

    public void onTryAgainClick()
    {
        Debug.Log("test");
        SceneManager.LoadSceneAsync("GamePlay");
        //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void onMainMenuClick() {
        Debug.Log("test2");
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
