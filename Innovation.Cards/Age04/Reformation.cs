using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Reformation : CardBase
    {
        public override string Name { get { return "Reformation"; } }
        public override int Age { get { return 4; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Leaf; } }
        public override Symbol Left { get { return Symbol.Leaf; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Leaf; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Leaf,"You may tuck a card from your hand for every two [LEAF] on your board.", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Leaf,"You may splay your yellow or purple cards right.", Action2)
                };
            }
        }
        CardActionResults Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        CardActionResults Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}