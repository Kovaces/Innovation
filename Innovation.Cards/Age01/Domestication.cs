using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Actions.Handlers;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Cards
{
	public class Domestication : CardBase
	{
		public override string Name { get { return "Domestication"; } }
		public override int Age { get { return 1; } }
		public override Color Color { get { return Color.Yellow; } }
		public override Symbol Top { get { return Symbol.Tower; } }
		public override Symbol Left { get { return Symbol.Crown; } }
		public override Symbol Center { get { return Symbol.Blank; } }
		public override Symbol Right { get { return Symbol.Tower; } }
		public override IEnumerable<CardAction> Actions
		{
			get
			{
				return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Tower, "Meld the lowest card in your hand. Draw a [1].", Action1)
                };
			}
		}

		CardActionResults Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			if (!parameters.TargetPlayer.Hand.Any())
				return new CardActionResults(false, false);

			var lowestAgeInHand = parameters.TargetPlayer.Hand.Min(c => c.Age);
			var lowestCards = parameters.TargetPlayer.Hand.Where(c => c.Age.Equals(lowestAgeInHand)).ToList();

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				lowestCards,
				1, 1,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return new CardActionResults(false, true);
		}
		CardActionResults Action1_Step2(CardActionParameters parameters)
		{
			ICard cardToMeld = parameters.Answer.SingleCard;
			if (cardToMeld == null)
				throw new ArgumentNullException("Must choose card.");

			parameters.TargetPlayer.RemoveCardFromHand(cardToMeld);
			Meld.Action(cardToMeld, parameters.TargetPlayer);

			var drawnCard = Draw.Action(1, parameters.Game);
			if (drawnCard == null)
				return new CardActionResults(true, false);

			parameters.TargetPlayer.AddCardToHand(drawnCard);

			return new CardActionResults(true, false);
		}
	}
}