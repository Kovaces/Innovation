using Innovation.Models.Enums;
using Innovation.Models.Interfaces;
using System;
using System.Collections.Generic;
using Innovation.Models;

namespace Innovation.Players
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
		public int Achievements { get; set; }

		public Action<string> UpdateClientHandler { get; set; }
        public PlayerInteraction Interaction { get; set; }
		
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
			if (UpdateClientHandler != null)
				UpdateClientHandler(Id);
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
