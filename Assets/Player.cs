using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody2D rb2D;

    void Start()
    {

    }

    void FixedUpdate()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");

        rb2D.velocity = new Vector2(h, v) * speed;
    }
}
