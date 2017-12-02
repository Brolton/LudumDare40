using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawn : MonoBehaviour
{
	public float distance;
	public GameObject background;    

	// Use this for initialization
	void Start () {

	}


	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (distance > 0 && other.gameObject.tag == "background_trigger_r") {
			GameObject newObj = Instantiate (background, new Vector3 (background.transform.position.x + distance, background.transform.position.y, 1f), transform.localRotation);
			Background newBackground = newObj.GetComponent<Background>();
			newBackground.SetTriggerEnable (Background.TriggerType.LEFT, false);
			//Destroy (background, 10f);
			this.gameObject.SetActive(false);
		}
		else if (distance < 0 && other.gameObject.tag == "background_trigger_l") {
			GameObject newObj = Instantiate (background, new Vector3 (background.transform.position.x + distance, background.transform.position.y, 1f), transform.localRotation);
			Background newBackground = newObj.GetComponent<Background>();
			newBackground.SetTriggerEnable (Background.TriggerType.RIGHT, false);
			//Destroy (background, 10f);
			this.gameObject.SetActive(false);
		}
	}
}
