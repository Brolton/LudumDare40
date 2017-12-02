using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firefly : MonoBehaviour
{
    public Player player;


    void Start()
    {
        
    }

    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject == player)
        {
            player.FireflyPickedUp();
            // Effect ?
            Destroy(this);
        }
    }
}
