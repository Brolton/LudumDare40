using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public Player player;
	public int MoveSpeed = 4;
	public int AttackMoveSpeed = 6;
	public float DistToDie = 25;
	public float MinDist = 1;
	public float DistToAttack = 20;

	Vector3 rndDirection;
	int rndMovesCount = 0;

	public int MaxRandomMovingFrames = 100;
	public int MinRandomMovingFrames = 20;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null)
			return;
		
		if (Vector3.Distance (player.transform.position, transform.position) > DistToDie) {
			Destroy (this.gameObject);
			EnemyManager.Instance.OnEnemyDestroyed (this);
		} 
		else if (Vector3.Distance (player.transform.position, transform.position) > DistToAttack) {
			if (rndMovesCount == 0) {
				rndMovesCount = Random.Range (MinRandomMovingFrames, MaxRandomMovingFrames);
				rndDirection = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
				rndDirection.Normalize ();
			}
			rndMovesCount -= 1;
			transform.position += rndDirection * MoveSpeed * Time.deltaTime;
		}
		else if (Vector3.Distance(player.transform.position, transform.position) > MinDist)
		{
			Vector3 dir = player.transform.position - transform.position;
			dir.Normalize();
			transform.position += dir * AttackMoveSpeed * Time.deltaTime;

			rndDirection = dir;
			rndMovesCount = 30;
		}
	}

	public void SetTarget(Player target)
	{
		player = target;
	}
}
