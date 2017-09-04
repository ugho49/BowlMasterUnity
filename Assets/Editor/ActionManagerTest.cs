using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class ActionManagerTest {

	private ActionManager.Action reset = ActionManager.Action.Reset;
	private ActionManager.Action endTurn = ActionManager.Action.EndTurn;
	private ActionManager.Action endGame = ActionManager.Action.EndGame;
	private ActionManager.Action tidy = ActionManager.Action.Tidy;


	[Test]
	public void T01_OneStrikeReturnsEndTurn() {
		int[] rolls = {10};
		Assert.AreEqual (endTurn, ActionManager.NextAction(rolls.ToList()));
	}

	[Test]
	public void T02_Bowl8ReturnsTidy() {
		int[] rolls = {8};
		Assert.AreEqual (tidy, ActionManager.NextAction(rolls.ToList()));
	}

	[Test]
	public void T03_SpareReturnsEndTurn() {
		int[] rolls = {2,8};
		Assert.AreEqual (endTurn, ActionManager.NextAction(rolls.ToList()));
	}

	[Test]
	public void T04_CheckResetAtSrikeInLastFrame() {
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10};
		Assert.AreEqual (reset, ActionManager.NextAction(rolls.ToList()));
	}

	[Test]
	public void T05_CheckResetAtSpareInLastFrame() {
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 4,6};
		Assert.AreEqual (reset, ActionManager.NextAction(rolls.ToList()));
	}

	[Test]
	public void T06_CheckEndGameAtBowl21() {
		int[] rolls = {8,2, 7,3, 3,4, 10, 2,8, 10, 10, 8,0, 10, 8,2,9};
		Assert.AreEqual (endGame, ActionManager.NextAction(rolls.ToList()));
	}

	[Test]
	public void T07_CheckEndGameAtBowl20() {
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1};
		Assert.AreEqual (endGame, ActionManager.NextAction(rolls.ToList()));
	}

	[Test]
	public void T08_CheckTidyAtBowl20WithBowl21Awarded() {
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10, 5};
		Assert.AreEqual (tidy, ActionManager.NextAction(rolls.ToList()));
	}

	[Test]
	public void T09_CheckTidyAtBowl20WithBowl21Awarded() {
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10, 0};
		Assert.AreEqual (tidy, ActionManager.NextAction(rolls.ToList()));
	}

	[Test]
	public void T10_BowlIndexTest() {
		int[] rolls = {0, 10, 5, 1};
		Assert.AreEqual (endTurn, ActionManager.NextAction(rolls.ToList()));
	}

	[Test]
	public void T11_10thFrameTurkey() {
		int[] rolls = {1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 1,1, 10, 10, 10};
		Assert.AreEqual (endGame, ActionManager.NextAction(rolls.ToList()));
	}

	[Test]
	public void T12_OThen1EqualEndTurn() {
		int[] rolls = {0, 1};
		Assert.AreEqual (endTurn, ActionManager.NextAction(rolls.ToList()));
	}
}
