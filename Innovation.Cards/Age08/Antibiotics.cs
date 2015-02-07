using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Anitbiotics : ICard
    {
        public string Name { get { return "Antibiotics"; } }
        public int Age { get { return 8; } }
        public Color Color { get { return Color.Yellow; } }
        public Symbol Top { get { return Symbol.Leaf; } }
        public Symbol Left { get { return Symbol.Leaf; } }
        public Symbol Center { get { return Symbol.Leaf; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Leaf,"You may return up to three cards from your hand. For every different value of card that you returned, draw two [8].", Action1)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}