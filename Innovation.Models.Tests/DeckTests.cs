using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models.Enums;
using Innovation.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Innovation.Models.Tests
{
	[TestClass]
	public class DeckTests
	{

		private Deck testDeck;
		private Card testCard;

		[TestInitialize]
		public void Setup()
		{
			testCard = new Card {Top = Symbol.Leaf, Left = Symbol.Leaf, Center = Symbol.Blank, Right = Symbol.Crown};
			testDeck = new Deck
			{
				Cards = new List<ICard>
				{
					testCard,
					new Card {Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Crown},
					new Card {Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower},
				},
				Age = 1,

			};
		}

		[TestMethod]
		public void Deck_Draw_WhenDeckHasZeroCards()
		{
			var emptyDeck = new Deck { Age = 1, Cards = new List<ICard>() };

			Assert.IsNull(emptyDeck.Draw());
		}

		[TestMethod]
		public void Deck_Draw()
		{
			var drawnCard = testDeck.Draw();

			Assert.AreEqual(testCard, drawnCard);
			Assert.AreEqual(2, testDeck.Cards.Count());
		}
	}
}
