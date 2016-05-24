using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Anitbiotics : CardBase
    {
        public override string Name => "Antibiotics";
        public override int Age => 8;
        public override Color Color => Color.Yellow;
        public override Symbol Top => Symbol.Leaf;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Leaf;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Leaf,"You may return up to three cards from your hand. For every different value of card that you returned, draw two [8].", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}