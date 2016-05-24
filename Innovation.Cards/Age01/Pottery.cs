using Innovation.Actions;
using System.Collections.Generic;
using System.Linq;
using Innovation.Interfaces;


using Innovation.Player;

namespace Innovation.Cards
{
    public class Pottery : CardBase
    {
        public override string Name => "Pottery";
        public override int Age => 1;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Leaf;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Optional, Symbol.Leaf, "You may return up to three cards from your hand. If you returned any cards, draw and score a card of value equal to the number of cards you returned.", Action1)
            ,new CardAction(ActionType.Required, Symbol.Leaf, "Draw a [1].", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            

            ValidateParameters(parameters);

            if (parameters.TargetPlayer.Hand.Count == 0)
                return;

            var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may return up to three cards from your hand. If you returned any cards, draw and score a card of value equal to the number of cards you returned.");

            if (!answer.HasValue || !answer.Value)
                return;

            var selectedCards = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Hand, MinimumCardsToPick = 1, MaximumCardsToPick = 3 }).ToList();

            foreach (var card in selectedCards)
            {
                parameters.TargetPlayer.RemoveCardFromHand(card);
                Return.Action(card, parameters.AgeDecks);
            }

            Score.Action(Draw.Action(selectedCards.Count, parameters.AgeDecks), parameters.TargetPlayer);
            
            PlayerActed(parameters);
        }


        void Action2(ICardActionParameters parameters)
        {
            

            ValidateParameters(parameters);

            parameters.TargetPlayer.AddCardToHand(Draw.Action(1, parameters.AgeDecks));

            PlayerActed(parameters);
        }
    }
}