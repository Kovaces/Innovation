using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Perspective : CardBase
    {
        public override string Name => "Perspective";
        public override int Age => 4;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Leaf;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you do, score a card from your hand for every two [BULB] on your board.", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}