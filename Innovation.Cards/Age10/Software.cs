using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Software : ICard
    {
        public string Name { get { return "Software"; } }
        public int Age { get { return 10; } }
        public Color Color { get { return Color.Blue; } }
        public Symbol Top { get { return Symbol.Clock; } }
        public Symbol Left { get { return Symbol.Clock; } }
        public Symbol Center { get { return Symbol.Clock; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Clock,"Draw and score a [10].", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Clock,"Draw and meld two [10], then execute each of the second card's non-demand dogma effects. Do not share them.", Action2)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
        void Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}