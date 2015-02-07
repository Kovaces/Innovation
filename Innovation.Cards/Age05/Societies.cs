using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Societies : ICard
    {
        public string Name { get { return "Societies"; } }
        public int Age { get { return 5; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Crown; } }
        public Symbol Left { get { return Symbol.Blank; } }
        public Symbol Center { get { return Symbol.Lightbulb; } }
        public Symbol Right { get { return Symbol.Crown; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a top non-purple card with a [BULB] from your board to my board! If you do, draw a [5]!", Action1)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}