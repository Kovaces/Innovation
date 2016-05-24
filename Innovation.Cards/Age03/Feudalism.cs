using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Feudalism : CardBase
    {
        public override string Name => "Feudalism";
        public override int Age => 3;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Tower;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Tower,"I demand you transfer a card with a [TOWER] from your hand to my hand!", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Tower,"You may splay your yellow or purple cards left.", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}