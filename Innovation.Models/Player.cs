
using System.Collections.Generic;
using Innovation.Models.Enums;

namespace Innovation.Models
{
	public class Player
	{
		public string Name { get; set; }
		public Tableau Tableau { get; set; }
		public List<ICard> Hand { get; set; }
		public string Team { get; set; } //the base rules support team play but implementing that is low on the priority list

		public ICard PickCardFromHand()
		{
			return PickCardFromHand(Hand);
		}
		public ICard PickCardFromHand(IEnumerable<ICard> cardsToSelectFrom)
		{
			throw new System.NotImplementedException();
		}
		public List<ICard> PickMultipleCardsFromHand(IEnumerable<ICard> cardsToSelectFrom, int minimumNumberToSelect, int maximumNumberToSelect)
		{
			throw new System.NotImplementedException();
		}
		public bool AskQuestion(string question)
		{
			return true;
		}
		public bool AskToParticipate(CardAction action)
		{
			return true;  // participation is fun!
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
