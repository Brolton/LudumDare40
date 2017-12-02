using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirelfyManager : MonoBehaviour
{
    public Firefly firefly;
    public Player player;
    public const float spawnTime = 7f;    // Spawn time in seconds - NEEDS TO BE ADJUSTED
    public const float timeOfFirstSpawn = 0;
    public const float offsetFromBorders = 5;

    public float XSizeOfPlayArea;
    public float ZSizeOfPlayArea;

    void Start ()
    {
        InvokeRepeating("Spawn", timeOfFirstSpawn, spawnTime);
	}


    void Spawn()
    {
        float randomXCoordinate = Random.Range(offsetFromBorders, XSizeOfPlayArea - offsetFromBorders);
        float randomZCoordinate = Random.Range(offsetFromBorders, ZSizeOfPlayArea - offsetFromBorders);

        Vector3 spawnPosition = new Vector3(randomXCoordinate, 0, randomZCoordinate);

        // Makes sure that firefly is not spawned inside lit area
        while (Vector3.Distance(spawnPosition, player.transform.position) < player.lightVicinity)
        {
            randomXCoordinate = Random.Range(offsetFromBorders, XSizeOfPlayArea - offsetFromBorders);
            randomZCoordinate = Random.Range(offsetFromBorders, ZSizeOfPlayArea - offsetFromBorders);
            spawnPosition = new Vector3(randomXCoordinate, 0, randomZCoordinate);
        }

        Instantiate(firefly, spawnPosition, Quaternion.identity);
    }
}
