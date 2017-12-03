using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public World world;
    public GameObject enemyPrefab;

    public float spawnTime = 3f;
    public float timeOfFirstSpawn = 0f;

    public int MaxEnemiesCount = 3;
    public int MinOffsetFromPlayer = 10;
    public int MaxOffsetFromPlayer = 10;

    List<Enemy> enemies = new List<Enemy>();
    int enemyNo = 0;

    static public EnemyManager instance;

    void Start()
    {
		instance = this;
        InvokeRepeating("Spawn", timeOfFirstSpawn, spawnTime);
    }

    void Update()
    {

    }

    void Spawn()
    {
        //Debug.Log(enemies.Count);

        if (enemies.Count >= MaxEnemiesCount)
            return;
        
        enemyNo++;

        float randomXOffset = Random.Range(MinOffsetFromPlayer, MaxOffsetFromPlayer);
        bool randomBool = (Random.value < 0.5);
        if (randomBool) randomXOffset *= -1;
        float posX = world.player.transform.position.x + randomXOffset;

        float minPosY = world.groundMinY;
        float maxPosY = world.groundMaxY;
        float posY = Random.Range(minPosY, maxPosY);

        Vector3 spawnPosition = new Vector3(posX, posY, 0);

        Enemy newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity).GetComponent<Enemy>();
        newEnemy.SetTarget(world.player);
        newEnemy.gameObject.name = "Enemy" + enemyNo.ToString();
        newEnemy.transform.parent = world.transform;

        enemies.Add(newEnemy);
		world.AddObject(newEnemy.gameObject);
    }

    public void OnEnemyDestroyed(Enemy enemy)
    {
        enemies.Remove(enemy);
		//world.RemoveObject(enemy.gameObject);
        //Spawn();
    }
}
