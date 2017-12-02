using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSpawn : MonoBehaviour
{
	public float distance;
	public GameObject background;    

	static int instanceNo = 0;

	// Use this for initialization
	void Start () {

	}


	void Update () {

	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if ((distance > 0 && other.gameObject.tag == "background_trigger_r") ||
			(distance < 0 && other.gameObject.tag == "background_trigger_l")) {
			instanceNo++;
			GameObject newObj = Instantiate (background, new Vector3 (background.transform.position.x + distance, background.transform.position.y, 1f), transform.localRotation);
			Background newBackground = newObj.GetComponent<Background>();
			newBackground.gameObject.name = "Background" + instanceNo.ToString ();
			if (distance > 0) {
				newBackground.SetTriggerEnable (Background.TriggerType.LEFT, false);
			} else {
				newBackground.SetTriggerEnable (Background.TriggerType.RIGHT, false);
			}
			newBackground.transform.parent = background.transform.parent;
			this.gameObject.SetActive(false);
		}
	}
}
