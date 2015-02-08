
using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Archery : ICard
    {
        public string Name { get { return "Archery"; } }
        public int Age { get { return 1; } }
        public Color Color { get { return Color.Red; } }
        public Symbol Top { get { return Symbol.Tower; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Blank; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Tower,"I demand you draw a [1], then transfer the highest card in your hand to my hand!", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}