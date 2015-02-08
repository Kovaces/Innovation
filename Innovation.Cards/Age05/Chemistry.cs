using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Chemistry : ICard
    {
        public string Name { get { return "Chemistry"; } }
        public int Age { get { return 5; } }
        public Color Color { get { return Color.Blue; } }
        public Symbol Top { get { return Symbol.Factory; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Factory; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your blue cards right.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Factory,"Draw and score a card of value one higher than the highest top card on your board and then return a card from your score pile.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}