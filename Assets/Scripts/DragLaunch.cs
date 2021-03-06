﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (Ball))]
public class DragLaunch : MonoBehaviour {

	private Ball ball;

	private bool ballIsLaunch;
	private Vector3 dragStart, dragEnd;
	private float startTime, endTime;

	// Use this for initialization
	void Start () {
		ball = GetComponent<Ball> ();
		ballIsLaunch = false;
	}

	public void MoveStart(float amount) {
		if (ballIsLaunch) {
			return;
		}

		Vector3 newPosition = ball.transform.position + new Vector3 (amount, 0, 0);
		newPosition.x = Mathf.Clamp (newPosition.x, -40, 40);

		ball.transform.position = newPosition;

	}

	// Capture time and position of drag start
	public void DragStart() {

		if (ballIsLaunch) {
			return;
		}

		dragStart = Input.mousePosition;
		startTime = Time.time;
	}

	// Launch the ball
	public void DragEnd() {

		if (ballIsLaunch) {
			return;
		}

		ballIsLaunch = true;

		dragEnd = Input.mousePosition;
		endTime = Time.time;

		float dragDuration = endTime - startTime;
		float launchSpeedX = (dragEnd.x - dragStart.x) / dragDuration;
		float launchSpeedZ = (dragEnd.y - dragStart.y) / dragDuration;

		Vector3 launchVelocity = new Vector3 (launchSpeedX, 0, launchSpeedZ);
		ball.Launch (launchVelocity);
	}

	public void SetBallIsReset() {
		ballIsLaunch = false;
	}
}
