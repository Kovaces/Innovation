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

		[TestInitialize]
		public void Setup()
		{
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
							new Card { Name = "Test Yellow Card", Color = Color.Yellow, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower },
							new Card { Name = "Test Yellow Card", Color = Color.Yellow, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower },
							new Card { Name = "Test Yellow Card", Color = Color.Yellow, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Tower }
						}
					}
				}
			};

			testGame.Players[0].Tableau.Stacks[Color.Blue].AddCardToTop(
				 new Card { Name = "Test Blue Card", Color = Color.Blue, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Crown, Right = Symbol.Leaf }
			);

			testGame.Players[1].Tableau.Stacks[Color.Red].AddCardToTop(
				 new Card { Name = "Test Red Card", Color = Color.Red, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Crown, Right = Symbol.Tower }
			);
		}

		private bool DrawCardAction(object[] parameters)
		{
			if (parameters.Length < 2)
				throw new ArgumentOutOfRangeException("parameters", "Parameter list must include Player and Game");

			var targetPlayer = parameters[0] as Player;
			if (targetPlayer == null)
				throw new NullReferenceException("Player cannot be null");

			var game = parameters[1] as Game;
			if (game == null)
				throw new NullReferenceException("Game cannot be null");

			// Draw a [1].
			targetPlayer.Hand.Add(Draw.Action(1, game));

			return true;
		}
		private bool TransferCardAction(object[] parameters)
		{
			if (parameters.Length < 2)
				throw new ArgumentOutOfRangeException("parameters", "Parameter list must include Player and Game");

			var targetPlayer = parameters[0] as Player;
			if (targetPlayer == null)
				throw new NullReferenceException("Target Player cannot be null");

			var game = parameters[1] as Game;
			if (game == null)
				throw new NullReferenceException("Game cannot be null");

			var activePlayer = parameters[2] as Player;
			if (activePlayer == null)
				throw new NullReferenceException("Active Player cannot be null");

			// I demand you transfer a card from your hand to my hand
			ICard card = targetPlayer.Hand.First();
			targetPlayer.Hand.Remove(card);
			activePlayer.Hand.Add(card);

			return true;
		}

		[TestMethod]
		public void DogmaAction_Required_Shares()
		{
			testGame.Players[0].AlwaysParticipates = false;
			testGame.Players[1].AlwaysParticipates = false;
			
			var testCard = new Card
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
					new CardAction(ActionType.Required, Symbol.Crown, "Action Text", DrawCardAction)
				}
			};

			Dogma.Action(testCard, testGame.Players[0], testGame);
			Assert.AreEqual(5, testGame.Players[0].Hand.Count);   // starts with 3, action adds 1, player2 shares adds 1
			Assert.AreEqual(3, testGame.Players[1].Hand.Count);   // starts with 2, action adds 1
		}

		[TestMethod]
		public void DogmaAction_Required_DoesNotShare()
		{
			testGame.Players[0].AlwaysParticipates = false;
			testGame.Players[1].AlwaysParticipates = false;

			testGame.Players[0].Tableau.Stacks[Color.Yellow].AddCardToTop(
				 new Card { Name = "Test Yellow Card", Color = Color.Yellow, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Crown, Right = Symbol.Tower }
			);

			var testCard = new Card
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
					new CardAction(ActionType.Required, Symbol.Crown, "Action Text", DrawCardAction)
				}
			};

			Dogma.Action(testCard, testGame.Players[0], testGame);
			Assert.AreEqual(4, testGame.Players[0].Hand.Count);   // starts with 3, action adds 1
			Assert.AreEqual(2, testGame.Players[1].Hand.Count);   // starts with 2, action adds 1
		}

		[TestMethod]
		public void DogmaAction_Demand_Player2Affected()
		{
			testGame.Players[0].AlwaysParticipates = false;
			testGame.Players[1].AlwaysParticipates = false;

			testGame.Players[0].Tableau.Stacks[Color.Red].AddCardToTop(
				 new Card { Name = "Test Red Card", Color = Color.Red, Age = 1, Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Crown, Right = Symbol.Tower }
			);

			var testCard = new Card
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
					new CardAction(ActionType.Demand, Symbol.Crown, "Action Text", TransferCardAction)
				}
			};

			Dogma.Action(testCard, testGame.Players[0], testGame);
			Assert.AreEqual(4, testGame.Players[0].Hand.Count);   // starts with 3, player2 gives 1, no draw
			Assert.AreEqual(1, testGame.Players[1].Hand.Count);   // starts with 2, gives 1 to player1
		}

		[TestMethod]
		public void DogmaAction_Demand_Player2Unaffected()
		{
			testGame.Players[0].AlwaysParticipates = false;
			testGame.Players[1].AlwaysParticipates = false;
			
			var testCard = new Card
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
					new CardAction(ActionType.Demand, Symbol.Crown, "Action Text", TransferCardAction)
				}
			};

			Dogma.Action(testCard, testGame.Players[0], testGame);
			Assert.AreEqual(3, testGame.Players[0].Hand.Count);   // starts with 3
			Assert.AreEqual(2, testGame.Players[1].Hand.Count);   // starts with 2
		}



		[TestMethod]
		public void DogmaAction_Optional_BothTakePart()
		{
			testGame.Players[0].AlwaysParticipates = true;
			testGame.Players[1].AlwaysParticipates = true;

			var testCard = new Card
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
					new CardAction(ActionType.Optional, Symbol.Crown, "Action Text", DrawCardAction)
				}
			};

			Dogma.Action(testCard, testGame.Players[0], testGame);
			Assert.AreEqual(5, testGame.Players[0].Hand.Count);   // starts with 3, action adds 1, player2 shares adds 1
			Assert.AreEqual(3, testGame.Players[1].Hand.Count);   // starts with 2, action adds 1
		}

		[TestMethod]
		public void DogmaAction_Optional_BothAbdicate()
		{
			testGame.Players[0].AlwaysParticipates = false;
			testGame.Players[1].AlwaysParticipates = false;

			var testCard = new Card
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
					new CardAction(ActionType.Optional, Symbol.Crown, "Action Text", DrawCardAction)
				}
			};

			Dogma.Action(testCard, testGame.Players[0], testGame);
			Assert.AreEqual(3, testGame.Players[0].Hand.Count);   // starts with 3
			Assert.AreEqual(2, testGame.Players[1].Hand.Count);   // starts with 2
		}

		[TestMethod]
		public void DogmaAction_Optional_Player2Abdicates()
		{
			testGame.Players[0].AlwaysParticipates = true;
			testGame.Players[1].AlwaysParticipates = false;

			var testCard = new Card
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
					new CardAction(ActionType.Optional, Symbol.Crown, "Action Text", DrawCardAction)
				}
			};

			Dogma.Action(testCard, testGame.Players[0], testGame);
			Assert.AreEqual(4, testGame.Players[0].Hand.Count);   // starts with 3, draws 1
			Assert.AreEqual(2, testGame.Players[1].Hand.Count);   // starts with 2
		}
	}
}
