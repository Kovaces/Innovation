using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Sailing : ICard
    {
        public string Name { get { return "Sailing"; } }
        public int Age { get { return 1; } }
        public Color Color { get { return Color.Green; } }
        public Symbol Top { get { return Symbol.Crown; } }
        public Symbol Left { get { return Symbol.Crown; } }
        public Symbol Center { get { return Symbol.Blank; } }
        public Symbol Right { get { return Symbol.Leaf; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Crown,"Draw and meld a [1].", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}