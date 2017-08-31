using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	private Vector3 initialPosition;
	private Quaternion initialRotation;

	private Rigidbody rigidBody;
	private AudioSource audioSource;
	private bool inPlay;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.useGravity = false;

		inPlay = false;

		initialPosition = transform.position;
		initialRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Launch(Vector3 velocity)
	{
		inPlay = true;
		rigidBody.useGravity = true;
		rigidBody.velocity = velocity;
		audioSource.Play ();
	}

	public void Reset () {
		inPlay = false;

		transform.SetPositionAndRotation (initialPosition, initialRotation);
		rigidBody.useGravity = false;
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
	}
}
