using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Fermenting : ICard
    {
        public string Name { get { return "Fermenting"; } }
        public int Age { get { return 2; } }
        public Color Color { get { return Color.Yellow; } }
        public Symbol Top { get { return Symbol.Leaf; } }
        public Symbol Left { get { return Symbol.Leaf; } }
        public Symbol Center { get { return Symbol.Blank; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Leaf,"Draw a [2] for every two [LEAF] icons on your board.", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}