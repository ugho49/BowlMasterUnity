using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinCounter : MonoBehaviour {

	public Text standingDisplay;
	public float settleTime = 2f;

	private bool ballOutOfPlay;
	private float lastChangeTime;
	private int lastSettledCount = 10;
	private ActionManager actionManager;
	private int lastStandingCount = -1;
	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.FindObjectOfType<GameManager> ();
		actionManager = new ActionManager ();
		ballOutOfPlay = false;
	}
	
	// Update is called once per frame
	void Update () {
		standingDisplay.text = CountStanding ().ToString ();

		if (ballOutOfPlay) {
			standingDisplay.color = Color.red;
			UpdateStandingCountAndSettle ();
		}
	}

	public void Reset() {
		lastSettledCount = 10;
	}

	void UpdateStandingCountAndSettle() {
		int currentStanding = CountStanding();

		if (currentStanding != lastStandingCount) {
			lastChangeTime = Time.time;
			lastStandingCount = currentStanding;
			return;
		}

		if ((Time.time - lastChangeTime) > settleTime) {
			PinsHaveSettled ();
		}
	}

	int CountStanding() {
		int standing = 0;

		foreach(Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			if (pin.IsStanding ()) {
				standing++;
			}
		}

		return standing;
	}
		
	void PinsHaveSettled() {
		int standing = CountStanding ();
		int pinFall = lastSettledCount - standing;
		lastSettledCount = standing;

		gameManager.Bowl (pinFall);

		lastStandingCount = -1;
		ballOutOfPlay = false;
		standingDisplay.color = Color.green;
	}

	void OnTriggerExit (Collider collider) {
		if (collider.gameObject.GetComponent<Ball> ()) {
			ballOutOfPlay = true;
		}
	}
}
