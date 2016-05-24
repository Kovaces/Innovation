using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Medicine : CardBase
    {
        public override string Name => "Medicine";
        public override int Age => 3;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Crown;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you exchange the highest card in your score pile with the lowest card in my score pile!", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}