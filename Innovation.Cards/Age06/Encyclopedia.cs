using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Encyclopedia : ICard
    {
        public string Name { get { return "Encyclopedia"; } }
        public int Age { get { return 6; } }
        public Color Color { get { return Color.Blue; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Crown; } }
        public Symbol Center { get { return Symbol.Crown; } }
        public Symbol Right { get { return Symbol.Crown; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Crown,"You may meld all the highest cards in your score pile. If you meld one of the highest, you must meld all of the highest.", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}