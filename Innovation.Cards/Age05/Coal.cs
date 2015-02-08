using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Coal : ICard
    {
        public string Name { get { return "Coal"; } }
        public int Age { get { return 5; } }
        public Color Color { get { return Color.Red; } }
        public Symbol Top { get { return Symbol.Factory; } }
        public Symbol Left { get { return Symbol.Factory; } }
        public Symbol Center { get { return Symbol.Factory; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Factory,"Draw and tuck a [5].", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your red cards right.", Action2)
                    ,new CardAction(ActionType.Optional,Symbol.Factory,"You may score any one of your top cards. If you do, also score the card beneath it.", Action3)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
        bool Action3(object[] parameters) { throw new NotImplementedException(); }
    }
}