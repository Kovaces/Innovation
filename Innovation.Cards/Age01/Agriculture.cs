using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Cards
{
    public class Agriculture : ICard
    {
        public string Name { get { return "Agriculture"; } }
        public int Age { get { return 1; } }
        public Color Color { get { return Color.Yellow; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Leaf; } }
        public Symbol Center { get { return Symbol.Leaf; } }
        public Symbol Right { get { return Symbol.Leaf; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Leaf,"You may return a card from your hand. If you do, draw and score a card of value one higher than the card you returned.", Action1)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}