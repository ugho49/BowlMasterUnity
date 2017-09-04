﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager {

	public enum Action { Reset, Tidy, EndTurn, EndGame }

	private int[] bowls = new int[21];
	private int bowl = 1;

	public static Action NextAction(List<int> pinsFalls) {
		ActionManager am = new ActionManager ();
		Action currentAction = new Action ();

		foreach (int pinfall in pinsFalls) {
			currentAction = am.Bowl (pinfall);
		}

		return currentAction;
	}

	private Action Bowl(int pins) {

		if (pins < 0 || pins > 10) {
			throw new UnityException ("Invalid pins");
		}

		bowls [bowl - 1] = pins;

		if (bowl == 21) {
			return Action.EndGame;
		}

		if (bowl >= 19 && pins == 10) {
			bowl++;
			return Action.Reset;
		} else if (bowl == 20) {
			bowl++;
			if (bowls [19 - 1] == 10 && bowls [20 - 1] == 0) {
				return Action.Tidy;
			} else if (((bowls [19 - 1] + bowls [20 - 1]) % 10) == 0) {
				return Action.Reset;
			} else if (Bowl21Awarded ()) {
				return Action.Tidy;
			} else {
				return Action.EndGame;
			}
		}

		if (bowl % 2 != 0) {

			if (pins == 10) {
				bowl += 2;
				return Action.EndTurn;
			} 

			bowl++;
			return Action.Tidy;
		} else {
			bowl++;
			return Action.EndTurn;
		}

		throw new UnityException ("Undefined type of action");
	}

	private bool Bowl21Awarded () {
		return (bowls [19 - 1] + bowls [20 - 1] >= 10);
	}
}
