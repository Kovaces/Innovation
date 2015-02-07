using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Satellites : ICard
    {
        public string Name { get { return "Satellites"; } }
        public int Age { get { return 9; } }
        public Color Color { get { return Color.Green; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Clock; } }
        public Symbol Center { get { return Symbol.Clock; } }
        public Symbol Right { get { return Symbol.Clock; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Clock,"Return all cards from your hand, and draw three [8].", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Clock,"You may splay your purple cards up.", Action2)
                    ,new CardAction(ActionType.Required,Symbol.Clock,"Meld a card from your hand and then execute each of its non-demand dogma effects. Do not share them", Action3)
                };
            }
        }
        void Action1(object[] parameters) { throw new NotImplementedException(); }
        void Action2(object[] parameters) { throw new NotImplementedException(); }
        void Action3(object[] parameters) { throw new NotImplementedException(); }
    }
}