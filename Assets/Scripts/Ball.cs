using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

	public Vector3 launchVelocity;

	private Rigidbody rigidBody;
	private AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource> ();
		rigidBody = GetComponent<Rigidbody> ();
	}

	// Use this for initialization
	void Start () {
		Launch ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Launch()
	{
		rigidBody.velocity = launchVelocity;
		audioSource.Play ();
	}
}
