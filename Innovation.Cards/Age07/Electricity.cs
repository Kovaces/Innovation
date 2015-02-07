using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Electricity : ICard
    {
        public string Name { get { return "Electricity"; } }
        public int Age { get { return 7; } }
        public Color Color { get { return Color.Green; } }
        public Symbol Top { get { return Symbol.Lightbulb; } }
        public Symbol Left { get { return Symbol.Factory; } }
        public Symbol Center { get { return Symbol.Blank; } }
        public Symbol Right { get { return Symbol.Factory; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Factory,"Return all your top cards without a [FACTORY], then draw an [8] for each card you returned.", Action1)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}