using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class Education : CardBase
    {
        public override string Name => "Education";
        public override int Age => 3;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Lightbulb;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return the highest card from your score pile. If you do, draw a card of value two higher than the highest card remaining in your score pile.", Action1)
        };


        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may return the highest card from your score pile. If you do, draw a card of value two higher than the highest card remaining in your score pile.");
            if (!answer.HasValue || !answer.Value)
                return;

            var highestCards = parameters.TargetPlayer.Tableau.ScorePile.Where(c => c.Age == parameters.TargetPlayer.Tableau.ScorePile.Max(a => a.Age)).ToList();

            if (highestCards.Any())
            {
                var selectedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters {CardsToPickFrom = highestCards, MinimumCardsToPick = 1, MaximumCardsToPick = 1}).First();
                parameters.TargetPlayer.RemoveCardFromScorePile(selectedCard);
                Return.Action(selectedCard, parameters.AgeDecks);
            }

            var highestRemainingAge = parameters.TargetPlayer.Tableau.ScorePile.Any() ? parameters.TargetPlayer.Tableau.ScorePile.Max(a => a.Age) : 0;
            parameters.TargetPlayer.AddCardToHand(Draw.Action(highestRemainingAge + 2, parameters.AgeDecks));

            PlayerActed(parameters);
        }
    }
}