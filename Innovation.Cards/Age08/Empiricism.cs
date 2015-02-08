using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Empiricism : ICard
    {
        public string Name { get { return "Empiricism"; } }
        public int Age { get { return 8; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Lightbulb; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Lightbulb; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Lightbulb,"Choose two colors, then draw and reveal a [9]. If it is either of the colors you chose, meld it and you may splay your cards of that color up.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Lightbulb,"If you have twenty or more [BULB] on your board, you win.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}