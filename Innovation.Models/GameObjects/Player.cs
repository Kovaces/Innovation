using System.Linq;
using System.Collections.Generic;
using Innovation.Models.Enums;
using System;
using System.Threading;
using Innovation.Models.Interfaces;
using Newtonsoft.Json;

namespace Innovation.Models
{
	public class Player : IPlayer
	{
		//ctor
		public Player()
		{
			Tableau = new Tableau();
			Hand = new List<ICard>();
		}

		//properties
		public string Id { get; set; }
		public string Name { get; set; }
		public ITableau Tableau { get; set; }
		public List<ICard> Hand { get; set; }
		public string Team { get; set; } //the base rules support team play but implementing that is low on the priority list
		public int ActionsTaken { get; set; }

		//Handler properties
		[JsonIgnore]
		public RevealCard RevealCardHandler { get; set; }
		[JsonIgnore]
		public PickColorToSplayOutgoing PickColorToSplayHandler { get; set; }
		[JsonIgnore]
		public PickMultipleCardsOutgoing PickMultipleCardsHandler { get; set; }
		[JsonIgnore]
		public AskQuestionOutgoing AskQuestionHandler { get; set; }
		[JsonIgnore]
		public PickPlayersOutgoing PickPlayersHandler { get; set; }
		[JsonIgnore]
		public StartTurn StartTurnHandler { get; set; }
		[JsonIgnore]
		public UpdateClient UpdateClientHandler { get; set; }

		//methods
		public void RevealCard(ICard card)
		{
			RevealCardHandler(Id, card);
		}
		public void AskToSplay(IEnumerable<Color> colorsToSplay, SplayDirection directionToSplay)
		{
			if (!colorsToSplay.Any())
				return;

			PickColorToSplayHandler(Id, colorsToSplay, directionToSplay);
		}
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
		public void AskQuestion(string question)
		{
			AskQuestionHandler(Id, question);
		}
		public void PickPlayer(List<IPlayer> playerList, int minimumNumberToSelect, int maximumNumberToSelect)
		{
			if (playerList.Count == 0)
				return;

			PickPlayersHandler(Id, playerList, minimumNumberToSelect, maximumNumberToSelect);
		}
		public void StartTurn()
		{
			ActionsTaken = 0;
			StartTurnHandler(Id);
		}

		public void AddCardToHand(ICard card)
		{
			Hand.Add(card);
			if (UpdateClientHandler != null)
				UpdateClientHandler(Id);
		}
		public void RemoveCardFromHand(ICard card)
		{
			Hand.Remove(card);
			if (UpdateClientHandler != null)
				UpdateClientHandler(Id);
		}
		public void AddCardToStack(ICard card)
		{
			Tableau.Stacks[card.Color].AddCardToTop(card);
			if (UpdateClientHandler != null)
				UpdateClientHandler(Id);
		}
		public void RemoveCardFromStack(ICard card)
		{
			Tableau.Stacks[card.Color].RemoveCard(card);
		}
		public void TuckCard(ICard card)
		{
			Tableau.Stacks[card.Color].AddCardToBottom(card);
			if (UpdateClientHandler != null)
				UpdateClientHandler(Id);
		}
		public void SplayStack(Color stackColor, SplayDirection direction)
		{
			Tableau.Stacks[stackColor].Splay(direction);
			if (UpdateClientHandler != null)
				UpdateClientHandler(Id);
		}
		public void AddCardToScorePile(ICard card)
		{
			Tableau.ScorePile.Add(card);
			if (UpdateClientHandler != null)
				UpdateClientHandler(Id);
		}
		public void RemoveCardFromScorePile(ICard card)
		{
			Tableau.ScorePile.Remove(card);
			if (UpdateClientHandler != null)
				UpdateClientHandler(Id);
		}
	}
}
