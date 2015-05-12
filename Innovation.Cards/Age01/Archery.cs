﻿using Innovation.Actions;
using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;


using Innovation.Player;

namespace Innovation.Cards
{
	public class Archery : CardBase
	{
		public override string Name { get { return "Archery"; } }
		public override int Age { get { return 1; } }
		public override Color Color { get { return Color.Red; } }
		public override Symbol Top { get { return Symbol.Tower; } }
		public override Symbol Left { get { return Symbol.Lightbulb; } }
		public override Symbol Center { get { return Symbol.Blank; } }
		public override Symbol Right { get { return Symbol.Tower; } }
		public override IEnumerable<ICardAction> Actions
		{
			get
			{
				return new List<CardAction>
				{
                    new CardAction(ActionType.Demand, Symbol.Tower, "I demand you draw a [1], then transfer the highest card in your hand to my hand!", Action1)
                };
			}
		}
		void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			parameters.TargetPlayer.AddCardToHand(Draw.Action(1, parameters.AgeDecks));

			var highestAgeInHand = parameters.TargetPlayer.Hand.Max(c => c.Age);
			var highestCards = parameters.TargetPlayer.Hand.Where(c => c.Age.Equals(highestAgeInHand)).ToList();

			var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = highestCards, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();

			parameters.TargetPlayer.RemoveCardFromHand(selectedCard);
			parameters.ActivePlayer.AddCardToHand(selectedCard);	
		}
	}
}