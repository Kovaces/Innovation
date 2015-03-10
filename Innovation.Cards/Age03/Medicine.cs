using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Medicine : CardBase
    {
        public override string Name { get { return "Medicine"; } }
        public override int Age { get { return 3; } }
        public override Color Color { get { return Color.Yellow; } }
        public override Symbol Top { get { return Symbol.Crown; } }
        public override Symbol Left { get { return Symbol.Leaf; } }
        public override Symbol Center { get { return Symbol.Leaf; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you exchange the highest card in your score pile with the lowest card in my score pile!", Action1)
                };
            }
        }
        CardActionResults Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}