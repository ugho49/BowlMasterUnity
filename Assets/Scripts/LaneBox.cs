using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBox : MonoBehaviour {

	private PinSetter pinSetter;

	// Use this for initialization
	void Start () {
		pinSetter = FindObjectOfType<PinSetter> ();
	}
	
	// Update is called once per frame
	void OnTriggerExit (Collider collider) {
		if (collider.gameObject.GetComponent<Ball> ()) {
			pinSetter.SetBallOutOfPlay (true);
		}
	}
}
