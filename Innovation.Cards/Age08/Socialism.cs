using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Socialism : ICard
    {
        public string Name { get { return "Socialism"; } }
        public int Age { get { return 8; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Leaf; } }
        public Symbol Left { get { return Symbol.Blank; } }
        public Symbol Center { get { return Symbol.Leaf; } }
        public Symbol Right { get { return Symbol.Leaf; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Leaf,"You may tuck all cards from your hand. If you tuck one, you must tuck them all. If you tucked at least one purple card, take all the lowest cards in each other player's hand into your hand.", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}