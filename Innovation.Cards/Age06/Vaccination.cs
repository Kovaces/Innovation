using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Vaccination : ICard
    {
        public string Name { get { return "Vaccination"; } }
        public int Age { get { return 6; } }
        public Color Color { get { return Color.Yellow; } }
        public Symbol Top { get { return Symbol.Leaf; } }
        public Symbol Left { get { return Symbol.Factory; } }
        public Symbol Center { get { return Symbol.Leaf; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you return all the lowest cards in your score pile! If you returned any, draw and meld a [6]!", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Leaf,"If any card was returned as a result of the demand, draw and meld a [7].", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}