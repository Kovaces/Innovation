using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class QuantumTheory : CardBase
    {
        public override string Name { get { return "Quantum Theory"; } }
        public override int Age { get { return 8; } }
        public override Color Color { get { return Color.Blue; } }
        public override Symbol Top { get { return Symbol.Clock; } }
        public override Symbol Left { get { return Symbol.Clock; } }
        public override Symbol Center { get { return Symbol.Clock; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Clock,"You may return up to two cards from your hand. If you return two, draw a [10] and then draw and score a [10].", Action1)
                };
            }
        }
        CardActionResults Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}