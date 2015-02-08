using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Composites : ICard
    {
        public string Name { get { return "Composites"; } }
        public int Age { get { return 9; } }
        public Color Color { get { return Color.Red; } }
        public Symbol Top { get { return Symbol.Factory; } }
        public Symbol Left { get { return Symbol.Factory; } }
        public Symbol Center { get { return Symbol.Blank; } }
        public Symbol Right { get { return Symbol.Factory; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Factory,"I demand you transfer all but one card from your hand to my hand! Also, transfer the highest card from your score pile to my score pile!", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}