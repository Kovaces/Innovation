using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Fission : CardBase
    {
        public override string Name => "Fission";
        public override int Age => 9;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Clock;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Clock;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Demand,Symbol.Clock,"I demand you draw a [10]! If it is red, remove all hands, boards, and score piles from the game! If this occurs, the dogma action is complete.", Action1)
            ,new CardAction(ActionType.Required,Symbol.Clock,"Return a top card other than Fission from any player's board.", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}