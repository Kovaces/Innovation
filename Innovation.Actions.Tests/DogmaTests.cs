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
	public class DogmaTests
	{
		private Game testGame;
		private Card testCard;
		private Player testPlayer;

		[TestInitialize]
		public void Setup()
		{
			Dictionary<Color, Stack> stackPlayer1 = new Dictionary<Color, Stack>();
			stackPlayer1.Add(Color.Blue, new Stack
			{
				Cards = new List<ICard>()
					{
						 new Card { Name = "Test Blue Card", Color = Color.Blue, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Crown, Right = Symbol.Tower }
					},
				SplayedDirection = SplayDirection.None
			});
			Dictionary<Color, Stack> stackPlayer2 = new Dictionary<Color, Stack>();
			stackPlayer2.Add(Color.Red, new Stack
			{
				Cards = new List<ICard>()
					{
						 new Card { Name = "Test Red Card", Color = Color.Red, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Crown, Right = Symbol.Tower }
					},
				SplayedDirection = SplayDirection.None
			});

			testGame = new Game
			{
				Players = new List<Player>()
				{
					new Player
					{
						Name = "Test Player 1",
						Tableau = new Tableau
						{
							NumberOfAchievements = 0,
							ScorePile = new List<ICard>(),
							Stacks = stackPlayer1
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
							Stacks = stackPlayer2
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
							new Card { Name = "Test Yellow Card", Color = Color.Yellow, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower },
							new Card { Name = "Test Yellow Card", Color = Color.Yellow, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower },
							new Card { Name = "Test Yellow Card", Color = Color.Yellow, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower }
						}
					}
				}
			};

			testCard = new Card
			{
				Name = "Test Blue Card",
				Color = Color.Blue,
				Age = 1,
				Top = Symbol.Blank,
				Left = Symbol.Tower,
				Center = Symbol.Tower,
				Right = Symbol.Tower,
				Actions = new List<CardAction>()
				{
					new CardAction(ActionType.Required, Symbol.Crown, "Action Text", RequiredAction)
				}
			};


			testPlayer = testGame.Players[0];
		}

		[TestMethod]
		public void DogmaAction_Base()
		{
			Dogma.Action(testCard, testPlayer, testGame);
			Assert.AreEqual(5, testGame.Players[0].Hand.Count);   // starts with 3, action adds 1, player2 shares adds 1
			Assert.AreEqual(3, testGame.Players[1].Hand.Count);   // starts with 2, action adds 1
		}

		private bool RequiredAction(object[] parameters)
		{
			// Draw a [1].

			if (parameters.Length < 2)
				throw new ArgumentOutOfRangeException("parameters", "Parameter list must include Player and Game");

			var player = parameters[0] as Player;
			if (player == null)
				throw new NullReferenceException("Player cannot be null");

			var game = parameters[1] as Game;
			if (game == null)
				throw new NullReferenceException("Game cannot be null");

			player.Hand.Add(Draw.Action(1, game));

			return true;
		}
		private bool DemandAction(object[] parameters){
			return true;
		}
	}
}
