using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Actions.Handlers;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
	public class Pottery : CardBase
	{
		public override string Name { get { return "Pottery"; } }
		public override int Age { get { return 1; } }
		public override Color Color { get { return Color.Blue; } }
		public override Symbol Top { get { return Symbol.Blank; } }
		public override Symbol Left { get { return Symbol.Leaf; } }
		public override Symbol Center { get { return Symbol.Leaf; } }
		public override Symbol Right { get { return Symbol.Leaf; } }
		public override IEnumerable<CardAction> Actions
		{
			get
			{
				return new List<CardAction>()
				{
                    new CardAction(ActionType.Optional, Symbol.Leaf, "You may return up to three cards from your hand. If you returned any cards, draw and score a card of value equal to the number of cards you returned.", Action1)
                    ,new CardAction(ActionType.Required, Symbol.Leaf, "Draw a [1].", Action2)
                };
			}
		}

		CardActionResults Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Hand.Count == 0)
				return new CardActionResults(false, false);

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				parameters.TargetPlayer.Hand,
				0, parameters.TargetPlayer.Hand.Count,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return new CardActionResults(false, true);
		}
		CardActionResults Action1_Step2(CardActionParameters parameters)
		{
			var selectedCards = parameters.Answer.MultipleCards;

			if (selectedCards.Count == 0)
				return new CardActionResults(false, false);
			if (selectedCards.Count != 3)
				throw new ArgumentNullException("Must choose three cards.");

			foreach (ICard card in selectedCards)
			{
				parameters.TargetPlayer.RemoveCardFromHand(card);
				Return.Action(card, parameters.Game);
			}

			var drawnCard = Draw.Action(selectedCards.Count, parameters.Game);
			if (drawnCard == null)
				return new CardActionResults(true, false);

			Score.Action(drawnCard, parameters.TargetPlayer);

			return new CardActionResults(true, false);
		}

		CardActionResults Action2(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			var drawnCard = Draw.Action(1, parameters.Game);
			if (drawnCard == null)
				return new CardActionResults(true, false);

			parameters.TargetPlayer.AddCardToHand(drawnCard);

			return new CardActionResults(true, false);
		}
	}
}