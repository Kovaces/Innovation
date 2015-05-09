using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Cards
{
    public class Machinery : CardBase
    {
        public override string Name { get { return "Machinery"; } }
        public override int Age { get { return 3; } }
        public override Color Color { get { return Color.Yellow; } }
        public override Symbol Top { get { return Symbol.Leaf; } }
        public override Symbol Left { get { return Symbol.Leaf; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you exchange all the cards in your hand with all the highest cards in my hand!", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Leaf,"Score a card from your hand with a [TOWER]. You may splay your red cards left.", Action2)
                };
            }
        }
        void Action1(ICardActionParameters input) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters input) { throw new NotImplementedException(); }
    }
}