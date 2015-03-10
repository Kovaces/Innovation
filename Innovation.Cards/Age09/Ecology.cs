using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Ecology : CardBase
    {
        public override string Name { get { return "Ecology"; } }
		public override int Age { get { return 9; } }
		public override Color Color { get { return Color.Yellow; } }
		public override Symbol Top { get { return Symbol.Leaf; } }
		public override Symbol Left { get { return Symbol.Lightbulb; } }
		public override Symbol Center { get { return Symbol.Lightbulb; } }
		public override Symbol Right { get { return Symbol.Blank; } }
		public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you do, score a card from your hand and draw two [10].", Action1)
                };
            }
        }
        CardActionResults Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}