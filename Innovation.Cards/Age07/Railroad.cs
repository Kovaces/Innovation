using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Railroad : ICard
    {
        public string Name { get { return "Railroad"; } }
        public int Age { get { return 7; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Clock; } }
        public Symbol Left { get { return Symbol.Factory; } }
        public Symbol Center { get { return Symbol.Clock; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Clock,"Return all cards from your hand, then draw three [6].", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Clock,"You may splay up any one color of your cards currently splayed right.", Action2)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
        void Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}