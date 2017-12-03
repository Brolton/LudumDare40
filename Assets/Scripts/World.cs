using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
	public float groundMinY = 0;
	public float groundMaxY = 10;

	public Player player;

	List<GameObject> allObjects = new List<GameObject> ();

	void Start ()
    {
		AddObject (player.gameObject);
	}
	
	void Update ()
    {
		allObjects.Sort((obj1, obj2)=>obj1.transform.position.y.CompareTo(obj2.transform.position.y) );
		int order = allObjects.Count;
		foreach(GameObject obj in allObjects) {
			order--;
			if (obj.tag == "Player") {
				Player objSprite = obj.GetComponent<Player>();
				objSprite.sprite.sortingOrder = order;
			}
			else if (obj.tag == "enemy") {
				Enemy objSprite = obj.GetComponent<Enemy>();
				objSprite.sprite.sortingOrder = order;
			}
			else if (obj.tag == "firefly") {
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
