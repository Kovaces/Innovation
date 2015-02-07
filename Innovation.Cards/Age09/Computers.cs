using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Computers : ICard
    {
        public string Name { get { return "Computers"; } }
        public int Age { get { return 9; } }
        public Color Color { get { return Color.Blue; } }
        public Symbol Top { get { return Symbol.Clock; } }
        public Symbol Left { get { return Symbol.Blank; } }
        public Symbol Center { get { return Symbol.Clock; } }
        public Symbol Right { get { return Symbol.Factory; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Clock,"You may splay your red cards or your green cards up.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Clock,"Draw and meld a [10], then execute each of its non-demand dogma effects. Do not share them.", Action2)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
        void Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}