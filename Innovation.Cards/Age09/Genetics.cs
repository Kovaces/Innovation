using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Genetics : CardBase
    {
        public override string Name => "Genetics";
        public override int Age => 9;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Lightbulb;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Lightbulb,"Draw and meld a [10]. Score all cards beneath it.", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}