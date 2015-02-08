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
	public class AchieveTests
	{
		private Game testGame;
		private Player testPlayer;

		[TestInitialize]
		public void Setup()
		{
			testGame = new Game
			{
				AgeAchievementDeck = new Deck(),
			};

			testPlayer = new Player
			{
				Tableau = new Tableau
				{
					ScorePile = new List<ICard>
					{
						new Card {Age = 1},
						new Card {Age = 2},
						new Card {Age = 3},
						new Card {Age = 4},
						new Card {Age = 5},
					},
					Stacks = new Dictionary<Color, Stack>
					{
						{ Color.Blue, new Stack { Cards = new List<ICard>{ new Card{ Age = 2} }} }
					}
				}
			};
		}

		[TestMethod]
		public void AchieveAction_Base()
		{
			testGame.AgeAchievementDeck.Cards.Add(new Card { Age = 1 });
			Assert.IsTrue(Achieve.Action(testPlayer, testGame));
		}

		[TestMethod]
		public void AchieveAction_PlayerHasScoreButNotTopCard()
		{
			testGame.AgeAchievementDeck.Cards.Add(new Card { Age = 3 });
			Assert.IsFalse(Achieve.Action(testPlayer, testGame));
		}

		[TestMethod]
		public void AchieveAction_PlayerHasTopCardButNotScore()
		{
			testPlayer.Tableau.ScorePile = new List<ICard>
			{
				new Card {Age = 1},
			};

			testGame.AgeAchievementDeck.Cards.Add(new Card { Age = 2 });
			Assert.IsFalse(Achieve.Action(testPlayer, testGame));
		}
	}
}
