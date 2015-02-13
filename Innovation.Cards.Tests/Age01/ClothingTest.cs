using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Tests.Helpers;
using Innovation.Actions;
using Innovation.Models.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Innovation.Cards.Tests
{
	[TestClass]
	public class ClothingTest
	{
		private Game testGame;

		[TestInitialize]
		public void Setup()
		{
			testGame = new Game
			{
				Players = new List<IPlayer>()
				{
					new Player
					{
						Name = "Test Player 1",
						Tableau = new Tableau
						{
							NumberOfAchievements = 0,
							ScorePile = new List<ICard>(),
						},
						Hand = new List<ICard>() 
						{
							 new Card { Name = "Test Red Card", Color = Color.Red, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower },
							 new Card { Name = "Test Green Card", Color = Color.Green, Age = 1, Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower },
							 new Card { Name = "Test Blue Card", Color = Color.Blue, Age = 1, Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower }
						}
					},
					new Player
					{
						Name = "Test Player 2",
						Tableau = new Tableau
						{
							NumberOfAchievements = 0,
							ScorePile = new List<ICard>(),
						},
						Hand = new List<ICard>() 
						{
							 new Card { Name = "Test Red Card", Color = Color.Red, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower },
						}
					}
				},
				AgeDecks = new List<Deck>() 
				{
					new	Deck 
					{
						Age = 1,
						Cards = new List<ICard>() 
						{
							new Card { Name = "Test Yellow Card", Color = Color.Yellow, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower },
							new Card { Name = "Test Yellow Card", Color = Color.Yellow, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower },
							new Card { Name = "Test Purple Card", Color = Color.Purple, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower }
						}
					},
					new	Deck 
					{
						Age = 2,
						Cards = new List<ICard>() 
						{
							new Card { Name = "Test Yellow Card", Color = Color.Yellow, Age = 2, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower },
							new Card { Name = "Test Blue Card", Color = Color.Blue, Age = 2, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower },
							new Card { Name = "Test Green Card", Color = Color.Green, Age = 2, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower }
						}
					}
				}
			};


			testGame.Players[0].Tableau.Stacks[Color.Blue].AddCardToTop(
				new Card { Name = "Test Blue Card", Color = Color.Blue, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Crown, Right = Symbol.Tower }
			);
			testGame.Players[0].Tableau.Stacks[Color.Red].AddCardToTop(
				new Card { Name = "Test Red Card", Color = Color.Red, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Crown, Right = Symbol.Leaf }
			);
			testGame.Players[0].Tableau.Stacks[Color.Yellow].AddCardToTop(
				new Card { Name = "Test Yellow Card", Color = Color.Yellow, Age = 1, Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower }
			);

			testGame.Players[1].Tableau.Stacks[Color.Red].AddCardToTop(
				new Card { Name = "Test Red Card", Color = Color.Red, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Crown, Right = Symbol.Leaf }
			);
		}

		//ActionType.Required, Symbol.Leaf, "Meld a card from your hand of different color from any card on your board."

		[TestMethod]
		public void Card_ClothingAction1()
		{
			// player1:  blue,red,yellow stack;	red,yellow,green card;	melds green card

			//testGame.Players[0].SelectsCards = new List<int>() { 0 };

			new Clothing().Actions.ToList()[0].ActionHandler(new CardActionParameters { TargetPlayer = testGame.Players[0], Game = testGame, ActivePlayer = testGame.Players[0], PlayerSymbolCounts = new Dictionary<IPlayer, Dictionary<Symbol, int>>() });

			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Green].Cards.Count);
			Assert.AreEqual(2, testGame.Players[0].Hand.Count);
		}

		[TestMethod]
		public void Card_ClothingAction1_ColorsMatch()
		{
			// player2:  red stack;				red card;				nothing happens

			new Clothing().Actions.ToList()[0].ActionHandler(new CardActionParameters { TargetPlayer = testGame.Players[1], Game = testGame, ActivePlayer = testGame.Players[1], PlayerSymbolCounts = new Dictionary<IPlayer, Dictionary<Symbol, int>>() });
			Assert.AreEqual(1, testGame.Players[1].Tableau.Stacks[Color.Red].Cards.Count);
			Assert.AreEqual(1, testGame.Players[1].Hand.Count);
		}


		// ActionType.Required, Symbol.Leaf, "Draw and score a [1] for each color present on your board not present on any other player's board."

		[TestMethod]
		public void Card_ClothingAction2()
		{
			// player1: red,blue,green
			// player2:	red
			// player1 -> draws + scores 2 cards

			//testGame.Players[0].SelectsCards = new List<int>() { 0 };

			new Clothing().Actions.ToList()[1].ActionHandler(new CardActionParameters { TargetPlayer = testGame.Players[0], Game = testGame, ActivePlayer = testGame.Players[0], PlayerSymbolCounts = new Dictionary<IPlayer, Dictionary<Symbol, int>>() });

			Assert.AreEqual(3, testGame.Players[0].Hand.Count);

			Assert.AreEqual(2, testGame.Players[0].Tableau.ScorePile.Count);
			Assert.AreEqual(2, testGame.Players[0].Tableau.GetScore());
			Assert.AreEqual(0, testGame.Players[1].Tableau.ScorePile.Count);

			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Blue].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Green].Cards.Count);
			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Red].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Purple].Cards.Count);
			Assert.AreEqual(0, testGame.Players[1].Tableau.Stacks[Color.Yellow].Cards.Count);
		}

		[TestMethod]
		public void Card_ClothingAction2_NothingHappens()
		{
			// player1: red,blue,green
			// player2:	red
			// player2 -> nothing happens

			new Clothing().Actions.ToList()[1].ActionHandler(new CardActionParameters { TargetPlayer = testGame.Players[1], Game = testGame, ActivePlayer = testGame.Players[1], PlayerSymbolCounts = new Dictionary<IPlayer, Dictionary<Symbol, int>>() });

			Assert.AreEqual(1, testGame.Players[1].Hand.Count);
			
			Assert.AreEqual(0, testGame.Players[0].Tableau.ScorePile.Count);
			Assert.AreEqual(0, testGame.Players[1].Tableau.ScorePile.Count);

			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Blue].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Green].Cards.Count);
			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Red].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Purple].Cards.Count);
			Assert.AreEqual(0, testGame.Players[1].Tableau.Stacks[Color.Yellow].Cards.Count);
		}
	}
}