using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Combustion : CardBase
    {
        public override string Name => "Combustion";
        public override int Age => 7;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Crown;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Factory;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer two cards from your score pile to my score pile!", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}