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
	public delegate void StartTurn(string playerId);

	public class Player : IPlayer
	{
		public Player()
		{
			Tableau = new Tableau();
			Hand = new List<ICard>();
		}

		public string Id { get; set; }
		public string Name { get; set; }
		public ITableau Tableau { get; set; }
		public List<ICard> Hand { get; set; }
		public string Team { get; set; } //the base rules support team play but implementing that is low on the priority list
		public int ActionsTaken { get; set; }

		public RevealCard RevealCardHandler { get; set; }
		public void RevealCard(ICard card)
		{
			RevealCardHandler(Id, card);
		}
		
		public PickColorToSplayOutgoing PickColorToSplayHandler { get; set; }
		public void AskToSplay(IEnumerable<Color> colorsToSplay, SplayDirection directionToSplay)
		{
			if (!colorsToSplay.Any())
				return;

			PickColorToSplayHandler(Id, colorsToSplay, directionToSplay);
		}
		
		public PickMultipleCardsOutgoing PickMultipleCardsHandler { get; set; }
		public void PickCard(IEnumerable<ICard> cardsToSelectFrom)
		{
			PickMultipleCards(cardsToSelectFrom, 1, 1);
		}
		public void PickCardFromHand()
		{
			PickMultipleCards(Hand, 1, 1);
		}
		public void PickMultipleCards(IEnumerable<ICard> cardsToSelectFrom, int minimumNumberToSelect, int maximumNumberToSelect)
		{
			if (!cardsToSelectFrom.Any())
				return;

			PickMultipleCardsHandler(Id, cardsToSelectFrom, minimumNumberToSelect, maximumNumberToSelect);
		}
		
		public AskQuestionOutgoing AskQuestionHandler { get; set; }
		public void AskQuestion(string question)
		{
			AskQuestionHandler(Id, question);
		}
		
		public PickPlayersOutgoing PickPlayersHandler { get; set; }
		public void PickPlayer(List<IPlayer> playerList, int minimumNumberToSelect, int maximumNumberToSelect)
		{
			if (playerList.Count == 0)
				return;

			PickPlayersHandler(Id, playerList, minimumNumberToSelect, maximumNumberToSelect);
		}

		public StartTurn StartTurnHandler { get; set; }
		public void StartTurn()
		{
			ActionsTaken = 0;
			StartTurnHandler(Id);
		}
	}
}
