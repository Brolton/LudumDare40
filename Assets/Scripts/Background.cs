using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {
	public enum TriggerType {
		LEFT,
		RIGHT
	}

	public GameObject leftTrigger;
	public GameObject rightTrigger;
	public GameObject player;

	public int background_width;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Mathf.Abs (leftTrigger.transform.position.x - player.transform.position.x) > background_width/2) {
			SetTriggerEnable (TriggerType.LEFT, true);
		}
		if (Mathf.Abs (rightTrigger.transform.position.x - player.transform.position.x) > background_width/2) {
			SetTriggerEnable (TriggerType.RIGHT, true);
		}
		if (Mathf.Abs (transform.position.x - player.transform.position.x) > background_width) {
			Destroy (this.gameObject);
		}
		if (Mathf.Abs (transform.position.x - player.transform.position.x) > background_width) {
			Destroy (this.gameObject);
		}
	}

	public void SetTriggerEnable(TriggerType trigType, bool enable)
	{
		if (trigType == TriggerType.LEFT) {
			leftTrigger.SetActive (enable);
		} else {
			rightTrigger.SetActive (enable);
		}
	}
}
