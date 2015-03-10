using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Actions.Handlers;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Mathematics : CardBase
    {
        public override string Name { get { return "Mathematics"; } }
        public override int Age { get { return 2; } }
        public override Color Color { get { return Color.Blue; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Lightbulb; } }
        public override Symbol Center { get { return Symbol.Crown; } }
        public override Symbol Right { get { return Symbol.Lightbulb; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may return a card from your hand. If you do, draw and meld a card of value one higher than the card you returned.", Action1)
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
				1, 1,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return new CardActionResults(false, true);
		}

		CardActionResults Action1_Step2(CardActionParameters parameters) 
		{
			var card = parameters.Answer.SingleCard;

			if (card == null)
				return new CardActionResults(false, false);

			parameters.TargetPlayer.Hand.Remove(card);
			
			Return.Action(card, parameters.Game);
			
			Meld.Action(Draw.Action(card.Age + 1, parameters.Game), parameters.TargetPlayer);

			return new CardActionResults(true, false);
		}
    }
}