using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Bicycle : CardBase
    {
        public override string Name { get { return "Bicycle"; } }
        public override int Age { get { return 7; } }
        public override Color Color { get { return Color.Green; } }
        public override Symbol Top { get { return Symbol.Crown; } }
        public override Symbol Left { get { return Symbol.Crown; } }
        public override Symbol Center { get { return Symbol.Clock; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Crown,"You may exchange all cards in your hand with all the cards in your score pile. If you exchange one, you must exchange them all.", Action1)
                };
            }
        }
        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}