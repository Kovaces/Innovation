
using Innovation.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Innovation.Interfaces;


namespace Innovation.Actions.Tests
{
    [TestClass]
    public class AchieveTests
    {
        private Deck testAgeAchievementDeck;
        private Player.Player testPlayer;

        [TestInitialize]
        public void Setup()
        {
            testAgeAchievementDeck = new Deck();

            testPlayer = new Player.Player
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
            testAgeAchievementDeck.Cards.Add(new Card { Age = 1 });
            Assert.IsTrue(Achieve.Action(testPlayer, testAgeAchievementDeck));
        }

        [TestMethod]
        public void AchieveAction_PlayerHasScoreButNotTopCard()
        {
            testAgeAchievementDeck.Cards.Add(new Card { Age = 3 });
            Assert.IsFalse(Achieve.Action(testPlayer, testAgeAchievementDeck));
        }

        [TestMethod]
        public void AchieveAction_PlayerHasTopCardButNotScore()
        {
            testPlayer.Tableau.ScorePile = new List<ICard>
            {
                new Card {Age = 1},
            };

            testAgeAchievementDeck.Cards.Add(new Card { Age = 2 });
            Assert.IsFalse(Achieve.Action(testPlayer, testAgeAchievementDeck));
        }
    }
}
