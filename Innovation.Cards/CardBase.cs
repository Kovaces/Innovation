using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Innovation.Players;

namespace Innovation.Cards
{
	public abstract class CardBase : ICard
	{
		public string Id { get { return "C_" + new Regex("[^a-zA-Z0-9]").Replace(Name, ""); } }

		public abstract Symbol Top { get; }
		public abstract Symbol Left { get; }
		public abstract Symbol Center { get; }
		public abstract Symbol Right { get; }

		public abstract string Name { get; }
		public abstract int Age { get; }
		public abstract Color Color { get; }
		public abstract IEnumerable<ICardAction> Actions { get; }

		public bool HasSymbol(Symbol symbol)
		{
			return ((Top == symbol) || (Left == symbol) || (Center == symbol) || (Right == symbol));
		}

		//Protected Properties and Methods
		protected void ValidateParameters(CardActionParameters parameters)
		{
			if (parameters == null)
				throw new ArgumentNullException("parameters");

			if (parameters.TargetPlayer == null)
				throw new ArgumentOutOfRangeException("parameters", "Target player cannot be null");
			
			if (parameters.ActivePlayer == null)
				throw new ArgumentOutOfRangeException("parameters", "Active player cannot be null");

			if (parameters.AgeDecks == null)
				throw new ArgumentOutOfRangeException("parameters", "Age Decks cannot be null");
		}

		protected ICard DrawAndReveal(CardActionParameters parameters, int age)
		{
			var drawnCard = Draw.Action(age, parameters.AgeDecks);

			((Player)parameters.TargetPlayer).Interaction.RevealCard(parameters.TargetPlayer.Id, drawnCard);

			return drawnCard;
		}

		protected void PlayerActed(CardActionParameters parameters)
		{
			if (parameters.TargetPlayer != parameters.ActivePlayer)
				parameters.AddToStorage(ContextStorage.AnotherPlayerTookDogmaActionKey, true);
		}
	}
}
