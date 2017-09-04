using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour {

	public Text[] rolls, frames;

	// Use this for initialization
	void Start () {
		rolls [0].text = "X";
		frames [0].text = "0";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
