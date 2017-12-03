using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassPanel : MonoBehaviour 
{
	public Player player;
	public FireflyManager fireflyManager;

	public Image arrow;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Firefly nearest = fireflyManager.GetNearest();

		bool exist = (nearest != null);
		arrow.gameObject.SetActive (exist);

		if (!exist)
			return;

		Vector3 dir = nearest.transform.position - player.transform.position;

		float _r = Mathf.Atan2(dir.y, dir.x);
		float _d = (_r / Mathf.PI) * 180;

		arrow.transform.eulerAngles = new Vector3(0, 0, _d);
	}
}
