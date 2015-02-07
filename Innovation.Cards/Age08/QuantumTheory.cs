using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class QuantumTheory : ICard
    {
        public string Name { get { return "Quantum Theory"; } }
        public int Age { get { return 8; } }
        public Color Color { get { return Color.Blue; } }
        public Symbol Top { get { return Symbol.Clock; } }
        public Symbol Left { get { return Symbol.Clock; } }
        public Symbol Center { get { return Symbol.Clock; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Clock,"You may return up to two cards from your hand. If you return two, draw a [10] and then draw and score a [10].", Action1)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}