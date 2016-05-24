using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Empiricism : CardBase
    {
        public override string Name => "Empiricism";
        public override int Age => 8;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Lightbulb;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Lightbulb,"Choose two colors, then draw and reveal a [9]. If it is either of the colors you chose, meld it and you may splay your cards of that color up.", Action1)
            ,new CardAction(ActionType.Required,Symbol.Lightbulb,"If you have twenty or more [BULB] on your board, you win.", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}