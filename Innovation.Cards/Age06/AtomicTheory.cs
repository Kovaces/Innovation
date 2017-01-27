using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class AtomicTheory : CardBase
    {
        public override string Name => "Atomic Theory";
        public override int Age => 6;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Lightbulb;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may splay your blue cards right.", Action1)
            ,new CardAction(ActionType.Required,Symbol.Lightbulb,"Draw and meld a [7].", Action2)
        };

        void Action1(ICardActionParameters parameters)
        {
            AskToSplay(parameters, Color.Blue, SplayDirection.Right);
        }

        void Action2(ICardActionParameters parameters)
        {
            ValidateParameters(parameters);
            PlayerActed(parameters);

            Meld.Action(Draw.Action(7, parameters.AgeDecks), parameters.TargetPlayer);
        }
    }
}