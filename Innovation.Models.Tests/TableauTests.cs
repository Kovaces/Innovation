using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models.Enums;
using Innovation.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Innovation.Models.Tests
{
    [TestClass]
    public class TableauTests
    {
        private Tableau testTableau;

        [TestInitialize]
        public void Setup()
        {
            var blueStack = new Stack
            {
                SplayedDirection = SplayDirection.None,
                Cards = new List<ICard> //cards when played are added to end of list with .Add method therefore the last card in the list is the card on top
                {
                    new Card {Top = Symbol.Leaf,  Left = Symbol.Leaf,  Center = Symbol.Blank, Right = Symbol.Crown },
                    new Card {Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Crown },
                    new Card {Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower, Age = 1 },
                }
            };

            var greenStack = new Stack
            {
                SplayedDirection = SplayDirection.None,
                Cards = new List<ICard> //cards when played are added to end of list with .Add method therefore the last card in the list is the card on top
                {
                    new Card {Top = Symbol.Leaf,  Left = Symbol.Leaf,  Center = Symbol.Blank, Right = Symbol.Crown },
                    new Card {Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Crown },
                    new Card {Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower, Age = 2 },
                }
            };

            var purpleStack = new Stack
            {
                SplayedDirection = SplayDirection.None,
                Cards = new List<ICard> //cards when played are added to end of list with .Add method therefore the last card in the list is the card on top
                {
                    new Card {Top = Symbol.Leaf,  Left = Symbol.Leaf,  Center = Symbol.Blank, Right = Symbol.Crown },
                    new Card {Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Crown },
                    new Card {Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower, Age = 3 },
                }
            };

            var redStack = new Stack
            {
                SplayedDirection = SplayDirection.None,
                Cards = new List<ICard> //cards when played are added to end of list with .Add method therefore the last card in the list is the card on top
                {
                    new Card {Top = Symbol.Leaf,  Left = Symbol.Leaf,  Center = Symbol.Blank, Right = Symbol.Crown },
                    new Card {Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Crown },
                    new Card {Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower, Age = 4 },
                }
            };

            var yellowStack = new Stack
            {
                SplayedDirection = SplayDirection.None,
                Cards = new List<ICard> //cards when played are added to end of list with .Add method therefore the last card in the list is the card on top
                {
                    new Card {Top = Symbol.Leaf,  Left = Symbol.Leaf,  Center = Symbol.Blank, Right = Symbol.Crown },
                    new Card {Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Crown },
                    new Card {Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower, Age = 5 },
                }
            };

            testTableau = new Tableau
            {
                NumberOfAchievements = 0,
                ScorePile = new List<ICard>
                {
                    new Card { Age = 1 },
                    new Card { Age = 2 },
                    new Card { Age = 3 },
                },
                Stacks = new Dictionary<Color, Stack>
                {
                    {Color.Blue, blueStack},
                    {Color.Green, greenStack},
                    {Color.Purple, purpleStack},
                    {Color.Red, redStack},
                    {Color.Yellow, yellowStack},

                },
            };
        }

        [TestMethod]
        public void Tableau_WhenTableauHasZeroStacks()
        {
            var emptyTableau = new Tableau();
            Assert.AreEqual(1,emptyTableau.GetHighestAge());
            Assert.AreEqual(0, emptyTableau.GetScore());
            
            Assert.AreEqual(0, emptyTableau.GetSymbolCount(Symbol.Blank));
            Assert.AreEqual(0, emptyTableau.GetSymbolCount(Symbol.Clock));
            Assert.AreEqual(0, emptyTableau.GetSymbolCount(Symbol.Crown));
            Assert.AreEqual(0, emptyTableau.GetSymbolCount(Symbol.Factory));
            Assert.AreEqual(0, emptyTableau.GetSymbolCount(Symbol.Leaf));
            Assert.AreEqual(0, emptyTableau.GetSymbolCount(Symbol.Lightbulb));
            Assert.AreEqual(0, emptyTableau.GetSymbolCount(Symbol.Tower));
        }

        [TestMethod]
        public void Tableau_GetHighestAge()
        {
            Assert.AreEqual(5, testTableau.GetHighestAge());
        }

        [TestMethod]
        public void Tableau_GetScore()
        {
            Assert.AreEqual(6, testTableau.GetScore());
        }
    }
}
