using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Robotics : ICard
    {
        public string Name { get { return "Robotics"; } }
        public int Age { get { return 10; } }
        public Color Color { get { return Color.Red; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Factory; } }
        public Symbol Center { get { return Symbol.Clock; } }
        public Symbol Right { get { return Symbol.Factory; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Factory,"Score your top green card. Draw and meld a [10], then execute each of its non-demand dogma effects. Do not share them.", Action1)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}