using System.Linq;
using System.Collections.Generic;
using Innovation.Models.Enums;
using System;

namespace Innovation.Models
{
	public class Player
	{
		public string Name { get; set; }
		public Tableau Tableau { get; set; }
		public List<ICard> Hand { get; set; }
		public string Team { get; set; } //the base rules support team play but implementing that is low on the priority list

		public bool AlwaysParticipates { get; set; }  // testing help
		public List<int> SelectsCards { get; set; }  // testing help

		public ICard PickCardFromHand()
		{
			return PickCardFromHand(Hand);
		}
		public ICard PickCardFromHand(IEnumerable<ICard> cardsToSelectFrom)
		{
			if (SelectsCards.Count > 0)
				return cardsToSelectFrom.ToList()[SelectsCards.First()];

			throw new NotImplementedException();
		}
		public List<ICard> PickMultipleCardsFromHand(IEnumerable<ICard> cardsToSelectFrom, int minimumNumberToSelect, int maximumNumberToSelect)
		{
			List<ICard> cards = new List<ICard>();
			if (SelectsCards.Count > 0)
			{
				foreach (int i in SelectsCards)
					cards.Add(cardsToSelectFrom.ToList()[i]);

				return cards;
			}

			throw new NotImplementedException();
		}
		public bool AskQuestion(string question)
		{
			return AlwaysParticipates;
		}
		public bool AskToParticipate(CardAction action)
		{
			return AlwaysParticipates;
		}

		public void RevealCard(ICard card) { }

		public bool AskToSplay(Color colorToSplay, SplayDirection directionToSplay)
		{
			if (Tableau.Stacks[colorToSplay].Cards.Count > 1
				&& Tableau.Stacks[colorToSplay].SplayedDirection == SplayDirection.None)
			{
				if (AskQuestion("Splay your " + colorToSplay + " cards " + directionToSplay + "?"))
					return true;

				// should we just splay here ?
			}

			return false;
		}
	}
}
