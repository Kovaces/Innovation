using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Alchemy : ICard
    {
        public string Name { get { return "Alchemy"; } }
        public int Age { get { return 3; } }
        public Color Color { get { return Color.Blue; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Leaf; } }
        public Symbol Center { get { return Symbol.Tower; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Tower,"Draw and reveal a [4] for every three [TOWER] on your board. If any of the drawn cards are red, return the cards drawn and return all cards in your hand. Otherwise, keep them.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Tower,"Meld a card from your hand, then score a card from your hand.", Action2)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
        void Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}