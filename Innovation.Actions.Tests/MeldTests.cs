using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Interfaces;


using Innovation.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Innovation.Actions.Tests
{
    [TestClass]
    public class MeldTests
    {
        private Card testCard;
        private Player testPlayer;

        [TestInitialize]
        public void Setup()
        {
            testCard = new Card { Color = Color.Blue, Age = 1, Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower };

            testPlayer = new Player
            {
                Tableau = new Tableau
                {
                    Stacks = new Dictionary<Color, Stack>
                    {
                        { 
                            Color.Blue, 
                            new Stack 
                            {
                                Cards = new List<ICard>{ new Card{ Color = Color.Blue, Age = 1, Top = Symbol.Leaf, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower} }, 
                                SplayedDirection = SplayDirection.Up
                            } 
                        }
                    }
                }
            };
        }

        [TestMethod]
        public void MeldAction_Base()
        {
            Meld.Action(testCard, testPlayer);
            Assert.AreEqual(testCard, testPlayer.Tableau.Stacks[Color.Blue].GetTopCard());
            Assert.AreEqual(6, testPlayer.Tableau.GetSymbolCount(Symbol.Tower));
            Assert.AreEqual(0, testPlayer.Tableau.GetSymbolCount(Symbol.Leaf));
            
        }
    }
}
