using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Lightning : CardBase, ICard
    {
        public override string Name => "Lightning";
        public override int Age => 7;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Leaf;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Leaf;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Leaf,"You may tuck up to three cards from your hand. If you do, draw and score a [7] for every different value of card you tucked.", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}