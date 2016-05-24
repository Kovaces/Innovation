using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Rocketry : CardBase
    {
        public override string Name => "Rocketry";
        public override int Age => 8;
        public override Color Color => Color.Blue;
        public override Symbol Top => Symbol.Clock;
        public override Symbol Left => Symbol.Clock;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Clock,"Return a card in any other player's score pile for every two [CLOCK] on your board.", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}