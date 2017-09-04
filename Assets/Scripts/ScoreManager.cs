using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager {

	public static List<int> ScoreCumulative (List<int> rolls) {
		List<int> cumulativeScore = new List<int> ();
		int runningTotal = 0;

		foreach(int frameScore in ScoreFrames(rolls)) {
			runningTotal += frameScore;
			cumulativeScore.Add (runningTotal);
		}

		return cumulativeScore;
	}

	public static List<int> ScoreFrames (List<int> rolls) {
		List<int> frames = new List<int> ();

		for (int i = 1; i < rolls.Count; i += 2) {

			if (frames.Count == 10) { break; }

			int sumOfTwoRolls = rolls [i - 1] + rolls [i];

			if (sumOfTwoRolls < 10) {
				// Normal "open" frame
				frames.Add (sumOfTwoRolls);
			} 

			// Insufficient look-ahead
			if (rolls.Count - i <= 1) { break; }

			if (rolls [i - 1] == 10) {
				i--;
				// Strike
				frames.Add(10 + rolls[i+1] + rolls[i+2]);
			} else if (sumOfTwoRolls == 10) {
				// Spare
				frames.Add (10 + rolls [i + 1]);
			}

		}

		return frames;
	}
}
