using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Computers : CardBase
    {
        public override string Name => "Computers";
        public override int Age => 9;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Clock;
        public override Symbol Left => Symbol.Blank;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Clock,"You may splay your red cards or your green cards up.", Action1)
            ,new CardAction(ActionType.Required,Symbol.Clock,"Draw and meld a [10], then execute each of its non-demand dogma effects. Do not share them.", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}