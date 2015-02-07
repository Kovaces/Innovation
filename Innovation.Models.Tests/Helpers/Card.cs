using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models.Enums;

namespace Innovation.Models.Tests.Helpers
{
	public class Card : ICard
	{
		public Symbol Top { get; set; }
		public Symbol Left { get; set; }
		public Symbol Center { get; set; }
		public Symbol Right { get; set; }

		public int Age { get; set; }
		public Color Color { get; set; }
		public IEnumerable<CardAction> Actions { get; set; }
	}
}
