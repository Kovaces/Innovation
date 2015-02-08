using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Education : ICard
    {
        public string Name { get { return "Education"; } }
        public int Age { get { return 3; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Lightbulb; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Lightbulb; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return the highest card from your score pile. If you do, draw a card of value two higher than the highest card remaining in your score pile.", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}