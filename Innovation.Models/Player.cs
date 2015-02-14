using System.Linq;
using System.Collections.Generic;
using Innovation.Models.Enums;
using System;
using Innovation.Models.Interfaces;

namespace Innovation.Models
{
	public class Player : IPlayer
	{
		public string Name { get; set; }
		public ITableau Tableau { get; set; }
		public List<ICard> Hand { get; set; }
		public string Team { get; set; } //the base rules support team play but implementing that is low on the priority list

		public ICard PickCardFromHand()
		{
			return PickCard(Hand);
		}

		public ICard PickCard(IEnumerable<ICard> cardsToSelectFrom)
		{
			if (!cardsToSelectFrom.Any())
				return null;

			throw new NotImplementedException();
		}

		public IEnumerable<ICard> PickMultipleCards(IEnumerable<ICard> cardsToSelectFrom, int minimumNumberToSelect, int maximumNumberToSelect)
		{
			if (!cardsToSelectFrom.Any())
				return null;

			throw new NotImplementedException();
		}

		public void RevealCard(ICard card)
		{
			//not sure this should go here as it involves showing other players the card info
			//and as such will involve the game object
			throw new NotImplementedException();
		}

		public bool AskToSplay(Color colorToSplay, SplayDirection directionToSplay)
		{
			if (Tableau.Stacks[colorToSplay].Cards.Count <= 1)
				return false;

			return AskQuestion("Splay your " + colorToSplay + " cards " + directionToSplay + "?");
		}

		public bool AskQuestion(string question)
		{
			throw new NotImplementedException();
		}

		public IPlayer PickPlayer(List<IPlayer> playerList)
		{
			throw new NotImplementedException();
		}
	}
}
