using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour {

	public GameObject pinSet;

	private PinCounter pinCounter;
	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		pinCounter = GameObject.FindObjectOfType<PinCounter> ();
	}
	
	public void PerformAction(ActionManager.Action action) {
		if (ActionManager.Action.Tidy == action) {
			animator.SetTrigger ("Tidy");
		} else if (ActionManager.Action.EndTurn == action || ActionManager.Action.Reset == action) {
			animator.SetTrigger ("Reset");
			pinCounter.Reset ();
		} else if (ActionManager.Action.EndGame == action) {
			animator.SetTrigger ("Reset");
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

	void OnTriggerExit(Collider collider) {
		if (collider.GetComponent<Pin>()) {
			Destroy (collider.gameObject);
		}
	}
}
