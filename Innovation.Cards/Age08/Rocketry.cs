using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Rocketry : ICard
    {
        public string Name { get { return "Rocketry"; } }
        public int Age { get { return 8; } }
        public Color Color { get { return Color.Blue; } }
        public Symbol Top { get { return Symbol.Clock; } }
        public Symbol Left { get { return Symbol.Clock; } }
        public Symbol Center { get { return Symbol.Clock; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Clock,"Return a card in any other player's score pile for every two [CLOCK] on your board.", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}