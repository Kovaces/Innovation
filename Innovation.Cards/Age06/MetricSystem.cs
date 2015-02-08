using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class MetricSystem : ICard
    {
        public string Name { get { return "Metric System"; } }
        public int Age { get { return 6; } }
        public Color Color { get { return Color.Green; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Factory; } }
        public Symbol Center { get { return Symbol.Crown; } }
        public Symbol Right { get { return Symbol.Crown; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Crown,"If your green cards are splayed right, you may splay any one color of your cards right.", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Crown,"You may splay your green cards right.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}