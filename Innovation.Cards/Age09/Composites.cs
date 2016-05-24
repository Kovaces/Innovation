using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Composites : CardBase
    {
        public override string Name => "Composites";
        public override int Age => 9;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Factory;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Factory,"I demand you transfer all but one card from your hand to my hand! Also, transfer the highest card from your score pile to my score pile!", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}