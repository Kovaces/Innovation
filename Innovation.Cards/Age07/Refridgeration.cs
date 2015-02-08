using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Refridgeration : ICard
    {
        public string Name { get { return "Refridgeration"; } }
        public int Age { get { return 7; } }
        public Color Color { get { return Color.Yellow; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Leaf; } }
        public Symbol Center { get { return Symbol.Leaf; } }
        public Symbol Right { get { return Symbol.Crown; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you return half (rounded down) of the cards in your hand!", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Leaf,"You may score a card from your hand.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}