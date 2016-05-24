using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Gunpowder : CardBase
    {
        public override string Name => "Gunpowder";
        public override int Age => 4;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Crown;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Factory,"I demand you transfer a top card with a [TOWER] from your board to my score pile!", Action1)
            ,new CardAction(ActionType.Required,Symbol.Factory,"If any card was transferred due to the demand, draw and score a [2].", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}