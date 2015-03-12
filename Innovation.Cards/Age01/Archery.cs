using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Actions.Handlers;
using Innovation.Models;
using Innovation.Models.Enums;
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
		public override IEnumerable<CardAction> Actions
		{
			get
			{
				return new List<CardAction>
				{
                    new CardAction(ActionType.Demand, Symbol.Tower, "I demand you draw a [1], then transfer the highest card in your hand to my hand!", Action1)
                };
			}
		}
		CardActionResults Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			var drawnCard = Draw.Action(1, parameters.Game);
			if (drawnCard == null)
				return new CardActionResults(true, false);

			parameters.TargetPlayer.AddCardToHand(drawnCard);

			var highestAgeInHand = parameters.TargetPlayer.Hand.Max(c => c.Age);
			var highestCards = parameters.TargetPlayer.Hand.Where(c => c.Age.Equals(highestAgeInHand)).ToList();

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				highestCards,
				1, 1,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return new CardActionResults(true, true);
		}

		CardActionResults Action1_Step2(CardActionParameters parameters) 
		{
			ICard selectedCard = parameters.Answer.SingleCard;
			if (selectedCard == null)
				throw new ArgumentNullException("Must choose card.");

			parameters.TargetPlayer.RemoveCardFromHand(selectedCard);
			parameters.ActivePlayer.AddCardToHand(selectedCard);

			ActionQueueManager.PopNextAction(parameters.Game);
			return new CardActionResults(true, false);
		}
	}
}