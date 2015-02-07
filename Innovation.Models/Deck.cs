using System.Collections.Generic;
using System.Linq;

namespace Innovation.Models
{
	public class Deck
	{
		public List<ICard> Cards { get; set; }
		public int Age { get; set; }

		public ICard Draw()
		{
			if (!Cards.Any())
				return null;

			var drawnCard = Cards[0];
			Cards.RemoveAt(0);
			
			return drawnCard;
		}
	}
}
