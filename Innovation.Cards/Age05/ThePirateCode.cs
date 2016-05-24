using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class ThePirateCode : CardBase
    {
        public override string Name => "The Pirate Code";
        public override int Age => 5;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Crown;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer two cards of value [4] or less from your score pile to my score pile!", Action1)
            ,new CardAction(ActionType.Required,Symbol.Crown,"If any card was transferred due to the demand, score the lowest top card with a [CROWN] from your board.", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}