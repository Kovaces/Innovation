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
	public class MetalworkingTest
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
							 new Card { Name = "Test Yellow Card", Color = Color.Yellow, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower },
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
							new Card { Name = "Test Blue Card", Color = Color.Blue, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower },
							new Card { Name = "Test Yellow Card", Color = Color.Yellow, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Leaf, Right = Symbol.Leaf },
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

		//ActionType.Required, Symbol.Tower, "Draw and reveal a [1]. If it has a [TOWER], score it and repeat this dogma effect. Otherwise, keep it."

		[TestMethod]
		public void Card_MetalworkingAction1()
		{
			bool result = new Metalworking().Actions.ToList()[0].ActionHandler(new CardActionParameters { TargetPlayer = testGame.Players[0], Game = testGame, ActivePlayer = testGame.Players[0], PlayerSymbolCounts = new Dictionary<IPlayer, Dictionary<Symbol, int>>() });

            Assert.AreEqual(true, result);
            
            Assert.AreEqual(4, testGame.Players[0].Hand.Count);
			Assert.AreEqual(1, testGame.AgeDecks.Where(x => x.Age == 1).FirstOrDefault().Cards.Count);
			Assert.AreEqual(3, testGame.AgeDecks.Where(x => x.Age == 2).FirstOrDefault().Cards.Count);

			Assert.AreEqual(1, testGame.Players[0].Tableau.ScorePile.Count);
			Assert.AreEqual(1, testGame.Players[0].Tableau.GetScore());
			Assert.AreEqual(0, testGame.Players[1].Tableau.ScorePile.Count);

			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Blue].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Green].Cards.Count);
			Assert.AreEqual(1, testGame.Players[0].Tableau.Stacks[Color.Red].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Purple].Cards.Count);
			Assert.AreEqual(0, testGame.Players[0].Tableau.Stacks[Color.Yellow].Cards.Count);

		}
	}
}
