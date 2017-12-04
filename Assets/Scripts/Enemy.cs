using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public Player player;
    public EnemyManager enemyManager;
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

    Animator animator;
    public RuntimeAnimatorController idleAnim;
    public RuntimeAnimatorController recoilAnim;

    public int PlayerOrderInLayer = 10;

    float timeToDeath = 0f;


    void Start()
    {
        animator = this.gameObject.GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (timeToDeath > 0f)
        {
            animator.runtimeAnimatorController = recoilAnim;
            timeToDeath -= Time.deltaTime;

            if (timeToDeath <= 0f)
            {
                ActualDeath();
            }
        }
        else
        {
            animator.runtimeAnimatorController = idleAnim;
        }

        if (player == null)
            return;

		float distanceToPlayer = Vector3.Distance (player.transform.position, transform.position);

		if (distanceToPlayer > DistToDie)
        { 
			Kill();
        }
		else if (distanceToPlayer > DistToAttack)
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
		else if (distanceToPlayer > MinDist)
        {
            Vector3 dir = player.transform.position - transform.position;
            dir.Normalize();
            transform.position += dir * AttackMoveSpeed * Time.deltaTime;

            if (dir.x != 0)
                sprite.flipX = (dir.x > 0);

            rndDirection = dir;
            rndMovesCount = 30;
        }
    }

    public void SetTarget(Player target)
    {
        player = target;
    }

	public void Kill()
	{     
        timeToDeath = 1f;
    }

    public void ActualDeath()
    {
        EnemyManager.instance.OnEnemyDestroyed(this);
        Destroy(this.gameObject);
    }
}
