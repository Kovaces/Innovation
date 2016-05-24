using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Bicycle : CardBase
    {
        public override string Name => "Bicycle";
        public override int Age => 7;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Crown;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Crown,"You may exchange all cards in your hand with all the cards in your score pile. If you exchange one, you must exchange them all.", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}