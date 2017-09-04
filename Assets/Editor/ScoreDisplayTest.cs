using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

[TestFixture]
public class ScoreDisplayTest {


	[Test]
	public void T01_Bowl1 () {
		int[] rolls = {1};
		string rollStrings = "1";
		Assert.AreEqual (rollStrings, ScoreDisplay.FormatRolls (rolls.ToList()));
	}

}
