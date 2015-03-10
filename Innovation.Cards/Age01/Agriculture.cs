using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Actions.Handlers;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Cards
{
	public class Agriculture : CardBase
	{
		public override string Name { get { return "Agriculture"; } }
		public override int Age { get { return 1; } }
		public override Color Color { get { return Color.Yellow; } }
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
                    new CardAction(ActionType.Optional, Symbol.Leaf, "You may return a card from your hand. If you do, draw and score a card of value one higher than the card you returned.", Action1)
                };
			}
		}

		private CardActionResults Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				parameters.TargetPlayer.Hand,
				1, 1,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return new CardActionResults(false, true);
		}

		private CardActionResults Action1_Step2(CardActionParameters parameters)
		{
			ICard selectedCard = parameters.Answer.SingleCard;
			if (selectedCard == null)
				return new CardActionResults(false, false);

			parameters.TargetPlayer.Hand.Remove(selectedCard);
			Return.Action(selectedCard, parameters.Game);

			int ageToDraw = selectedCard.Age + 1;
			var cardToScore = Draw.Action(ageToDraw, parameters.Game);
			if (cardToScore == null)
				return new CardActionResults(true, false);

			Score.Action(cardToScore, parameters.TargetPlayer);

			return new CardActionResults(true, false);
		}
	}
}