using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class TheInternet : ICard
    {
        public string Name { get { return "The Internet"; } }
        public int Age { get { return 10; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Clock; } }
        public Symbol Center { get { return Symbol.Clock; } }
        public Symbol Right { get { return Symbol.Lightbulb; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Clock,"You may splay your green cards up.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Clock,"Draw and score a [10].", Action2)
                    ,new CardAction(ActionType.Required,Symbol.Clock,"Draw and meld a [10] for every two [CLOCK] on your board.", Action3)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
        bool Action3(object[] parameters) { throw new NotImplementedException(); }
    }
}