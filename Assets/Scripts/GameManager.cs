using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score { get; private set; }
    public List<int> highScores = new List<int>();

    public int scoresSaved = 20; // to be changed, once the UI is implemented

    public const int FireflyDtScore = 100;

    void Start()
    {

    }

    void Update()
    {

    }

    public void GameOver()
    {
        highScores.Add(score);
        highScores.Sort((a, b) => b - a);
        if (highScores.Count > scoresSaved)
        {
            highScores.RemoveAt(scoresSaved);
        }
        score = 0;
        // Effect?
        // Game Over screen

		AkSoundEngine.PostEvent ("PLAY_death", gameObject);					// Wwise play death sound
		AkSoundEngine.PostEvent ("STOP_heartbeat", gameObject);				// Wwise stop heartbeat sound
    }

    public void PickUpFirefly() { score += FireflyDtScore; }
}
