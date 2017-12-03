using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = normalSpeed;
    public Rigidbody2D rb2D;
    public float heartbeat = 0f;     // value between 0 and 100
    public float lightVicinity = 15f;     // TO BE CHANGED

    public GameManager gameManager;

    public FireflyManager fireflyManager;

    public const float heartbeatDecreasePerPickedUpFirefly = 25f;
    public const float lengthOfDecreasingHeartbeatAfterPickedUpFirefly = 5f;
    public const float decreaseOfHeartbeatInOneSecond = heartbeatDecreasePerPickedUpFirefly / lengthOfDecreasingHeartbeatAfterPickedUpFirefly;
    public const float maxHeartbeatForRunning = 80f;
    public const float normalSpeed = 1f;
    public const float runningSpeed = 2f;
    public const float normalHeartbeatIncrease = 1f;
    public const float runningHeartbeatIncrease = 4f;
    public const float pickedUpFireflyCountdownSet = 1.5f;
    float pickedUpFireflyCountdown = 0;
    public const string enemyTag = "enemy";

    private float continuoslyDecreaseHeartbeat;

	public SpriteRenderer sprite;

    public Player instance;

    public RuntimeAnimatorController runAnim;
    public RuntimeAnimatorController idleAnim;
    public RuntimeAnimatorController walkAnim;
    public RuntimeAnimatorController glowAnim;

    Animator animator;

    void Start()
    {
        lightVicinity = 15f;
        instance = this;
        animator = this.gameObject.GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        var horizontalMovementRaw = Input.GetAxisRaw("Horizontal");
        var vericalMovementRaw = Input.GetAxisRaw("Vertical");

        // Player recently picked up firefly - play glow animation and stay in one place
        if (pickedUpFireflyCountdown > 0)
        {
            animator.runtimeAnimatorController = glowAnim;
            pickedUpFireflyCountdown -= Time.deltaTime;
            rb2D.velocity = new Vector2(0f, 0f);
            return;
        }

        // Is running
        if (Input.GetKey(KeyCode.LeftShift) && heartbeat < maxHeartbeatForRunning)
        {
            continuoslyDecreaseHeartbeat = 0f;   // heartbeat decrease reseted after start of running
            speed = 2f;           
        }
        else
        {
            speed = 1f;
        }

        // Movement
        rb2D.velocity = new Vector2(horizontalMovementRaw, vericalMovementRaw) * speed;
		if (horizontalMovementRaw != 0)
			sprite.flipX = (horizontalMovementRaw < 0);     // rotation to direction of movement

        //Selection of animation
        if (horizontalMovementRaw == 0 && vericalMovementRaw == 0)
        {
            animator.runtimeAnimatorController = idleAnim;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                //animator.runtimeAnimatorController = runAnim;
            }
            else
            {
                animator.runtimeAnimatorController = walkAnim;
            }           
        }

        
        // Clipping of heartbeat value
        if (heartbeat < 0f)
        {
            heartbeat = 0f;
        }
        if (heartbeat > 100f)
        {
            gameManager.GameOver();
        }
       
        // Decrease of heartbeat after picking up firefly
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
        pickedUpFireflyCountdown = pickedUpFireflyCountdownSet;

        instance.continuoslyDecreaseHeartbeat = heartbeatDecreasePerPickedUpFirefly;            
    }

    public void DestroyEnemiesInsideLightVicinity()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag(enemyTag))
        {
            if (Vector2.Distance(instance.transform.localPosition, enemy.transform.localPosition) < lightVicinity)
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
			fireflyManager.OnFireflyDestroyed (col.gameObject.GetComponent<Firefly>());
            Destroy(col.gameObject);
        }
    }
}
