using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
	public float groundMinY = 0f;
	public float groundMaxY = 10f;

    public const int numberOfObstacles = 7;

	public Player player;

    public GameObject obstaclePrefab;

	List<GameObject> allObjects = new List<GameObject> ();

	void Start ()
    {
		AddObject (player.gameObject);
        ObstacleGenerator();
	}

    void ObstacleGenerator()
    {
        List<Vector3> obstacles = new List<Vector3>();

        bool goodDistance = false;

        for (int i = 0; i < numberOfObstacles; i++)
        {
            Vector3 spawnPosition;

            do
            {
                float randomXOffset = Random.Range(10, 20);
                bool randomBool = (Random.value < 0.5);
                if (randomBool) randomXOffset *= -1;
                float posX = player.transform.position.x + randomXOffset;

                float minPosY = groundMinY;
                float maxPosY = groundMaxY;
                float posY = Random.Range(minPosY, maxPosY);

                spawnPosition = new Vector3(posX, posY, 0);

                goodDistance = true;

                // Makes sure obstacles are not too close to each other
                for (int j = 0; j < obstacles.Count; j++)
                {
                    if (Vector3.Distance(obstacles[j], spawnPosition) < 5f)
                    {
                        goodDistance = false;
                        break;
                    }
                }               
            } while (!goodDistance);

            obstacles.Add(spawnPosition);

            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
        }
    }

    void Update ()
    {
        allObjects.Sort((obj1, obj2) => obj1.transform.position.y.CompareTo(obj2.transform.position.y));
        int order = allObjects.Count;
        foreach (GameObject obj in allObjects)
        {
            order--;
            if (obj.tag == "Player")
            {
                Player objSprite = obj.GetComponent<Player>();
                objSprite.sprite.sortingOrder = order;
            }
            else if (obj.tag == "enemy")
            {
                Enemy objSprite = obj.GetComponent<Enemy>();
                objSprite.sprite.sortingOrder = order;
            }
            else if (obj.tag == "firefly")
            {
                Firefly objSprite = obj.GetComponent<Firefly>();
                objSprite.sprite.sortingOrder = order;
            }
        }
    }

    public void AddObject(GameObject obj)
	{
		allObjects.Add (obj);
	}

	public void RemoveObject(GameObject obj)
	{
		allObjects.Remove (obj);
	}
}
