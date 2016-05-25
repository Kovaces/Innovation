using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Cards;
using Innovation.GameObjects;
using Innovation.Interfaces;


using Innovation.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Innovation.Actions.Tests
{
    [TestClass]
    public class DogmaTests
    {
        private Game.Game testGame;

        private Dictionary<IPlayer, int> playerActionsTaken;
        private Dictionary<IPlayer, int> playerActionsCalled;

        private IPlayer targetedPlayer;
        private IPlayer activePlayer;

        private bool takeAction;
    
        private void TestRequiredActionHandler(ICardActionParameters parameters)
        {
            targetedPlayer = parameters.TargetPlayer;
            activePlayer = parameters.ActivePlayer;

            playerActionsCalled[targetedPlayer]++;
            playerActionsTaken[targetedPlayer]++;
        }

        private void TestOptionalActionHandler(ICardActionParameters parameters)
        {
            targetedPlayer = parameters.TargetPlayer;
            activePlayer = parameters.ActivePlayer;

            playerActionsCalled[targetedPlayer]++;
            
            if ((targetedPlayer == activePlayer) || takeAction)
                playerActionsTaken[targetedPlayer]++;
        }

        private Player.Player GeneratePlayer(int highestAge, Dictionary<Symbol, int> symbolCounts)
        {
            var testTableau = MockRepository.GenerateStub<ITableau>();
            testTableau.Stub(t => t.GetSymbolCounts()).Return(symbolCounts);
            testTableau.Stub(t => t.GetHighestAge()).Return(highestAge);

            var player = new Player.Player();
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
            Player.Player player1 = GeneratePlayer(1, new Dictionary<Symbol, int>(){{Symbol.Clock, 0},{Symbol.Crown, 0},{Symbol.Factory, 0},{Symbol.Leaf, 0},{Symbol.Lightbulb, 0},{Symbol.Tower, 1},{Symbol.Blank, 0},});
            Player.Player player2 = GeneratePlayer(1, new Dictionary<Symbol, int>(){{Symbol.Clock, 0},{Symbol.Crown, 0},{Symbol.Factory, 0},{Symbol.Leaf, 0},{Symbol.Lightbulb, 0},{Symbol.Tower, 0},{Symbol.Blank, 0},});
            Player.Player player3 = GeneratePlayer(1, new Dictionary<Symbol, int>(){{Symbol.Clock, 0},{Symbol.Crown, 0},{Symbol.Factory, 0},{Symbol.Leaf, 0},{Symbol.Lightbulb, 0},{Symbol.Tower, 0},{Symbol.Blank, 0},});
            Player.Player player4 = GeneratePlayer(1, new Dictionary<Symbol, int>(){{Symbol.Clock, 0},{Symbol.Crown, 0},{Symbol.Factory, 0},{Symbol.Leaf, 0},{Symbol.Lightbulb, 0},{Symbol.Tower, 0},{Symbol.Blank, 0},});

            playerActionsTaken = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };
            playerActionsCalled = new Dictionary<IPlayer, int> { { player1, 0 }, { player2, 0 }, { player3, 0 }, { player4, 0 }, };

            testGame = new Game.Game
            {
                Players = new List<Player.Player> {player1, player2, player3, player4,},
                AgeDecks = new List<Deck> {new Deck {Age = 1, Cards = new List<ICard> {new Card()}}}
            };
            
            var testCard = MockRepository.GenerateStub<ICard>();
            testCard.Stub(c => c.Actions).Return(new List<CardAction> { new CardAction(ActionType.Required, Symbol.Tower, "test", TestRequiredActionHandler) });

            Dogma.Action(testCard, new CardActionParameters
            {
                TargetPlayer = null,
                ActivePlayer = player1,
                AgeDecks = testGame.AgeDecks,
                Players = testGame.GetPlayersInPlayerOrder(testGame.Players.IndexOf(player1)),
                AddToStorage = (s, o) => { ; },
                GetFromStorage = s => false
            });

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
            
        }

        [TestMethod]
        public void Dogma_Action_OtherPlayersQualify_OptionalAction_ActionNotTaken()
        {
            
        }

        [TestMethod]
        public void Dogma_Action_OtherPlayersQualify_OptionalAction()
        {
            
        }

        [TestMethod]
        public void Dogma_Action_DemandAction_NoneEligable()
        {
            
        }

        [TestMethod]
        public void Dogma_Action_DemandAction_SomeEligable()
        {
            
        }

        [TestMethod]
        public void Dogma_Action_MultipleActions()
        {
            
        }
        
    }
}
