﻿using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Innovation.Actions;
using Innovation.Models.Interfaces;

namespace Innovation.Cards.Tests
{
	[TestClass]
	public class OarsTest
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
							 new Card { Name = "Test Yellow Card - traded", Color = Color.Yellow, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower },
							 new Card { Name = "Test Blue Card", Color = Color.Blue, Age = 1, Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower }
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
				 new Card { Name = "Test Blue Card", Color = Color.Blue, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Crown, Right = Symbol.Leaf }
			);
			testGame.Players[0].Tableau.Stacks[Color.Red].AddCardToTop(
				 new Card { Name = "Test Red Card", Color = Color.Red, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Crown, Right = Symbol.Tower }
			);

			testGame.Players[1].Tableau.Stacks[Color.Red].AddCardToTop(
				 new Card { Name = "Test Red Card", Color = Color.Red, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Crown, Right = Symbol.Tower }
            );

            Mocks.ConvertPlayersToMock(testGame);
        }

		//ActionType.Demand, Symbol.Tower, "I demand you transfer a card with a [CROWN] from your hand to my score pile! If you do, draw a [1]!"

		[TestMethod]
		public void Card_OarsAction1()
		{
			//testGame.Players[0].AlwaysParticipates = true;
			//testGame.Players[0].SelectsCards = new List<int>() { 0 };

			//testGame.Players[1].SelectsCards = new List<int>() { 0 };

			string cardName = testGame.Players[1].Hand.Where(x => x.HasSymbol(Symbol.Crown)).First().Name;

			bool result = new Oars().Actions.ToList()[0].ActionHandler(new CardActionParameters { TargetPlayer = testGame.Players[1], Game = testGame, ActivePlayer = testGame.Players[0], PlayerSymbolCounts = new Dictionary<IPlayer, Dictionary<Symbol, int>>() });

			Assert.AreEqual(true, result);

			Assert.AreEqual(1, testGame.Players[0].Tableau.ScorePile.Count);
			Assert.AreEqual(1, testGame.Players[0].Tableau.GetScore());
			Assert.AreEqual(cardName, testGame.Players[0].Tableau.ScorePile.First().Name);
			Assert.AreEqual(2, testGame.Players[1].Hand.Count);

			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Blue].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Green].Cards.Count);
			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Red].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Purple].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Yellow].Cards.Count);
		}

		[TestMethod]
		public void Card_OarsAction1_NoCrowns()
		{
			//testGame.Players[0].AlwaysParticipates = true;
			//testGame.Players[0].SelectsCards = new List<int>() { 0 };

			//testGame.Players[1].SelectsCards = new List<int>() { 0 };

			testGame.Players[1].Hand.Remove(testGame.Players[1].Hand.First());

			bool result = new Oars().Actions.ToList()[0].ActionHandler(new CardActionParameters { TargetPlayer = testGame.Players[1], Game = testGame, ActivePlayer = testGame.Players[0], PlayerSymbolCounts = new Dictionary<IPlayer, Dictionary<Symbol, int>>() });

			Assert.AreEqual(false, result);

			Assert.AreEqual(1, testGame.Players[1].Hand.Count);

			Assert.AreEqual(0, testGame.Players[0].Tableau.ScorePile.Count);
			Assert.AreEqual(0, testGame.Players[1].Tableau.ScorePile.Count);

			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Blue].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Green].Cards.Count);
			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Red].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Purple].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Yellow].Cards.Count);
		}


		//ActionType.Required, Symbol.Tower, "If no cards were transfered due to this demand, draw a [1]."
        
		[TestMethod]
		public void Card_OarsAction2_NothingHappens()
		{
			//testGame.Players[0].AlwaysParticipates = true;
			//testGame.Players[0].SelectsCards = new List<int>() { 0 };

			//testGame.Players[1].SelectsCards = new List<int>() { 0 };

			string cardName = testGame.Players[1].Hand.Where(x => x.HasSymbol(Symbol.Crown)).First().Name;

			new Oars().Actions.ToList()[0].ActionHandler(new CardActionParameters { TargetPlayer = testGame.Players[1], Game = testGame, ActivePlayer = testGame.Players[0], PlayerSymbolCounts = new Dictionary<IPlayer, Dictionary<Symbol, int>>() });
			bool result = new Oars().Actions.ToList()[1].ActionHandler(new CardActionParameters { TargetPlayer = testGame.Players[1], Game = testGame, ActivePlayer = testGame.Players[1], PlayerSymbolCounts = new Dictionary<IPlayer, Dictionary<Symbol, int>>() });

			Assert.AreEqual(false, result);

			Assert.AreEqual(2, testGame.Players[1].Hand.Count);

			Assert.AreEqual(1, testGame.Players[0].Tableau.ScorePile.Count);
			Assert.AreEqual(cardName, testGame.Players[0].Tableau.ScorePile.First().Name);
			Assert.AreEqual(0, testGame.Players[1].Tableau.ScorePile.Count);

			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Blue].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Green].Cards.Count);
			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Red].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Purple].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Yellow].Cards.Count);
		}

		[TestMethod]
		public void Card_OarsAction2_NoTransfer()
		{
			//testGame.Players[0].AlwaysParticipates = true;
			//testGame.Players[0].SelectsCards = new List<int>() { 0 };

			//testGame.Players[1].SelectsCards = new List<int>() { 0 };

			testGame.Players[1].Hand.Remove(testGame.Players[1].Hand.First());

			new Oars().Actions.ToList()[0].ActionHandler(new CardActionParameters { TargetPlayer = testGame.Players[1], Game = testGame, ActivePlayer = testGame.Players[0], PlayerSymbolCounts = new Dictionary<IPlayer, Dictionary<Symbol, int>>() });
			bool result = new Oars().Actions.ToList()[1].ActionHandler(new CardActionParameters { TargetPlayer = testGame.Players[1], Game = testGame, ActivePlayer = testGame.Players[1], PlayerSymbolCounts = new Dictionary<IPlayer, Dictionary<Symbol, int>>() });

			Assert.AreEqual(true, result);

			Assert.AreEqual(2, testGame.Players[1].Hand.Count);

			Assert.AreEqual(0, testGame.Players[0].Tableau.ScorePile.Count);
			Assert.AreEqual(0, testGame.Players[1].Tableau.ScorePile.Count);

			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Blue].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Green].Cards.Count);
			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Red].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Purple].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Yellow].Cards.Count);
		}

	}
}
