using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

	public Text standingDisplay;
	public GameObject pinSet;
	public float settleTime = 2f;

	private bool ballOutOfPlay;
	private Ball ball;
	private float lastChangeTime;
	private int lastSettledCount = 10;
	private ActionManager actionManager;
	private Animator animator;
	private int lastStandingCount = -1;

	// Use this for initialization
	void Start () {
		actionManager = new ActionManager ();
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
			pin.transform.rotation = Quaternion.Euler (270f, 0f, 0f);
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
			
		if ((Time.time - lastChangeTime) > settleTime) {
			PinsHaveSettled ();
		}
	}

	void PinsHaveSettled() {
		int standing = CountStanding ();
		int pinFall = lastSettledCount - standing;
		lastSettledCount = standing;

		ActionManager.Action action = actionManager.Bowl (pinFall);
		Debug.Log (action);

		if (ActionManager.Action.Tidy == action) {
			animator.SetTrigger ("Tidy");
		} else if (ActionManager.Action.EndTurn == action || ActionManager.Action.Reset == action) {
			lastSettledCount = 10;
			animator.SetTrigger ("Reset");
		} else if (ActionManager.Action.EndGame == action) {
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
