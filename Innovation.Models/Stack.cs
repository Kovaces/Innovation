using System.Linq;
using Innovation.Models.Enums;
using System.Collections.Generic;

namespace Innovation.Models
{
	public class Stack
	{
		public Stack()
		{
			Cards = new List<ICard>();
			SplayedDirection = SplayDirection.None;
		}

		public List<ICard> Cards { get; set; } //TODO: make this private and create methods with validations for adding cards to the stack to ensure consistant color
		public SplayDirection SplayedDirection { get; set; }

		public ICard GetTopCard()
		{
			return Cards.Any() ? Cards.Last() : null;
		}

		// put implementation in here so if we change it later, no other classes need to know details
		public void AddCardToTop(ICard card)
		{
			Cards.Add(card);
		}
		public void RemoveCard(ICard card)
		{
			Cards.Remove(card);

			if (Cards.Count <= 1)
				SplayedDirection = SplayDirection.None;
		}
		public void AddCardToBottom(ICard card)
		{
			List<ICard> tempList = new List<ICard>();
			tempList.Add(card);
			tempList.AddRange(Cards);
			Cards = tempList;
		}

		public int GetSymbolCount(Symbol symbol)
		{
			return GetSymbolCounts()[symbol];
		}

		public Dictionary<Symbol, int> GetSymbolCounts()
		{
			var retVal = new Dictionary<Symbol, int>()
			{
				{ Symbol.Blank, 0 },
				{ Symbol.Clock, 0 },
				{ Symbol.Crown, 0 },
				{ Symbol.Factory, 0 },
				{ Symbol.Leaf, 0 },
				{ Symbol.Lightbulb, 0 },
				{ Symbol.Tower, 0 },
			};

			Cards.ForEach(c => CountSymbols(retVal, c));
			
			return retVal;
		}
		
		private void CountSymbols(Dictionary<Symbol, int> retVal, ICard card)
		{
			if (Cards.Last() == card)
			{
				retVal[card.Top] = retVal[card.Top] + 1;
				retVal[card.Left] = retVal[card.Left] + 1;
				retVal[card.Center] = retVal[card.Center] + 1;
				retVal[card.Right] = retVal[card.Right] + 1;
			}
			else
			{
				switch (SplayedDirection)
				{
					case SplayDirection.Left:
						retVal[card.Right] = retVal[card.Right] + 1;
						break;
					case SplayDirection.Right:
						retVal[card.Top] = retVal[card.Top] + 1;
						retVal[card.Left] = retVal[card.Left] + 1;
						break;
					case SplayDirection.Up:
						retVal[card.Left] = retVal[card.Left] + 1;
						retVal[card.Center] = retVal[card.Center] + 1;
						retVal[card.Right] = retVal[card.Right] + 1;
						break;
					case SplayDirection.None:
						break;
				}
			}
			
		}

		public void Splay(SplayDirection direction)
		{
			if (Cards.Count > 1)
				SplayedDirection = direction;
		}
	}
}
