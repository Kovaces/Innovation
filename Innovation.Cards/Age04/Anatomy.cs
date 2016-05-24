using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Anatomy : CardBase
    {
        public override string Name => "Anatomy";
        public override int Age => 4;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Leaf;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you return a card from your score pile! If you do, return a top card of equal value from your board!", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}