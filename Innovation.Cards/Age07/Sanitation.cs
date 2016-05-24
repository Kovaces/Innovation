using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Sanitation : CardBase
    {
        public override string Name => "Sanitation";
        public override int Age => 7;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Leaf;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Leaf;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you exchange the two highest cards in your hand with the lowest card in my hand!", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}