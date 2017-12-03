using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    public Player player;
    public FireflyManager fireflyManager;

	public SpriteRenderer sprite;

    void Start()
    {
        
    }

    void Update()
    {

    }   

    void OnDestroy()
    {
		fireflyManager.OnFireflyDestroyed (this);
        // Effect ?
    }   
}
