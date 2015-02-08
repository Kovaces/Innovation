using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Democracy : ICard
    {
        public string Name { get { return "Democracy"; } }
        public int Age { get { return 6; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Crown; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Lightbulb; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return any number of cards from your hand. If you have returned more cards than any other player due to Democracy so far during this dogma action, draw and score an [8].", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}