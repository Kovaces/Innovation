using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Banking : ICard
    {
        public string Name { get { return "Banking"; } }
        public int Age { get { return 5; } }
        public Color Color { get { return Color.Green; } }
        public Symbol Top { get { return Symbol.Factory; } }
        public Symbol Left { get { return Symbol.Crown; } }
        public Symbol Center { get { return Symbol.Blank; } }
        public Symbol Right { get { return Symbol.Crown; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a top non-green card with a [FACTORY] from your board to my board. If you do, draw and score a [5]!", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Crown,"You may splay your green cards right.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}