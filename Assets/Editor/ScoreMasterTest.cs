using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

[TestFixture]
public class ScoreMasterTest {

	private ScoreMaster scoreMaster;
	private ScoreMaster.Action reset = ScoreMaster.Action.Reset;
	private ScoreMaster.Action endTurn = ScoreMaster.Action.EndTurn;
	private ScoreMaster.Action endGame = ScoreMaster.Action.EndGame;
	private ScoreMaster.Action tidy = ScoreMaster.Action.Tidy;

	[SetUp]
	public void Setup() {
		scoreMaster = new ScoreMaster ();
	}

	[Test]
	public void T01_OneStrikeReturnsEndTurn() {
		Assert.AreEqual (endTurn, scoreMaster.Bowl(10));
	}

	[Test]
	public void T02_Bowl8ReturnsTidy() {
		Assert.AreEqual (tidy, scoreMaster.Bowl(8));
	}

	[Test]
	public void T03_SpareReturnsEndTurn() {
		scoreMaster.Bowl(2);
		Assert.AreEqual (endTurn, scoreMaster.Bowl(8));
	}

	[Test]
	public void T04_CheckResetAtSrikeInLastFrame() {
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1};

		foreach (int roll in rolls) {
			scoreMaster.Bowl (roll);
		}

		Assert.AreEqual (reset, scoreMaster.Bowl (10));
	}

	[Test]
	public void T05_CheckResetAtSpareInLastFrame() {
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1};

		foreach (int roll in rolls) {
			scoreMaster.Bowl (roll);
		}

		scoreMaster.Bowl (4);
		Assert.AreEqual (reset, scoreMaster.Bowl (6));
	}

	[Test]
	public void T06_CheckEndGameAtBowl21() {
		int[] rolls = {8,2, 7,3, 3,4, 10, 2,8, 10, 10, 8,0, 10, 8,2,9};

		foreach (int roll in rolls) {
			scoreMaster.Bowl (roll);
		}

		Assert.AreEqual (endGame, scoreMaster.Bowl (9));
	}

	[Test]
	public void T07_CheckEndGameAtBowl20() {
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1};

		foreach (int roll in rolls) {
			scoreMaster.Bowl (roll);
		}

		Assert.AreEqual (endGame, scoreMaster.Bowl (1));
	}

	[Test]
	public void T08_CheckTidyAtBowl20WithBowl21Awarded() {
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10};

		foreach (int roll in rolls) {
			scoreMaster.Bowl (roll);
		}

		Assert.AreEqual (tidy, scoreMaster.Bowl (5));
	}

	[Test]
	public void T09_CheckTidyAtBowl20WithBowl21Awarded() {
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10};

		foreach (int roll in rolls) {
			scoreMaster.Bowl (roll);
		}

		Assert.AreEqual (tidy, scoreMaster.Bowl (0));
	}
}
