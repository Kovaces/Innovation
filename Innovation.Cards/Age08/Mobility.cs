using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Mobility : CardBase
    {
        public override string Name => "Mobility";
        public override int Age => 8;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Factory,"I demand you transfer your two highest non-red top cards without a [FACTORY] from your board to my score pile! If you transferred any cards, draw an [8]!", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}