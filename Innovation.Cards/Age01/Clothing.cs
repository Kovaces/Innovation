using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Clothing : ICard
    {
        public string Name { get { return "Clothing"; } }
        public int Age { get { return 1; } }
        public Color Color { get { return Color.Green; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Crown; } }
        public Symbol Center { get { return Symbol.Leaf; } }
        public Symbol Right { get { return Symbol.Leaf; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Leaf,"Meld a card from your hand of different color from any card on your board.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Leaf,"Draw and score a [1] for each color present on your board not present on any other player's board.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}