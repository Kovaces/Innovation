using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Satellites : CardBase
    {
        public override string Name => "Satellites";
        public override int Age => 9;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Clock;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Clock;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Clock,"Return all cards from your hand, and draw three [8].", Action1)
            ,new CardAction(ActionType.Optional,Symbol.Clock,"You may splay your purple cards up.", Action2)
            ,new CardAction(ActionType.Required,Symbol.Clock,"Meld a card from your hand and then execute each of its non-demand dogma effects. Do not share them", Action3)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action3(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}