using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Physics : CardBase
    {
        public override string Name => "Physics";
        public override int Age => 5;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Factory;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Lightbulb,"Draw three [6] and reveal them. If two or more of the drawn cards are the same color, return the drawn cards and all the cards in your hand. Otherwise, keep them.", Action1)
        };

        void Action1(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);

            //Draw three [6] and reveal them.
            var c1 = DrawAndReveal(parameters, 6);
            var c2 = DrawAndReveal(parameters, 6);
            var c3 = DrawAndReveal(parameters, 6);

            //If two or more of the drawn cards are the same color, return the drawn cards and all the cards in your hand
            if (c1.Color == c2.Color ||
                c1.Color == c3.Color ||
                c2.Color == c3.Color)
            {
                Return.Action(c1, parameters.AgeDecks);
                Return.Action(c2, parameters.AgeDecks);
                Return.Action(c3, parameters.AgeDecks);

                parameters.TargetPlayer.Hand.ForEach(c => Return.Action(c, parameters.AgeDecks));
            }

            //Otherwise, keep them.
            parameters.TargetPlayer.AddCardToHand(c1);
            parameters.TargetPlayer.AddCardToHand(c2);
            parameters.TargetPlayer.AddCardToHand(c3);

            PlayerActed(parameters);
        }
    }
}