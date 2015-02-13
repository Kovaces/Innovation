using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Sanitation : CardBase
    {
        public override string Name { get { return "Sanitation"; } }
        public override int Age { get { return 7; } }
        public override Color Color { get { return Color.Yellow; } }
        public override Symbol Top { get { return Symbol.Leaf; } }
        public override Symbol Left { get { return Symbol.Leaf; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Leaf; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you exchange the two highest cards in your hand with the lowest card in my hand!", Action1)
                };
            }
        }
        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}