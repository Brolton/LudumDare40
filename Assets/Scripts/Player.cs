using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = normalSpeed;
    public Rigidbody2D rb2D;
    public float heartbeat = 0;     // value between 0 and 100
    public float lightVicinity;

    GameManager gameManager;

    public const float heartbeatDecreasePerPickedUpFirefly = 25f;     // TO BE CHANGED (placeholder value)
    public const float lengthOfDecreasingHeartbeatAfterPickedUpFirefly = 5f;
    public const float decreaseOfHeartbeatInOneSecond = heartbeatDecreasePerPickedUpFirefly / lengthOfDecreasingHeartbeatAfterPickedUpFirefly;
    public const float maxHeartbeatForRunning = 80f;
    public const float normalSpeed = 1f;
    public const float runningSpeed = 2f;
    public const string enemyTag = "enemy";

    private float continuoslyDecreaseHeartbeat;

	public SpriteRenderer sprite;

    void Start()
    {
        
    }

    void FixedUpdate()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        if (isRunning() && heartbeat < maxHeartbeatForRunning)
        {
            continuoslyDecreaseHeartbeat = 0f;   // heartbeat decrease reseted after start of running
            speed = 2f;
        }
        else
        {
            speed = 1f;
        }

        rb2D.velocity = new Vector2(h, v) * speed;
		if (h != 0)
			sprite.flipX = (h < 0);
        

        if (heartbeat < 0f)
        {
            heartbeat = 0f;
        }

        if (heartbeat > 100f)
        {
            gameManager.GameOver();
        }

        if (continuoslyDecreaseHeartbeat > 0f)
        {
            heartbeat -= decreaseOfHeartbeatInOneSecond * Time.deltaTime;
            continuoslyDecreaseHeartbeat -= decreaseOfHeartbeatInOneSecond;
        }
        else
        {
            heartbeat += 1f * Time.deltaTime;     // heartbeat always going up ?
        }
    }

    bool isRunning()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            return true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            return false;
        }

        return true;
    }

    public void FireflyPickedUp()
    {
        DestroyEnemiesInsideLightVicinity();

        continuoslyDecreaseHeartbeat = heartbeatDecreasePerPickedUpFirefly;            
    }

    public void DestroyEnemiesInsideLightVicinity()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag(enemyTag))
        {
            if (Vector2.Distance(this.transform.localPosition, enemy.transform.localPosition) < lightVicinity)
            {
                // Effect ?
                Destroy(enemy);
            }
        }
    }
}
