using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class TheWheel : ICard
    {
        public string Name { get { return "The Wheel"; } }
        public int Age { get { return 1; } }
        public Color Color { get { return Color.Green; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Tower; } }
        public Symbol Center { get { return Symbol.Tower; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Tower,"Draw two [1].", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}