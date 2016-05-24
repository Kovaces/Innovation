using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Interfaces;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Innovation.Tests.Helpers;

namespace Innovation.Actions.Tests
{
    [TestClass]
    public class DrawTests
    {
        private Game.Game testGame;
        
        [TestInitialize]
        public void Setup()
        {
            testGame = new Game.Game
            {
                AgeDecks = new List<Deck>
                {
                    new Deck {Age = 1, Cards = new List<ICard> {new Card {Age = 1}, new Card {Age = 1}}},
                    new Deck {Age = 2, Cards = new List<ICard>()},
                    new Deck {Age = 3, Cards = new List<ICard> {new Card {Age = 3}, new Card {Age = 3}}},
                    new Deck {Age = 4, Cards = new List<ICard>()},
                    new Deck {Age = 5, Cards = new List<ICard>()},
                    new Deck {Age = 6, Cards = new List<ICard>()},
                    new Deck {Age = 7, Cards = new List<ICard> {new Card {Age = 7}, new Card {Age = 7}}},
                    new Deck {Age = 8, Cards = new List<ICard> {new Card {Age = 8}, new Card {Age = 8}}},
                    new Deck {Age = 9, Cards = new List<ICard> {new Card {Age = 9}, new Card {Age = 9}}},
                    new Deck {Age = 10, Cards = new List<ICard>()},
                }
            };
        }

        [TestMethod]
        public void DrawAction_Base()
        {
            var drawnCard = Draw.Action(1, testGame);
            Assert.IsNotNull(drawnCard);
            Assert.AreEqual(1, drawnCard.Age);
        }

        [TestMethod]
        public void DrawAction_RequestedDeckEmpty()
        {
            var drawnCard = Draw.Action(2, testGame);
            Assert.IsNotNull(drawnCard);
            Assert.AreEqual(3, drawnCard.Age);
        }

        [TestMethod]
        public void DrawAction_MultipleDecksEmpty()
        {
            var drawnCard = Draw.Action(4, testGame);
            Assert.IsNotNull(drawnCard);
            Assert.AreEqual(7, drawnCard.Age);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException))]
        public void DrawAction_DrawAgeTen_DeckEmpty()
        {
            Draw.Action(10, testGame);
        }
    }
}
