using System.Collections.Generic;
using Innovation.Models.Enums;

namespace Innovation.Models
{
	public class Card
	{
		public Symbol Top { get; set; }
		public Symbol Left { get; set; }
		public Symbol Center { get; set; }
		public Symbol Right { get; set; }

		public int Age { get; set; }
		public Color CardColor { get; set; }
		public List<CardAction> Actions { get; set; }
	}
}
