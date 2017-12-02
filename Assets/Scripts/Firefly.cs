using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    public Player player;
    public FireflyManager fireflyManager;


    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("COLLISION");

        if (col.gameObject == player)
        {
            player.FireflyPickedUp();           
            Destroy(this);
        }
    }

    void OnDestroy()
    {
        // Effect ?
        fireflyManager.Spawn();
    }
}
