using Innovation.Actions;

using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;

using Innovation.Player;

namespace Innovation.Cards
{
    public class Agriculture : CardBase
    {
        public override string Name => "Agriculture";
        public override int Age => 1;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Leaf;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>() 
        {
            new CardAction(ActionType.Optional, Symbol.Leaf, "You may return a card from your hand. If you do, draw and score a card of value one higher than the card you returned.", Action)
        };

        private void Action(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            if (!parameters.TargetPlayer.Hand.Any())
                return;

            var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may return a card from your hand. If you do, draw and score a card of value one higher than the card you returned.");

            if (!answer.HasValue || !answer.Value)
                return;

            var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Hand, MinimumCardsToPick = 1, MaximumCardsToPick = 1 }).First();
            
            parameters.TargetPlayer.RemoveCardFromHand(selectedCard);
            
            Return.Action(selectedCard, parameters.AgeDecks);

            Score.Action(Draw.Action(selectedCard.Age + 1, parameters.AgeDecks), parameters.TargetPlayer);

            PlayerActed(parameters);
        }
    }
}