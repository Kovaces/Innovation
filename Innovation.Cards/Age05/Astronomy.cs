using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Astronomy : ICard
    {
        public string Name { get { return "Astronomy"; } }
        public int Age { get { return 5; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Crown; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Lightbulb; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Lightbulb,"Draw and reveal a [6]. If the card is green or blue, meld it and repeat this dogma effect.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Lightbulb,"If all the non-purple top cards on your board are value [6] or higher, claim the Universe achievement.", Action2)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
        void Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}