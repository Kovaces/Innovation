using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Models.ExtensionMethods;

namespace Innovation.Models
{
	public class Deck
	{
		public Deck()
		{
			Cards = new List<ICard>();
		}

		public Deck(List<ICard> cards, int age)
		{
			Cards = cards;
			Age = age;
		}

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

		public void InsertAtEnd(ICard card)
		{
			if (Cards.Count > 0)
				Cards.Insert(Cards.Count, card);
			else
				Cards.Add(card);
		}

		public void Shuffle()
		{
			Cards.Shuffle(new Random());
		}
	}
}
