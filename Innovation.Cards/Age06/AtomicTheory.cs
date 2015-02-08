using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class AtomicTheory : ICard
    {
        public string Name { get { return "Atomic Theory"; } }
        public int Age { get { return 6; } }
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
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may splay your blue cards right.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Lightbulb,"Draw and meld a [7].", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}