using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Classification : ICard
    {
        public string Name { get { return "Classification"; } }
        public int Age { get { return 6; } }
        public Color Color { get { return Color.Green; } }
        public Symbol Top { get { return Symbol.Lightbulb; } }
        public Symbol Left { get { return Symbol.Lightbulb; } }
        public Symbol Center { get { return Symbol.Lightbulb; } }
        public Symbol Right { get { return Symbol.Blank; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Lightbulb,"Reveal the color of a card from your hand. Take into your hand all cards of that color from all other player's hands. Then, meld all cards of that color from your hand.", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}