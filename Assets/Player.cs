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
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        rb2D.velocity = new Vector2(h, v) * speed;
        /*if (h != 0 || v != 0)
        {
            rb2D.MovePosition((new Vector2(h, v) * speed * Time.fixedDeltaTime) + (Vector2)transform.position);
        }
        else
        {
            rb2D.velocity = Vector2.zero;
        }*/
    }
}
