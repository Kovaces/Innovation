using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Refridgeration : CardBase
    {
        public override string Name => "Refridgeration";
        public override int Age => 7;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you return half (rounded down) of the cards in your hand!", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Leaf,"You may score a card from your hand.", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}