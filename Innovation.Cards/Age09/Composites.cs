using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Composites : CardBase
    {
        public override string Name { get { return "Composites"; } }
        public override int Age { get { return 9; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Factory; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Factory; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Factory,"I demand you transfer all but one card from your hand to my hand! Also, transfer the highest card from your score pile to my score pile!", Action1)
                };
            }
        }
        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}