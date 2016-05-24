using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Enterprise : CardBase
    {
        public override string Name => "Enterprise";
        public override int Age => 4;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a top non-purple card with a [CROWN] from your board to my board! If you do, draw and meld a [4]!", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Crown,"You may splay your green cards right.", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}