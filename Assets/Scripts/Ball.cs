using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	private Vector3 initialPosition;
	private Quaternion initialRotation;

	private Rigidbody rigidBody;
	private AudioSource audioSource;
	private DragLaunch dragLaunch;

	// Use this for initialization
	void Start () {
		audioSource = GetComponent<AudioSource> ();
		dragLaunch = GetComponent<DragLaunch> ();
		rigidBody = GetComponent<Rigidbody> ();
		rigidBody.useGravity = false;

		initialPosition = transform.position;
		initialRotation = transform.rotation;
	}

	public void Launch(Vector3 velocity)
	{
		rigidBody.useGravity = true;
		rigidBody.velocity = velocity;
		audioSource.Play ();
	}

	public void Reset () {
		transform.SetPositionAndRotation (initialPosition, initialRotation);
		dragLaunch.SetBallIsReset ();
		rigidBody.useGravity = false;
		rigidBody.velocity = Vector3.zero;
		rigidBody.angularVelocity = Vector3.zero;
	}
}
