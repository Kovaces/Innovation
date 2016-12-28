using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class Chemistry : CardBase
    {
        public override string Name => "Chemistry";
        public override int Age => 5;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Factory;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Factory;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your blue cards right.", Action1)
            ,new CardAction(ActionType.Required,Symbol.Factory,"Draw and score a card of value one higher than the highest top card on your board and then return a card from your score pile.", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may splay your blue cards right.");
            if (!answer.HasValue || !answer.Value)
                return;

            PlayerActed(parameters);

            parameters.TargetPlayer.SplayStack(Color.Blue, SplayDirection.Right);
        }

        void Action2(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            var ageToScore = Math.Min(parameters.TargetPlayer.Tableau.GetHighestAge() + 1, 10);

            Score.Action(Draw.Action(ageToScore, parameters.AgeDecks), parameters.TargetPlayer);

            var cardToReturn = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id,
                new PickCardParameters
                {
                    CardsToPickFrom = parameters.TargetPlayer.Tableau.ScorePile,
                    MinimumCardsToPick = 1,
                    MaximumCardsToPick = 1
                }).First();

            Return.Action(cardToReturn, parameters.AgeDecks);

            PlayerActed(parameters);
        }
    }
}