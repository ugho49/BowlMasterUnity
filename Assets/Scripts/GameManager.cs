using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private List<int> rolls; 

	private PinSetter pinSetter;
	private Ball ball;
	private ScoreDisplay scoreDisplay;

	// Use this for initialization
	void Start () {
		rolls = new List<int> ();
		pinSetter = GameObject.FindObjectOfType<PinSetter> ();
		ball = GameObject.FindObjectOfType<Ball> ();
		scoreDisplay = GameObject.FindObjectOfType<ScoreDisplay> ();
	}

	public void Bowl (int pinFall) {
		rolls.Add (pinFall);
		ball.Reset ();
		pinSetter.PerformAction (ActionManager.NextAction (rolls));
		scoreDisplay.FillRolls (rolls);
		scoreDisplay.FillFrames (ScoreManager.ScoreCumulative(rolls));
	}
}
