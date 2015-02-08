using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Services : ICard
    {
        public string Name { get { return "Services"; } }
        public int Age { get { return 9; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Leaf; } }
        public Symbol Center { get { return Symbol.Leaf; } }
        public Symbol Right { get { return Symbol.Leaf; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you transfer all the highest cards from your score pile to my hand! If you transferred any cards, then transfer a top card from my board without a [LEAF] to your hand!", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}