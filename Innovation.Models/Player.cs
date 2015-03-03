using System.Linq;
using System.Collections.Generic;
using Innovation.Models.Enums;
using System;
using System.Threading;
using Innovation.Models.Interfaces;

namespace Innovation.Models
{
	public delegate void RevealCard(string playerId, ICard card);
	public delegate void PickCardOutgoing(string playerId, IEnumerable<ICard> cardsToSelectFrom);
	public delegate void PickMultipleCardsOutgoing(string playerId, IEnumerable<ICard> cardsToSelectFrom, int minimumNumberToSelect, int maximumNumberToSelect);
	public delegate void AskQuestionOutgoing(string playerId, string question);
	public delegate void PickPlayerOutgoing(string playerId, List<IPlayer> playerList);

	public class Player : IPlayer
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public ITableau Tableau { get; set; }
		public List<ICard> Hand { get; set; }
		public string Team { get; set; } //the base rules support team play but implementing that is low on the priority list

		public RevealCard RevealCardHandler { get; set; }
		public void RevealCard(ICard card)
		{
			RevealCardHandler(Id, card);
		}

		public bool AskToSplay(Color colorToSplay, SplayDirection directionToSplay)
		{
			if (Tableau.Stacks[colorToSplay].Cards.Count <= 1)
				return false;

			return AskQuestion("Splay your " + colorToSplay + " cards " + directionToSplay + "?");
		}

		private ICard _selectedCard;
		public PickCardOutgoing PickCardHandler { get; set; }
		public ICard PickCardFromHand()
		{
			return PickCard(Hand);
		}
		public ICard PickCard(IEnumerable<ICard> cardsToSelectFrom)
		{
			if (!cardsToSelectFrom.Any())
				return null;

			_selectedCard = null;
			PickCardHandler(Id, cardsToSelectFrom);

			while (_selectedCard == null)
				Thread.Sleep(5000);

			return _selectedCard;
		}
		public void PickCardResponse(ICard cardSelected)
		{
			_selectedCard = cardSelected;
		}

		private IEnumerable<ICard> _cardsSelected;
		public PickMultipleCardsOutgoing PickMultipleCardsHandler { get; set; }
		public IEnumerable<ICard> PickMultipleCards(IEnumerable<ICard> cardsToSelectFrom, int minimumNumberToSelect, int maximumNumberToSelect)
		{
			if (!cardsToSelectFrom.Any())
				return null;

			_cardsSelected = null;
			PickMultipleCardsHandler(Id, cardsToSelectFrom, minimumNumberToSelect, maximumNumberToSelect);

			while (_cardsSelected == null)
				Thread.Sleep(5000);

			return _cardsSelected;
		}
		public void PickMultipleCardsResponse(IEnumerable<ICard> cardsSelected)
		{
			_cardsSelected = cardsSelected;
		}

		private bool? _answer;
		public AskQuestionOutgoing AskQuestionHandler { get; set; }
		public bool AskQuestion(string question)
		{
			_answer = null;
			AskQuestionHandler(Id, question);

			while (!_answer.HasValue)
				Thread.Sleep(5000);

			return _answer.Value;
		}
		public void AskQuestionResponse(bool answer)
		{
			_answer = answer;
		}

		private IPlayer _selectedPlayer;
		public PickPlayerOutgoing PickPlayerHandler { get; set; }
		public IPlayer PickPlayer(List<IPlayer> playerList)
		{
			_selectedPlayer = null;
			PickPlayerHandler(Id, playerList);

			while (_selectedPlayer == null)
				Thread.Sleep(5000);

			return _selectedPlayer;
		}
		public void PickPlayerResponse(IPlayer playerSelected)
		{
			_selectedPlayer = playerSelected;
		}
	}
}
