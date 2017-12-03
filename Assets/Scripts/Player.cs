using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = normalSpeed;
    public Rigidbody2D rb2D;
    public float heartbeat = 0;     // value between 0 and 100
    public float lightVicinity;

    public GameManager gameManager;

    public FireflyManager fireflyManager;

    public const float heartbeatDecreasePerPickedUpFirefly = 25f;     // TO BE CHANGED (placeholder value)
    public const float lengthOfDecreasingHeartbeatAfterPickedUpFirefly = 5f;
    public const float decreaseOfHeartbeatInOneSecond = heartbeatDecreasePerPickedUpFirefly / lengthOfDecreasingHeartbeatAfterPickedUpFirefly;
    public const float maxHeartbeatForRunning = 80f;
    public const float normalSpeed = 1f;
    public const float runningSpeed = 2f;
    public const float normalHeartbeatIncrease = 1f;
    public const float runningHeartbeatIncrease = 4f;
    public const string enemyTag = "enemy";

    private float continuoslyDecreaseHeartbeat;

	public SpriteRenderer sprite;

    public Player instance;

    void Start()
    {
        instance = this;
    }

    void FixedUpdate()
    {
        Debug.Log(continuoslyDecreaseHeartbeat);

        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(KeyCode.LeftShift) && heartbeat < maxHeartbeatForRunning)
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
            continuoslyDecreaseHeartbeat -= decreaseOfHeartbeatInOneSecond * Time.deltaTime;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                heartbeat += runningHeartbeatIncrease * Time.deltaTime;
            }
            else
            {
                heartbeat += normalHeartbeatIncrease * Time.deltaTime;  // Needed ?
            }           
        }
    }

    public void FireflyPickedUp()
    {
        DestroyEnemiesInsideLightVicinity();

        instance.continuoslyDecreaseHeartbeat = heartbeatDecreasePerPickedUpFirefly;            
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

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "firefly")
        {
            FireflyPickedUp();
            fireflyManager.Spawn();
            Destroy(col.gameObject);
        }
    }
}
