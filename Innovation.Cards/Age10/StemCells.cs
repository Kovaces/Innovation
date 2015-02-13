using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class StemCells : CardBase
    {
        public override string Name { get { return "Stem Cells"; } }
        public override int Age { get { return 10; } }
        public override Color Color { get { return Color.Yellow; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Leaf; } }
        public override Symbol Center { get { return Symbol.Leaf; } }
        public override Symbol Right { get { return Symbol.Leaf; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Leaf,"You may score all cards from your hand. If you score one, you must score them all.", Action1)
                };
            }
        }
        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}