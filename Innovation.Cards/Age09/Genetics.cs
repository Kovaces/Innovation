using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Genetics : ICard
    {
        public string Name { get { return "Genetics"; } }
        public int Age { get { return 9; } }
        public Color Color { get { return Color.Blue; } }
        public Symbol Top { get { return Symbol.Lightbulb; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Lightbulb; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Lightbulb,"Draw and meld a [10]. Score all cards beneath it.", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}