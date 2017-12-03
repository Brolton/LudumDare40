using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Player player;
    public float MoveSpeed = 4f;
    public float AttackMoveSpeed = 6f;
    public float DistToDie = 25f;
    public float MinDist = 1f;
    public float DistToAttack = 20f;

    Vector3 rndDirection;
    int rndMovesCount = 0;

    public int MaxRandomMovingFrames = 100;
    public int MinRandomMovingFrames = 20;

    public SpriteRenderer sprite;
    public int PlayerOrderInLayer = 10;

    void Start()
    {

    }

    void Update()
    {
        if (player == null)
            return;

        if (Vector3.Distance(player.transform.position, transform.position) > DistToDie)
        {
            EnemyManager.instance.OnEnemyDestroyed(this);                    
        }
        else if (Vector3.Distance(player.transform.position, transform.position) > DistToAttack)
        {
            if (rndMovesCount == 0)
            {
                rndMovesCount = Random.Range(MinRandomMovingFrames, MaxRandomMovingFrames);
                rndDirection = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
                rndDirection.Normalize();
            }
            rndMovesCount -= 1;
            transform.position += rndDirection * MoveSpeed * Time.deltaTime;

            if (rndDirection.x != 0)
                sprite.flipX = (rndDirection.x > 0);
        }
        else if (Vector3.Distance(player.transform.position, transform.position) > MinDist)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.Normalize();
            transform.position += dir * AttackMoveSpeed * Time.deltaTime;

            if (dir.x != 0)
                sprite.flipX = (dir.x > 0);

            rndDirection = dir;
            rndMovesCount = 30;
        }

        if (transform.position.y > player.transform.position.y)
        {
            sprite.sortingOrder = PlayerOrderInLayer - (int)(Mathf.Abs(player.transform.position.y - transform.position.y) / 0.1f);
        }
        else
        {
            sprite.sortingOrder = PlayerOrderInLayer + (int)((player.transform.position.y - transform.position.y) / 0.1f);
        }
    }

    public void SetTarget(Player target)
    {
        player = target;
    }
}
