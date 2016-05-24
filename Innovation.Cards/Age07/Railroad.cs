using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Railroad : CardBase
    {
        public override string Name => "Railroad";
        public override int Age => 7;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Clock;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Clock,"Return all cards from your hand, then draw three [6].", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Clock,"You may splay up any one color of your cards currently splayed right.", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}