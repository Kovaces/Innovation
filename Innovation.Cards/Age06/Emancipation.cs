using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Emancipation : ICard
    {
        public string Name { get { return "Emancipation"; } }
        public int Age { get { return 6; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Factory; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Factory; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Factory,"I demand you transfer a card from your hand to my score pile! If you do, draw a [6]!", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your red or puple cards right.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}