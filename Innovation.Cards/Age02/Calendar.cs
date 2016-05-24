using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Calendar : CardBase
    {
        public override string Name => "Calendar";
        public override int Age => 2;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Lightbulb;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Required, Symbol.Leaf, "If you have more cards in your score pile than in your hand, draw two [3].", Action1)
        };

        void Action1(ICardActionParameters parameters)
        {
            

            ValidateParameters(parameters);

            int cardsInScorePile = parameters.TargetPlayer.Tableau.ScorePile.Count;
            int cardsInHand = parameters.TargetPlayer.Hand.Count;

            if (cardsInScorePile <= cardsInHand)
                return;
            
            parameters.TargetPlayer.AddCardToHand(Draw.Action(3, parameters.AgeDecks));
            parameters.TargetPlayer.AddCardToHand(Draw.Action(3, parameters.AgeDecks));

            PlayerActed(parameters);
        }
    }
}