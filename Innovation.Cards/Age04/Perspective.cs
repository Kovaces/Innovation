using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Perspective : ICard
    {
        public string Name { get { return "Perspective"; } }
        public int Age { get { return 4; } }
        public Color Color { get { return Color.Yellow; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Lightbulb; } }
        public Symbol Right { get { return Symbol.Leaf; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you do, score a card from your hand for every two [BULB] on your board.", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}