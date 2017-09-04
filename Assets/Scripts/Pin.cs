﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour {

	public float standingThreshold = 3f;
	public float distanceToRaise = 40f;

	private Rigidbody rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public bool IsStanding() 
	{
		Vector3 rotationInEuler = transform.rotation.eulerAngles;

		float tiltInX = Mathf.Abs(270 - rotationInEuler.x);
		float tiltInZ = Mathf.Abs(rotationInEuler.z);

		if (tiltInX < standingThreshold && tiltInZ < standingThreshold) {
			return true;
		}

		return false;
	}

	public void Raise() {
		if (IsStanding ()) {
			rigidBody.useGravity = false;
			transform.Translate (new Vector3(0, distanceToRaise, 0), Space.World);
			transform.rotation = Quaternion.Euler (270f, 0f, 0f);
		}
	}

	public void Lower() {
		transform.Translate (new Vector3(0, -distanceToRaise, 0), Space.World);
		rigidBody.useGravity = true;
	}
}
