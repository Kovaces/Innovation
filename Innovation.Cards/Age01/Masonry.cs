using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Masonry : ICard
    {
        public string Name { get { return "Masonry"; } }
        public int Age { get { return 1; } }
        public Color Color { get { return Color.Yellow; } }
        public Symbol Top { get { return Symbol.Tower; } }
        public Symbol Left { get { return Symbol.Blank; } }
        public Symbol Center { get { return Symbol.Tower; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Tower,"You may meld any number of cards from your hand, each with a [TOWER]. If you melded four or more cards in this way, claim the Monument achievement.", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}