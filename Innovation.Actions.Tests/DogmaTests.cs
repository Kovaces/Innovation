﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;
using Innovation.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Innovation.Actions.Tests
{
	[TestClass]
	public class DogmaTests
	{
		private Game testGame;

		private Dictionary<IPlayer, int> playerActionsTaken;
		private Dictionary<IPlayer, int> playerActionsCalled;

		private IPlayer targetedPlayer;
		private IPlayer activePlayer;

		private bool takeAction;
	
		private bool TestRequiredActionHandler(CardActionParameters parameters)
		{
			targetedPlayer = parameters.TargetPlayer;
			activePlayer = parameters.ActivePlayer;

			playerActionsCalled[targetedPlayer]++;
			playerActionsTaken[targetedPlayer]++;

			return true;
		}

		private bool TestOptionalActionHandler(CardActionParameters parameters)
		{
			targetedPlayer = parameters.TargetPlayer;
			activePlayer = parameters.ActivePlayer;

			playerActionsCalled[targetedPlayer]++;
			
			if ((targetedPlayer == activePlayer) || takeAction)
				playerActionsTaken[targetedPlayer]++;

			return takeAction;
		}

		private IPlayer GeneratePlayer(int highestAge, Dictionary<Symbol, int> symbolCounts)
		{
			var testTableau = MockRepository.GenerateStub<ITableau>();
			testTableau.Stub(t => t.GetSymbolCounts()).Return(symbolCounts);
			testTableau.Stub(t => t.GetHighestAge()).Return(highestAge);

			var player = MockRepository.GenerateStub<IPlayer>();
			player.Tableau = testTableau;
			player.Hand = new List<ICard>();

			return player;
		}

		[TestInitialize]
		public void Setup()
		{
			targetedPlayer = null;
			activePlayer = null;
		}

		[TestMethod]
		public void Dogma_Action_OnlyActivePlayerQualifies_RequiredAction()
		{
			IPlayer player1 = GeneratePlayer(1, new Dictionary<Symbol, int>(){{Symbol.Clock, 0},{Symbol.Crown, 0},{Symbol.Factory, 0},{Symbol.Leaf, 0},{Symbol.Lightbulb, 0},{Symbol.Tower, 1},{Symbol.Blank, 0},});
			IPlayer player2 = GeneratePlayer(1, new Dictionary<Symbol, int>(){{Symbol.Clock, 0},{Symbol.Crown, 0},{Symbol.Factory, 0},{Symbol.Leaf, 0},{Symbol.Lightbulb, 0},{Symbol.Tower, 0},{Symbol.Blank, 0},});
			IPlayer player3 = GeneratePlayer(1, new Dictionary<Symbol, int>(){{Symbol.Clock, 0},{Symbol.Crown, 0},{Symbol.Factory, 0},{Symbol.Leaf, 0},{Symbol.Lightbulb, 0},{Symbol.Tower, 0},{Symbol.Blank, 0},});
			IPlayer player4 = GeneratePlayer(1, new Dictionary<Symbol, int>(){{Symbol.Clock, 0},{Symbol.Crown, 0},{Symbol.Factory, 0},{Symbol.Leaf, 0},{Symbol.Lightbulb, 0},{Symbol.Tower, 0},{Symbol.Blank, 0},});

			playerActionsTaken = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };
			playerActionsCalled = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };

			testGame = new Game { Players = new List<IPlayer> { player1,player2,player3,player4, }, AgeDecks = new List<Deck>{new Deck{Age = 1, Cards = new List<ICard>{new Card()}}}};
			
			var testCard = MockRepository.GenerateStub<ICard>();
			testCard.Stub(c => c.Actions).Return(new List<CardAction> { new CardAction(ActionType.Required, Symbol.Tower, "test", TestRequiredActionHandler) });

			Dogma.Action(testCard, player1, testGame);

			Assert.AreEqual(1, playerActionsCalled[player1]);
			Assert.AreEqual(0, playerActionsCalled[player2]);
			Assert.AreEqual(0, playerActionsCalled[player3]);
			Assert.AreEqual(0, playerActionsCalled[player4]);

			Assert.AreEqual(1, playerActionsTaken[player1]);
			Assert.AreEqual(0, playerActionsTaken[player2]);
			Assert.AreEqual(0, playerActionsTaken[player3]);
			Assert.AreEqual(0, playerActionsTaken[player4]);

			Assert.AreEqual(0, player1.Hand.Count()); //test free draw action
			Assert.AreEqual(player1, targetedPlayer); //test to make sure the current player went last
			Assert.AreEqual(player1, activePlayer);

		}

		[TestMethod]
		public void Dogma_Action_OtherPlayersQualify_RequiredAction()
		{
			IPlayer player1 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player2 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player3 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player4 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });

			playerActionsTaken = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };
			playerActionsCalled = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };

			testGame = new Game { Players = new List<IPlayer> { player1, player2, player3, player4, }, AgeDecks = new List<Deck> { new Deck { Age = 1, Cards = new List<ICard> { new Card() } } } };

			var testCard = MockRepository.GenerateStub<ICard>();
			testCard.Stub(c => c.Actions).Return(new List<CardAction> { new CardAction(ActionType.Required, Symbol.Tower, "test", TestRequiredActionHandler) });

			Dogma.Action(testCard, player1, testGame);

			Assert.AreEqual(1, playerActionsCalled[player1]);
			Assert.AreEqual(1, playerActionsCalled[player2]);
			Assert.AreEqual(1, playerActionsCalled[player3]);
			Assert.AreEqual(1, playerActionsCalled[player4]);

			Assert.AreEqual(1, playerActionsTaken[player1]);
			Assert.AreEqual(1, playerActionsTaken[player2]);
			Assert.AreEqual(1, playerActionsTaken[player3]);
			Assert.AreEqual(1, playerActionsTaken[player4]);

			Assert.AreEqual(1, player1.Hand.Count()); //test free draw action
			Assert.AreEqual(player1, targetedPlayer); //test to make sure the current player went last
			Assert.AreEqual(player1, activePlayer);
		}

		[TestMethod]
		public void Dogma_Action_OtherPlayersQualify_OptionalAction_ActionNotTaken()
		{
			IPlayer player1 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player2 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player3 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player4 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });

			playerActionsTaken = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };
			playerActionsCalled = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };

			testGame = new Game { Players = new List<IPlayer> { player1, player2, player3, player4, }, AgeDecks = new List<Deck> { new Deck { Age = 1, Cards = new List<ICard> { new Card() } } } };

			var testCard = MockRepository.GenerateStub<ICard>();
			testCard.Stub(c => c.Actions).Return(new List<CardAction> { new CardAction(ActionType.Optional, Symbol.Tower, "test", TestOptionalActionHandler) });

			takeAction = false;

			Dogma.Action(testCard, player1, testGame);

			Assert.AreEqual(1, playerActionsCalled[player1]);
			Assert.AreEqual(1, playerActionsCalled[player2]);
			Assert.AreEqual(1, playerActionsCalled[player3]);
			Assert.AreEqual(1, playerActionsCalled[player4]);

			Assert.AreEqual(1, playerActionsTaken[player1]);
			Assert.AreEqual(0, playerActionsTaken[player2]);
			Assert.AreEqual(0, playerActionsTaken[player3]);
			Assert.AreEqual(0, playerActionsTaken[player4]);

			Assert.AreEqual(0, player1.Hand.Count()); //test free draw action
			Assert.AreEqual(player1, targetedPlayer); //test to make sure the current player went last
			Assert.AreEqual(player1, activePlayer);
		}

		[TestMethod]
		public void Dogma_Action_OtherPlayersQualify_OptionalAction()
		{
			IPlayer player1 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player2 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player3 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player4 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });

			playerActionsTaken = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };
			playerActionsCalled = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };

			testGame = new Game { Players = new List<IPlayer> { player1, player2, player3, player4, }, AgeDecks = new List<Deck> { new Deck { Age = 1, Cards = new List<ICard> { new Card() } } } };

			var testCard = MockRepository.GenerateStub<ICard>();
			testCard.Stub(c => c.Actions).Return(new List<CardAction> { new CardAction(ActionType.Optional, Symbol.Tower, "test", TestOptionalActionHandler) });

			takeAction = true;

			Dogma.Action(testCard, player1, testGame);

			Assert.AreEqual(1, playerActionsCalled[player1]);
			Assert.AreEqual(1, playerActionsCalled[player2]);
			Assert.AreEqual(1, playerActionsCalled[player3]);
			Assert.AreEqual(1, playerActionsCalled[player4]);

			Assert.AreEqual(1, playerActionsTaken[player1]);
			Assert.AreEqual(1, playerActionsTaken[player2]);
			Assert.AreEqual(1, playerActionsTaken[player3]);
			Assert.AreEqual(1, playerActionsTaken[player4]);

			Assert.AreEqual(1, player1.Hand.Count()); //test free draw action
			Assert.AreEqual(player1, targetedPlayer); //test to make sure the current player went last
			Assert.AreEqual(player1, activePlayer);
		}

		[TestMethod]
		public void Dogma_Action_DemandAction_NoneEligable()
		{
			IPlayer player1 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player2 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player3 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player4 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });

			playerActionsTaken = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };
			playerActionsCalled = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };

			testGame = new Game { Players = new List<IPlayer> { player1, player2, player3, player4, }, AgeDecks = new List<Deck> { new Deck { Age = 1, Cards = new List<ICard> { new Card() } } } };

			var testCard = MockRepository.GenerateStub<ICard>();
			testCard.Stub(c => c.Actions).Return(new List<CardAction> { new CardAction(ActionType.Demand, Symbol.Tower, "test", TestRequiredActionHandler) });

			Dogma.Action(testCard, player1, testGame);

			Assert.AreEqual(0, playerActionsCalled[player1]);
			Assert.AreEqual(0, playerActionsCalled[player2]);
			Assert.AreEqual(0, playerActionsCalled[player3]);
			Assert.AreEqual(0, playerActionsCalled[player4]);

			Assert.AreEqual(0, playerActionsTaken[player1]);
			Assert.AreEqual(0, playerActionsTaken[player2]);
			Assert.AreEqual(0, playerActionsTaken[player3]);
			Assert.AreEqual(0, playerActionsTaken[player4]);

			Assert.AreEqual(0, player1.Hand.Count()); //test free draw action
			Assert.AreEqual(null, targetedPlayer); //test to make sure the current player went last
			Assert.AreEqual(null, activePlayer);
		}

		[TestMethod]
		public void Dogma_Action_DemandAction_SomeEligable()
		{
			IPlayer player1 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player2 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 0 }, { Symbol.Blank, 0 }, });
			IPlayer player3 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 0 }, { Symbol.Blank, 0 }, });
			IPlayer player4 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });

			playerActionsTaken = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };
			playerActionsCalled = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };

			testGame = new Game { Players = new List<IPlayer> { player1, player2, player3, player4, }, AgeDecks = new List<Deck> { new Deck { Age = 1, Cards = new List<ICard> { new Card() } } } };

			var testCard = MockRepository.GenerateStub<ICard>();
			testCard.Stub(c => c.Actions).Return(new List<CardAction> { new CardAction(ActionType.Demand, Symbol.Tower, "test", TestRequiredActionHandler) });

			Dogma.Action(testCard, player1, testGame);

			Assert.AreEqual(0, playerActionsCalled[player1]);
			Assert.AreEqual(1, playerActionsCalled[player2]);
			Assert.AreEqual(1, playerActionsCalled[player3]);
			Assert.AreEqual(0, playerActionsCalled[player4]);

			Assert.AreEqual(0, playerActionsTaken[player1]);
			Assert.AreEqual(1, playerActionsTaken[player2]);
			Assert.AreEqual(1, playerActionsTaken[player3]);
			Assert.AreEqual(0, playerActionsTaken[player4]);

			Assert.AreEqual(0, player1.Hand.Count()); //test free draw action
			Assert.AreEqual(player3, targetedPlayer); //test to make sure who went last
			Assert.AreEqual(player1, activePlayer);
		}

		[TestMethod]
		public void Dogma_Action_MultipleActions()
		{
			IPlayer player1 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player2 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player3 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });
			IPlayer player4 = GeneratePlayer(1, new Dictionary<Symbol, int>() { { Symbol.Clock, 0 }, { Symbol.Crown, 0 }, { Symbol.Factory, 0 }, { Symbol.Leaf, 0 }, { Symbol.Lightbulb, 0 }, { Symbol.Tower, 1 }, { Symbol.Blank, 0 }, });

			playerActionsTaken = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };
			playerActionsCalled = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };

			testGame = new Game { Players = new List<IPlayer> { player1, player2, player3, player4, }, AgeDecks = new List<Deck> { new Deck { Age = 1, Cards = new List<ICard> { new Card() } } } };

			var testCard = MockRepository.GenerateStub<ICard>();
			testCard.Stub(c => c.Actions).Return(new List<CardAction>
			{
				new CardAction(ActionType.Required, Symbol.Tower, "test", TestRequiredActionHandler),
				new CardAction(ActionType.Required, Symbol.Tower, "test", TestOptionalActionHandler),
				
			});

			Dogma.Action(testCard, player1, testGame);

			Assert.AreEqual(2, playerActionsCalled[player1]);
			Assert.AreEqual(2, playerActionsCalled[player2]);
			Assert.AreEqual(2, playerActionsCalled[player3]);
			Assert.AreEqual(2, playerActionsCalled[player4]);

			Assert.AreEqual(2, playerActionsTaken[player1]);
			Assert.AreEqual(1, playerActionsTaken[player2]);
			Assert.AreEqual(1, playerActionsTaken[player3]);
			Assert.AreEqual(1, playerActionsTaken[player4]);

			Assert.AreEqual(1, player1.Hand.Count()); //test free draw action
			Assert.AreEqual(player1, targetedPlayer); //test to make sure the current player went last
			Assert.AreEqual(player1, activePlayer);
		}
		
	}
}
