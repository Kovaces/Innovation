using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class MassMedia : CardBase
    {
        public override string Name => "Mass Media";
        public override int Age => 8;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Lightbulb;
        public override Symbol Left => Symbol.Blank;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Lightbulb;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you do, choose a value, and return all cards of that value from all score piles.", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may splay your purple cards up.", Action2)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}