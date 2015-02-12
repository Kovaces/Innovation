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
		public Player AlwaysPicksPlayer { get; set; }  // testing help

		public ICard PickCardFromHand()
		{
			return PickFromMultipleCards(Hand, 1, 1).First();
		}
		public List<ICard> PickFromMultipleCards(IEnumerable<ICard> cardsToSelectFrom, int minimumNumberToSelect, int maximumNumberToSelect)
		{
			List<ICard> cards = new List<ICard>();
			if (SelectsCards.Count > 0)
				foreach (int i in SelectsCards)
					cards.Add(cardsToSelectFrom.ToList()[i]);

			return cards;
		}
		public bool AskQuestion(string question)
		{
			return AlwaysParticipates;
		}
		public bool AskToParticipate(CardAction action)
		{
			return AlwaysParticipates;
		}
		public Player PickPlayer(Game game)
		{
			return AlwaysPicksPlayer;
		}

		public void RevealCard(ICard card) { }

		public bool AskToSplay(Color colorToSplay, SplayDirection directionToSplay)
		{
			if (Tableau.Stacks[colorToSplay].Cards.Count > 1)
			{
				if (AskQuestion("Splay your " + colorToSplay + " cards " + directionToSplay + "?"))
					return true;
			}

			return false;
		}
	}
}
