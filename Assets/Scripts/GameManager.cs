using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private List<int> bowls; 

	private PinSetter pinSetter;
	private Ball ball;

	// Use this for initialization
	void Start () {
		bowls = new List<int> ();
		pinSetter = GameObject.FindObjectOfType<PinSetter> ();
		ball = GameObject.FindObjectOfType<Ball> ();
	}

	public void Bowl (int pinFall) {
		bowls.Add (pinFall);

		ActionManager.Action nextAction = ActionManager.NextAction (bowls);
		pinSetter.PerformAction (nextAction);
		ball.Reset ();
	}
}
