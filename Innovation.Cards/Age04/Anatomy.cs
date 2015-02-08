using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Anatomy : ICard
    {
        public string Name { get { return "Anatomy"; } }
        public int Age { get { return 4; } }
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
                    new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you return a card from your score pile! If you do, return a top card of equal value from your board!", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}