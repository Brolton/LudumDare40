using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireflyManager : MonoBehaviour
{
    public Firefly firefly;
    public Player player;
    public World world;

    public const float spawnTime = 7f;    // Spawn time in seconds - NEEDS TO BE ADJUSTED
    public float timeOfFirstSpawn = 0f;
    public const float offsetFromBorders = 5f;

    public float XSizeOfPlayArea;
    public float YSizeOfPlayArea;

    bool fireflyExists = false;

	List<Firefly> fireflies = new List<Firefly>();

    void Start ()
    {
		InvokeRepeating ("Spawn", timeOfFirstSpawn, 1f);
    }

    public void Spawn()
    {
        if (fireflyExists)
        {
            return;
        }

        float randomXOffset = Random.Range(player.lightVicinity, 10);
        bool randomBool = (Random.value < 0.5);
        if (randomBool)
            randomXOffset *= -1;
        float posX = player.transform.position.x + randomXOffset;

        float minPosY = world.groundMinY;
        float maxPosY = world.groundMaxY;
        float posY = Random.Range(minPosY, maxPosY);

        Vector3 spawnPosition = new Vector3(posX, posY, 0);

        // Makes sure that firefly is not spawned inside lit area
        if (Vector3.Distance(spawnPosition, player.transform.position) < player.lightVicinity)
        {
            Spawn();
        }

		Firefly newFirefly = Instantiate(firefly, spawnPosition, Quaternion.identity);
		newFirefly.fireflyManager = this;
		newFirefly.transform.parent = world.transform;
		fireflies.Add(newFirefly);
		world.AddObject (newFirefly.gameObject);

        fireflyExists = true;
    }

	public void OnFireflyDestroyed(Firefly firefly)
	{
		fireflies.Remove(firefly);
		world.RemoveObject(firefly.gameObject);
        fireflyExists = false;
        Spawn();
	}

	public Firefly GetNearest()
	{
		if (fireflies.Count == 0)
			return null;

		//fireflies.Sort((obj1, obj2) => obj1.transform.position.y.CompareTo(obj2.transform.position.y));

		fireflies.Sort(delegate(Firefly obj1, Firefly obj2)
		{
				float dist1 = Vector3.Distance(obj1.transform.position, player.transform.position);
				float dist2 = Vector3.Distance(obj2.transform.position, player.transform.position);
				return dist1.CompareTo(dist2);
		});

		return fireflies[0];
	}
}
