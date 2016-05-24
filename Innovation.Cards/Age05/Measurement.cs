using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Measurement : CardBase
    {
        public override string Name => "Measurement";
        public override int Age => 5;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Lightbulb;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you do, splay any one color of your cards right, and draw a card of value equal to the number of cards of that color on your board.", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}