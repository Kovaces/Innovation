using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Innovation.Actions.Tests
{
	[TestClass]
	public class ReturnTests
	{
		private Game testGame;
		private Card testCard;

		[TestInitialize]
		public void Setup()
		{
			testCard = new Card { Name = "Test Blue Card", Color = Color.Blue, Age = 1, Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower };

			testGame = new Game
			{
				AgeDecks = new List<Deck>() {
						new Deck{
							 Age = 1,
							  Cards = new List<ICard> {
									new Card { Name = "Test Green Card", Color = Color.Green, Age = 1, Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower }
								}
						},
						new Deck{
							 Age = 2,
							  Cards = new List<ICard> {
									new Card { Name = "Test Red Card", Color = Color.Green, Age = 2, Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower }
								}
						}
				},
			};
		}

		[TestMethod]
		public void ReturnAction_Base()
		{
			Return.Action(testCard, testGame);
			Assert.AreEqual(testCard, testGame.AgeDecks[0].Cards[1]);
			Assert.AreEqual(2, testGame.AgeDecks[0].Cards.Count);
			Assert.AreEqual(1, testGame.AgeDecks[1].Cards.Count);
		}
	}
}
