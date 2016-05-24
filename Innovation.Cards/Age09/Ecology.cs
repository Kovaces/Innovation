using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Ecology : CardBase
    {
        public override string Name => "Ecology";
        public override int Age => 9;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Leaf;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you do, score a card from your hand and draw two [10].", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}