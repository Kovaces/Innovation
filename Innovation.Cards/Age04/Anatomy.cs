using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Anatomy : CardBase
    {
        public override string Name { get { return "Anatomy"; } }
        public override int Age { get { return 4; } }
        public override Color Color { get { return Color.Yellow; } }
        public override Symbol Top { get { return Symbol.Leaf; } }
        public override Symbol Left { get { return Symbol.Leaf; } }
        public override Symbol Center { get { return Symbol.Leaf; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you return a card from your score pile! If you do, return a top card of equal value from your board!", Action1)
                };
            }
        }
        CardActionResults Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}