using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gutters : MonoBehaviour {

	private List<GameObject> gutters;
	private bool active = false;

	// Use this for initialization
	void Start () {
		gutters = new List<GameObject> ();

		foreach (Transform child in transform)
		{
			gutters.Add (child.gameObject);
		}
	}

	public void ToggleGutters() {
		active = !active;
		foreach (GameObject obj in gutters) {
			obj.GetComponent<BoxCollider> ().isTrigger = !active;
		}
	}
}
