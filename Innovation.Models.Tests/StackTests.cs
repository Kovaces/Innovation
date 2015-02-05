using System;
using System.Collections;
using System.Collections.Generic;
using Innovation.Models.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Innovation.Models.Tests
{
	[TestClass]
	public class StackTests
	{
		[TestMethod]
		public void Stack_WhenStackHasZeroCards()
		{
			var testStack = new Stack { SplayedDirection = SplayDirection.None, Cards =  new List<Card>()};
			
			Assert.AreEqual(0, testStack.GetSymbolCount(Symbol.Blank));
			Assert.AreEqual(0, testStack.GetSymbolCount(Symbol.Clock));
			Assert.AreEqual(0, testStack.GetSymbolCount(Symbol.Crown));
			Assert.AreEqual(0, testStack.GetSymbolCount(Symbol.Factory));
			Assert.AreEqual(0, testStack.GetSymbolCount(Symbol.Leaf));
			Assert.AreEqual(0, testStack.GetSymbolCount(Symbol.Lightbulb));
			Assert.AreEqual(0, testStack.GetSymbolCount(Symbol.Tower));
		}

		[TestMethod]
		public void Stack_WhenStackIsNotSplayed()
		{
			var testStack = new Stack
			{
				SplayedDirection = SplayDirection.None, 
				Cards = new List<Card> //cards when played are added to end of list with .Add method therefore the last card in the list is the card on top
				{
					new Card {Top = Symbol.Leaf,  Left = Symbol.Leaf,  Center = Symbol.Blank, Right = Symbol.Crown },
					new Card {Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Crown },
					new Card {Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower },
				}
			};

			Assert.AreEqual(3, testStack.GetSymbolCount(Symbol.Tower));
			Assert.AreEqual(0, testStack.GetSymbolCount(Symbol.Crown));
		}

		[TestMethod]
		public void Stack_WhenStackIsSplayedLeft()
		{
			var testStack = new Stack
			{
				SplayedDirection = SplayDirection.Left,
				Cards = new List<Card>
				{
					new Card {Top = Symbol.Leaf,  Left = Symbol.Leaf,  Center = Symbol.Blank, Right = Symbol.Crown },
					new Card {Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Crown },
					new Card {Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower },
				}
			};

			Assert.AreEqual(3, testStack.GetSymbolCount(Symbol.Tower));
			Assert.AreEqual(2, testStack.GetSymbolCount(Symbol.Crown));
			Assert.AreEqual(0, testStack.GetSymbolCount(Symbol.Leaf));
		}

		[TestMethod]
		public void Stack_WhenStackIsSplayedRight()
		{
			var testStack = new Stack
			{
				SplayedDirection = SplayDirection.Right,
				Cards = new List<Card>
				{
					new Card {Top = Symbol.Leaf,  Left = Symbol.Leaf,  Center = Symbol.Blank, Right = Symbol.Crown },
					new Card {Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Crown },
					new Card {Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower },
				}
			};

			Assert.AreEqual(3, testStack.GetSymbolCount(Symbol.Tower));
			Assert.AreEqual(1, testStack.GetSymbolCount(Symbol.Crown));
			Assert.AreEqual(2, testStack.GetSymbolCount(Symbol.Leaf));
		}

		[TestMethod]
		public void Stack_WhenStackIsSplayedUp()
		{
			var testStack = new Stack
			{
				SplayedDirection = SplayDirection.Up,
				Cards = new List<Card>
				{
					new Card {Top = Symbol.Leaf,  Left = Symbol.Leaf,  Center = Symbol.Blank, Right = Symbol.Crown },
					new Card {Top = Symbol.Blank, Left = Symbol.Crown, Center = Symbol.Tower, Right = Symbol.Crown },
					new Card {Top = Symbol.Blank, Left = Symbol.Tower, Center = Symbol.Tower, Right = Symbol.Tower },
				}
			};

			Assert.AreEqual(4, testStack.GetSymbolCount(Symbol.Tower));
			Assert.AreEqual(3, testStack.GetSymbolCount(Symbol.Crown));
			Assert.AreEqual(1, testStack.GetSymbolCount(Symbol.Leaf));
		}
	}
}
