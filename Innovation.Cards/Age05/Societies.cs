using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Societies : CardBase
    {
        public override string Name => "Societies";
        public override int Age => 5;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Crown;
        public override Symbol Left => Symbol.Blank;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Crown;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a top non-purple card with a [BULB] from your board to my board! If you do, draw a [5]!", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}