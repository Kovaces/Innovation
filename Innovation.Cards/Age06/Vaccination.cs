using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Vaccination : CardBase
    {
        public override string Name => "Vaccination";
        public override int Age => 6;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Leaf;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you return all the lowest cards in your score pile! If you returned any, draw and meld a [6]!", Action1)
            ,new CardAction(ActionType.Required,Symbol.Leaf,"If any card was returned as a result of the demand, draw and meld a [7].", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}