using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

	public Text standingDisplay;
	public GameObject pinSet;

	private bool ballOutOfPlay;
	private Ball ball;
	private float lastChangeTime;
	private int lastSettledCount = 10;
	private ScoreMaster scoreMaster;
	private Animator animator;
	private int lastStandingCount = -1;

	// Use this for initialization
	void Start () {
		scoreMaster = new ScoreMaster ();
		ball = GameObject.FindObjectOfType<Ball> ();
		animator = GetComponent<Animator> ();
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

	public void RaisePins() {
		foreach(Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			pin.Raise ();
		}
	}

	public void LowerPins() {
		foreach(Pin pin in GameObject.FindObjectsOfType<Pin>()) {
			pin.Lower ();
		}
	}

	public void RenewPins() {
		GameObject obj = Instantiate (pinSet);
		obj.transform.position += new Vector3 (0, 40, 0);
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

	void UpdateStandingCountAndSettle() {
		int currentStanding = CountStanding();

		if (currentStanding != lastStandingCount) {
			lastChangeTime = Time.time;
			lastStandingCount = currentStanding;
			return;
		}

		float settleTime = 2f;

		if ((Time.time - lastChangeTime) > settleTime) {
			PinsHaveSettled ();
		}
	}

	void PinsHaveSettled() {
		int standing = CountStanding ();
		int pinFall = lastSettledCount - standing;
		lastSettledCount = standing;

		ScoreMaster.Action action = scoreMaster.Bowl (pinFall);
		Debug.Log (action);

		if (ScoreMaster.Action.Tidy == action) {
			animator.SetTrigger ("Tidy");
		} else if (ScoreMaster.Action.EndTurn == action || ScoreMaster.Action.Reset == action) {
			lastSettledCount = 10;
			animator.SetTrigger ("Reset");
		} else if (ScoreMaster.Action.EndGame == action) {
			animator.SetTrigger ("Reset");
		}

		ball.Reset ();
		lastStandingCount = -1;
		ballOutOfPlay = false;
		standingDisplay.color = Color.green;
	}

	void OnTriggerExit(Collider collider) {
		if (collider.GetComponent<Pin>()) {
			Destroy (collider.gameObject);
		}
	}

	public void SetBallOutOfPlay(bool outOfPlay) {
		ballOutOfPlay = outOfPlay;
	}
}
