using System;
using System.Collections;
using System.Collections.Generic;
using Innovation.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Innovation.Tests.Helpers;


namespace Innovation.Models.Tests
{
	[TestClass]
	public class StackTests
	{
		private Stack testStack;

		[TestInitialize]
		public void Setup()
		{
			testStack = new Stack
			{
				SplayedDirection = SplayDirection.None,
				Cards = new List<ICard> //cards when played are added to end of list with .Add method therefore the last card in the list is the card on top
				{
					new Card {Top = Symbol.Leaf,  Left = Symbol.Leaf,  Center = Symbol.Blank, Right = Symbol.Crown },
					new Card {Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Crown },
					new Card {Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower },
				}
			};
		}

		[TestMethod]
		public void Stack_GetSymbolCount_WhenStackHasZeroCards()
		{
			var emptyStack = new Stack { SplayedDirection = SplayDirection.None, Cards =  new List<ICard>()};

			Assert.AreEqual(0, emptyStack.GetSymbolCount(Symbol.Blank));
			Assert.AreEqual(0, emptyStack.GetSymbolCount(Symbol.Clock));
			Assert.AreEqual(0, emptyStack.GetSymbolCount(Symbol.Crown));
			Assert.AreEqual(0, emptyStack.GetSymbolCount(Symbol.Factory));
			Assert.AreEqual(0, emptyStack.GetSymbolCount(Symbol.Leaf));
			Assert.AreEqual(0, emptyStack.GetSymbolCount(Symbol.Lightbulb));
			Assert.AreEqual(0, emptyStack.GetSymbolCount(Symbol.Tower));
		}

		[TestMethod]
		public void Stack_GetSymbolCount_WhenStackIsNotSplayed()
		{
			

			Assert.AreEqual(3, testStack.GetSymbolCount(Symbol.Tower));
			Assert.AreEqual(0, testStack.GetSymbolCount(Symbol.Crown));
		}

		[TestMethod]
		public void Stack_GetSymbolCount_WhenStackIsSplayedLeft()
		{
			testStack.SplayedDirection = SplayDirection.Left;
			
			Assert.AreEqual(3, testStack.GetSymbolCount(Symbol.Tower));
			Assert.AreEqual(2, testStack.GetSymbolCount(Symbol.Crown));
			Assert.AreEqual(0, testStack.GetSymbolCount(Symbol.Leaf));
		}

		[TestMethod]
		public void Stack_GetSymbolCount_WhenStackIsSplayedRight()
		{
			testStack.SplayedDirection = SplayDirection.Right;
			
			Assert.AreEqual(3, testStack.GetSymbolCount(Symbol.Tower));
			Assert.AreEqual(1, testStack.GetSymbolCount(Symbol.Crown));
			Assert.AreEqual(2, testStack.GetSymbolCount(Symbol.Leaf));
		}

		[TestMethod]
		public void Stack_GetSymbolCount_WhenStackIsSplayedUp()
		{
			testStack.SplayedDirection = SplayDirection.Up;
			
			Assert.AreEqual(4, testStack.GetSymbolCount(Symbol.Tower));
			Assert.AreEqual(3, testStack.GetSymbolCount(Symbol.Crown));
			Assert.AreEqual(1, testStack.GetSymbolCount(Symbol.Leaf));
		}
	}
}
