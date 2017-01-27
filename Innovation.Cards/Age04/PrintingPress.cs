using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Interfaces;
using Innovation.Player;


namespace Innovation.Cards
{
    public class PrintingPress : CardBase
    {
        public override string Name => "Printing Press";
        public override int Age => 4;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your score pile. If you do, draw a card of value two higher than the top purple card on your board.", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may splay your blue cards right.", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            if (!parameters.TargetPlayer.Tableau.ScorePile.Any())
                return;

            //You may return a card from your score pile.
            var returnedCard = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id,
                                                                                new PickCardParameters
                                                                                {
                                                                                    CardsToPickFrom = parameters.TargetPlayer.Tableau.ScorePile,
                                                                                    MinimumCardsToPick = 1,
                                                                                    MaximumCardsToPick = 1
                                                                                }).FirstOrDefault();

            if (returnedCard == null)
                return;
            
            PlayerActed(parameters);

            parameters.TargetPlayer.RemoveCardFromScorePile(returnedCard);
            Return.Action(returnedCard, parameters.AgeDecks);

            //If you do, draw a card of value two higher than the top purple card on your board.
            var purpleAge = parameters.TargetPlayer.Tableau.GetTopCards().FirstOrDefault(c => c.Color == Color.Purple)?.Age ?? 2;

            parameters.TargetPlayer.AddCardToHand(Draw.Action(purpleAge, parameters.AgeDecks));
        }

        void Action2(ICardActionParameters parameters)
        {
            AskToSplay(parameters, Color.Blue, SplayDirection.Right);
        }
    }
}