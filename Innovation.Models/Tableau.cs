using Innovation.Models.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Innovation.Models
{
	public class Tableau
	{
		public Tableau()
		{
			NumberOfAchievements = 0;
			ScorePile = new List<ICard>();
			Stacks = new Dictionary<Color, Stack>
			{
				{Color.Blue, new Stack()},
				{Color.Green, new Stack()},
				{Color.Purple, new Stack()},
				{Color.Red, new Stack()},
				{Color.Yellow, new Stack()},
			};
		}

		public Dictionary<Color, Stack> Stacks { get; set; }
		public int NumberOfAchievements { get; set; }
		public List<ICard> ScorePile { get; set; }

		public int GetHighestAge()
		{
			return Stacks.Max(s => (s.Value.GetTopCard() != null ? s.Value.GetTopCard().Age : 1));
		}

		public int GetScore()
		{
			return ScorePile.Sum(c => c.Age);
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

			foreach (var stack in Stacks)
			{
				retVal[Symbol.Blank]	 += stack.Value.GetSymbolCount(Symbol.Blank);
				retVal[Symbol.Clock]	 += stack.Value.GetSymbolCount(Symbol.Clock);
				retVal[Symbol.Crown]	 += stack.Value.GetSymbolCount(Symbol.Crown);
				retVal[Symbol.Factory]	 += stack.Value.GetSymbolCount(Symbol.Factory);
				retVal[Symbol.Leaf]		 += stack.Value.GetSymbolCount(Symbol.Leaf);
				retVal[Symbol.Lightbulb] += stack.Value.GetSymbolCount(Symbol.Lightbulb);
				retVal[Symbol.Tower]	 += stack.Value.GetSymbolCount(Symbol.Tower);
			}
			
			return retVal;
		}
	}
}
