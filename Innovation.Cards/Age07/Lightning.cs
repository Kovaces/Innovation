using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Cards
{
    public class Lightning : CardBase, ICard
    {
        public override string Name { get { return "Lightning"; } }
        public override int Age { get { return 7; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Leaf; } }
        public override Symbol Center { get { return Symbol.Clock; } }
        public override Symbol Right { get { return Symbol.Leaf; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Leaf,"You may tuck up to three cards from your hand. If you do, draw and score a [7] for every different value of card you tucked.", Action1)
                };
            }
        }
        void Action1(ICardActionParameters input) { throw new NotImplementedException(); }
    }
}