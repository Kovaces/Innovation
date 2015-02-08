using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Ecology : ICard
    {
        public string Name { get { return "Ecology"; } }
        public int Age { get { return 9; } }
        public Color Color { get { return Color.Yellow; } }
        public Symbol Top { get { return Symbol.Leaf; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Lightbulb; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you do, score a card from your hand and draw two [10].", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}