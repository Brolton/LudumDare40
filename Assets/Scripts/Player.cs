using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = normalSpeed;
    public Rigidbody2D rb2D;
    public float heartbeat = 0f;     // value between 0 and 100
    public float lightVicinity = 3f;     // TO BE CHANGED

    public float scrollSensitivity = 1.3f;

    public float lanternDtSizeSensitivity = 2f;

    public GameManager gameManager;

    public FireflyManager fireflyManager;

    public ApplyTorchEffect torchLightEffect;

    public GameObject flashEffect;


    public const float heartbeatDecreasePerPickedUpFirefly = 20f;
    public const float lengthOfDecreasingHeartbeatAfterPickedUpFirefly = 5f;
    public const float decreaseOfHeartbeatInOneSecond = heartbeatDecreasePerPickedUpFirefly / lengthOfDecreasingHeartbeatAfterPickedUpFirefly;
    public const float maxHeartbeatForRunning = 80f;
    public const float normalSpeed = 1f;
    public const float runningSpeed = 3f;
    public const float normalHeartbeatIncrease = 3f;
    public const float runningHeartbeatIncrease = 7f;
    public const float pickedUpFireflyCountdownSet = 1.5f;
    float pickedUpFireflyCountdown = 0;
    public const string enemyTag = "enemy";
    public const float killEnemyDistance = 10f;

    private float continuoslyDecreaseHeartbeat;

	public SpriteRenderer sprite;

    public Player instance;

    public RuntimeAnimatorController runAnim;
    public RuntimeAnimatorController idleAnim;
    public RuntimeAnimatorController walkAnim;
    public RuntimeAnimatorController glowAnim;
    public RuntimeAnimatorController dieAnim;

    Animator animator;

	public HeartPanel Heart;

    public bool death = false;

	//Wwise variables
	bool isMoving = false;											// Flag to trigger STOP_footsteps

    void Start()
    {
        //lightVicinity = 15f;
        instance = this;
        animator = this.gameObject.GetComponentInChildren<Animator>();

		//Wwise globals, maybe needs to be moved to a general place
		AkSoundEngine.SetRTPCValue ("RTCP_Movement", 0);				// Wwise set movement to Walk
		AkSoundEngine.SetRTPCValue ("RTCP_HeartBeat", 0);				// Wwise set movement to Walk
		AkSoundEngine.PostEvent("PLAY_heartbeat", gameObject);			// Wwise play heartbeat sound

	}

    void FixedUpdate()
    {
        var horizontalMovementRaw = Input.GetAxisRaw("Horizontal");
        var vericalMovementRaw = Input.GetAxisRaw("Vertical");
								

        if (death)
        {
            if (torchLightEffect.radius != 0)   // Lantern fade to black
            {
                torchLightEffect.radius -= 2f * Time.deltaTime;
                rb2D.velocity = new Vector2(0f, 0f);
                return;
            }
        }

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
            continuoslyDecreaseHeartbeat = 0f;   			// heartbeat decrease reseted after start of running
            speed = 2f;

			//Wwise set footsteps to RUN
			//AkSoundEngine.SetSwitch ("Movement", "Run", gameObject);	// set footsteps to RUN
			AkSoundEngine.SetRTPCValue("RTPC_Movement", 1.0f);

			Debug.Log ("run");
        }
        else
        {
            speed = 1f;

			// Wwise set footsteps to WALK
			//AkSoundEngine.SetSwitch ("Movement", "Walk", gameObject);	
			AkSoundEngine.SetRTPCValue("RTPC_Movement", 0.0f);
        
			Debug.Log ("walk");
		}

        // Movement
        rb2D.velocity = new Vector2(horizontalMovementRaw, vericalMovementRaw).normalized * speed;
		//Debug.Log (rb2D.velocity);
		if (horizontalMovementRaw != 0)
			sprite.flipX = (horizontalMovementRaw < 0);     // rotation to direction of movement
		// Movement -> Wwise
		if ((horizontalMovementRaw != 0 || vericalMovementRaw != 0) && !isMoving) 
		{
			AkSoundEngine.PostEvent ("PLAY_footsteps", gameObject);
			isMoving = true;
		} 
		else if (horizontalMovementRaw == 0 && vericalMovementRaw == 0 && isMoving)
		{
			AkSoundEngine.PostEvent ("STOP_footsteps", gameObject);
			isMoving = false;
		}

        //Selection of animation
        if (horizontalMovementRaw == 0 && vericalMovementRaw == 0)
        {
            animator.runtimeAnimatorController = idleAnim;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift) && heartbeat < maxHeartbeatForRunning)
            {
                animator.runtimeAnimatorController = runAnim;
            }
            else
            {
                animator.runtimeAnimatorController = walkAnim;
            }           
        }

        // Lanter
        float mouseWheel = -Input.GetAxis("Mouse ScrollWheel") * scrollSensitivity;
        float laternDtSize = Input.GetAxisRaw("Lantern Size") * lanternDtSizeSensitivity * Time.fixedDeltaTime;

        // prevent from pressing both the mouseWheel and U/I to double the speed
        lightVicinity += Mathf.Clamp(mouseWheel + laternDtSize, Mathf.Min(mouseWheel, laternDtSize), Mathf.Max(mouseWheel, laternDtSize));
        lightVicinity = Mathf.Clamp(lightVicinity, 1f, 5f);

        // calculates how many screen pixels one unit is wide
        float screenPixelsPerUnit = (torchLightEffect.cam.WorldToScreenPoint(Vector3.one) -
            torchLightEffect.cam.WorldToScreenPoint(Vector3.zero)).magnitude;

        // make the light actual radius match lightVicinity(which is in units)
        torchLightEffect.radius = (screenPixelsPerUnit * lightVicinity) / // divides through the rough width of the light
            (torchLightEffect.flashLightRadius[0] * torchLightEffect.flashLightSkew[0].x);


        // Clipping of heartbeat value
        if (heartbeat <= 0f)
        {
            heartbeat = 0f;
        }
        if (heartbeat > 100f && !death)   // Main character dies
        {
            rb2D.velocity = new Vector2(0f, 0f);    // Restricts movement

            animator.runtimeAnimatorController = dieAnim;

            death = true;

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
            if (Input.GetKey(KeyCode.LeftShift) && (horizontalMovementRaw != 0 || vericalMovementRaw != 0) && (heartbeat < maxHeartbeatForRunning))
            {
                heartbeat += runningHeartbeatIncrease * Time.deltaTime * (NumberOfEnemiesNearby() + 1);
            }
            else
            {
                heartbeat += normalHeartbeatIncrease * Time.deltaTime * NumberOfEnemiesNearby();
            }           
        }

		Heart.SetHeartbeat (heartbeat);

		//Wwise
		AkSoundEngine.SetRTPCValue("RTPC_HeartBeat", heartbeat);					// Wwise set RTPC value Heartbeat
    }

    public int NumberOfEnemiesNearby()
    {
        int returnValue = 0;

        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag(enemyTag))
        {
            if (Vector2.Distance(instance.transform.localPosition, enemy.transform.localPosition) < lightVicinity)
            {
                returnValue++;
            }
        }

        return returnValue;
    }

    public void FireflyPickedUp()
    {
        Instantiate(flashEffect, instance.transform.position, Quaternion.identity);

        DestroyEnemiesInsideLightVicinity();
        pickedUpFireflyCountdown = pickedUpFireflyCountdownSet;

        instance.continuoslyDecreaseHeartbeat = heartbeatDecreasePerPickedUpFirefly;         
        gameManager.PickUpFirefly();   

		AkSoundEngine.PostEvent ("PLAY_pickupFirefly", gameObject);						// Wwise play pickupFirefly sound
    }

    public void DestroyEnemiesInsideLightVicinity()
    {
        foreach (GameObject enemy in GameObject.FindGameObjectsWithTag(enemyTag))
        {
            if (Vector2.Distance(instance.transform.localPosition, enemy.transform.localPosition) < killEnemyDistance)
            {               
				enemy.GetComponent<Enemy>().Kill();

				// Wwise
				AkSoundEngine.PostEvent("STOP_monster", enemy);							// Wwise stop single monster_loop
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "firefly")
        {
            FireflyPickedUp();
			col.gameObject.GetComponent<Firefly>().Kill();
        }
    }
}
