using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class Coal : CardBase
    {
        public override string Name => "Coal";
        public override int Age => 5;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Factory;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Factory;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Factory,"Draw and tuck a [5].", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your red cards right.", Action2)
            ,new CardAction(ActionType.Optional,Symbol.Factory,"You may score any one of your top cards. If you do, also score the card beneath it.", Action3)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            Tuck.Action(Draw.Action(5, parameters.AgeDecks), parameters.TargetPlayer);

            PlayerActed(parameters);
        }

        void Action2(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may splay your red cards right.");
            if (!answer.HasValue || !answer.Value)
                return;

            PlayerActed(parameters);

            parameters.TargetPlayer.SplayStack(Color.Red, SplayDirection.Right);
        }

        void Action3(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            var cardToScore = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters
                                                                                                        {
                                                                                                            CardsToPickFrom = parameters.TargetPlayer.Tableau.GetTopCards(),
                                                                                                            MinimumCardsToPick = 0,
                                                                                                            MaximumCardsToPick = 1
                                                                                                        }).FirstOrDefault();

            if (cardToScore == null)
                return;
            
            PlayerActed(parameters);

            parameters.TargetPlayer.RemoveCardFromStack(cardToScore);
            Score.Action(cardToScore, parameters.TargetPlayer);

            var cardBeneath = parameters.TargetPlayer.Tableau.GetTopCards().FirstOrDefault(c => c.Color == cardToScore.Color);
            if (cardBeneath != null)
            {
                parameters.TargetPlayer.RemoveCardFromStack(cardBeneath);
                Score.Action(cardBeneath, parameters.TargetPlayer);
            }
        }
    }
}