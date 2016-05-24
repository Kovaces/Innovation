using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Banking : CardBase
    {
        public override string Name => "Banking";
        public override int Age => 5;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Factory;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a top non-green card with a [FACTORY] from your board to my board. If you do, draw and score a [5]!", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Crown,"You may splay your green cards right.", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}