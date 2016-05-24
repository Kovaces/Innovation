using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;


using Innovation.Player;

namespace Innovation.Cards
{
    public class Mathematics : CardBase
    {
        public override string Name => "Mathematics";
        public override int Age => 2;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Lightbulb;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Optional, Symbol.Lightbulb, "You may return a card from your hand. If you do, draw and meld a card of value one higher than the card you returned.", Action1)
        };

        void Action1(ICardActionParameters parameters) 
        {
            

            ValidateParameters(parameters);

            if (parameters.TargetPlayer.Hand.Any())
                return;

            var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may return a card from your hand. If you do, draw and meld a card of value one higher than the card you returned.");
            if (!answer.HasValue || !answer.Value)
                return;

            var cardToReturn = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Hand, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();

            parameters.TargetPlayer.RemoveCardFromHand(cardToReturn);

            Return.Action(cardToReturn, parameters.AgeDecks);

            Meld.Action(Draw.Action(cardToReturn.Age + 1, parameters.AgeDecks), parameters.TargetPlayer);

            PlayerActed(parameters);
        }
    }
}