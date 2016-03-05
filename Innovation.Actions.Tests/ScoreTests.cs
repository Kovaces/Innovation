using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Interfaces;


using Innovation.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Innovation.Actions.Tests
{
	[TestClass]
	public class ScoreTests
	{
		private Card testCard;
		private Player testPlayer;

		[TestInitialize]
		public void Setup()
		{
			testCard = new Card { Name = "Test Blue Card", Color = Color.Blue, Age = 1, Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower };

			testPlayer = new Player
			{
				Tableau = new Tableau
				{
					ScorePile = new List<ICard> { }
				}
			};
		}

		[TestMethod]
		public void ScoreAction_Base()
		{
			Score.Action(testCard, testPlayer);
			Assert.AreEqual(testCard, testPlayer.Tableau.ScorePile.First());
			Assert.AreEqual(1, testPlayer.Tableau.GetScore());
		}
	}
}
