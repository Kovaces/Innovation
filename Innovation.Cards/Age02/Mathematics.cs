using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models.Interfaces;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Players;

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
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may return a card from your hand. If you do, draw and meld a card of value one higher than the card you returned.", Action1)
                };
            }
        }
        void Action1(ICardActionParameters input) 
		{
			var parameters = input as CardActionParameters;

			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Hand.Any())
				return;

			var answer = ((Player)parameters.TargetPlayer).Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may return a card from your hand. If you do, draw and meld a card of value one higher than the card you returned.");
			if (!answer.HasValue || !answer.Value)
				return;

			var cardToReturn = ((Player)parameters.TargetPlayer).Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Hand, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();

			parameters.TargetPlayer.RemoveCardFromHand(cardToReturn);

			Return.Action(cardToReturn, parameters.AgeDecks);

			Meld.Action(Draw.Action(cardToReturn.Age + 1, parameters.AgeDecks), parameters.TargetPlayer);

			PlayerActed(parameters);
		}
    }
}