using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class MassMedia : CardBase
    {
        public override string Name { get { return "Mass Media"; } }
        public override int Age { get { return 8; } }
        public override Color Color { get { return Color.Green; } }
        public override Symbol Top { get { return Symbol.Lightbulb; } }
        public override Symbol Left { get { return Symbol.Blank; } }
        public override Symbol Center { get { return Symbol.Clock; } }
        public override Symbol Right { get { return Symbol.Lightbulb; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you do, choose a value, and return all cards of that value from all score piles.", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may splay your purple cards up.", Action2)
                };
            }
        }
        CardActionResults Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        CardActionResults Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}