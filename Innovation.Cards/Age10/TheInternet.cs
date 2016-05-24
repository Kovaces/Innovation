using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class TheInternet : CardBase
    {
        public override string Name => "The Internet";
        public override int Age => 10;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Clock;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Lightbulb;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Clock,"You may splay your green cards up.", Action1)
            ,new CardAction(ActionType.Required,Symbol.Clock,"Draw and score a [10].", Action2)
            ,new CardAction(ActionType.Required,Symbol.Clock,"Draw and meld a [10] for every two [CLOCK] on your board.", Action3)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action3(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}