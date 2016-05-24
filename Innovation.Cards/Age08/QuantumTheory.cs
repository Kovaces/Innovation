using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class QuantumTheory : CardBase
    {
        public override string Name => "Quantum Theory";
        public override int Age => 8;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Clock;
        public override Symbol Left => Symbol.Clock;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Clock,"You may return up to two cards from your hand. If you return two, draw a [10] and then draw and score a [10].", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}