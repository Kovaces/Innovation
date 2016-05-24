using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Innovation.Interfaces;



namespace Innovation.Tests.Helpers
{
	public class Card : ICard
	{
		public string Id => "C_" + new Regex("[^a-zA-Z0-9]").Replace(Name, "");

	    public Symbol Top { get; set; }
		public Symbol Left { get; set; }
		public Symbol Center { get; set; }
		public Symbol Right { get; set; }

		public string Name { get; set; }
		public int Age { get; set; }
		public Color Color { get; set; }
		public IEnumerable<CardAction> Actions { get; set; }

		public bool HasSymbol(Symbol symbol)
		{
			return ((Top == symbol) || (Left == symbol) || (Center == symbol) || (Right == symbol));
		}
	}
}
