using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class MassMedia : ICard
    {
        public string Name { get { return "Mass Media"; } }
        public int Age { get { return 8; } }
        public Color Color { get { return Color.Green; } }
        public Symbol Top { get { return Symbol.Lightbulb; } }
        public Symbol Left { get { return Symbol.Blank; } }
        public Symbol Center { get { return Symbol.Clock; } }
        public Symbol Right { get { return Symbol.Lightbulb; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you do, choose a value, and return all cards of that value from all score piles.", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may splay your purple cards up.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}