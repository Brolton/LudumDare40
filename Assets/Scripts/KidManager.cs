using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KidManager : MonoBehaviour
{
    public GameObject kid;
    public float spawnTime = 7f;    // Spawn time in seconds - NEEDS TO BE ADJUSTED
    public float timeOfFirstSpawn = 0;
    public float offsetFromBorders = 5;

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

        Instantiate(kid, spawnPosition, Quaternion.identity);
    }
}
