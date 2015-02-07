using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Currency : ICard
    {
        public string Name { get { return "Currency"; } }
        public int Age { get { return 2; } }
        public Color Color { get { return Color.Green; } }
        public Symbol Top { get { return Symbol.Leaf; } }
        public Symbol Left { get { return Symbol.Crown; } }
        public Symbol Center { get { return Symbol.Blank; } }
        public Symbol Right { get { return Symbol.Crown; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Crown,"You may return any number of cards from your hand. If you do, draw and score a [2] for every different value card you returned.", Action1)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}