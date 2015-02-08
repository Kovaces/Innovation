using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Monotheism : ICard
    {
        public string Name { get { return "Monotheism"; } }
        public int Age { get { return 2; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Tower; } }
        public Symbol Center { get { return Symbol.Tower; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Tower,"I demand you transfer a top card on your board of different color from any card on my board to my score pile! If you do, draw and tuck a [1]!", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Tower,"Draw and tuck a [1].", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}