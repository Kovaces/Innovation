using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Calendar : ICard
    {
        public string Name { get { return "Calendar"; } }
        public int Age { get { return 2; } }
        public Color Color { get { return Color.Blue; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Leaf; } }
        public Symbol Center { get { return Symbol.Leaf; } }
        public Symbol Right { get { return Symbol.Lightbulb; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Leaf,"If you have more cards in your score pile than in your hand, draw two [3].", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}