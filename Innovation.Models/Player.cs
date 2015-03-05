using System.Linq;
using System.Collections.Generic;
using Innovation.Models.Enums;
using System;
using System.Threading;
using Innovation.Models.Interfaces;

namespace Innovation.Models
{
	public delegate void RevealCard(string playerId, ICard card);
	public delegate void PickColorToSplayOutgoing(string playerId, IEnumerable<Color> colorsToSplay, SplayDirection splayDirection);
	public delegate void PickMultipleCardsOutgoing(string playerId, IEnumerable<ICard> cardsToSelectFrom, int minimumNumberToSelect, int maximumNumberToSelect);
	public delegate void AskQuestionOutgoing(string playerId, string question);
	public delegate void PickPlayersOutgoing(string playerId, IEnumerable<IPlayer> playerList, int minimumNumberToSelect, int maximumNumberToSelect);

	public class Player : IPlayer
	{
		private const int THREAD_SLEEP_MS = 1000;

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



		private Color _colorSelected;
		private bool _waitingForColor;
		public PickColorToSplayOutgoing PickColorToSplayHandler { get; set; }
		public Color AskToSplay(IEnumerable<Color> colorsToSplay, SplayDirection directionToSplay)
		{
			if (!colorsToSplay.Any())
				return Color.None;

			_colorSelected = Color.None;
			PickColorToSplayHandler(Id, colorsToSplay, directionToSplay);

			while (_waitingForColor)
				Thread.Sleep(THREAD_SLEEP_MS);

			return _colorSelected;
		}
		public void PickColorToSplayResponse(Color colorSelected)
		{
			_colorSelected = colorSelected;
			_waitingForColor = false;
		}



		public ICard PickCard(IEnumerable<ICard> cardsToSelectFrom)
		{
			return PickMultipleCards(cardsToSelectFrom, 1, 1).First();
		}

		private IEnumerable<ICard> _cardsSelected;
		public PickMultipleCardsOutgoing PickMultipleCardsHandler { get; set; }
		public ICard PickCardFromHand()
		{
			return PickMultipleCards(Hand, 1, 1).First();
		}
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
		public PickPlayersOutgoing PickPlayersHandler { get; set; }
		public IPlayer PickPlayer(List<IPlayer> playerList)
		{
			_selectedPlayer = null;
			PickPlayersHandler(Id, playerList, 1, 1);

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
