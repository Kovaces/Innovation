using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Encyclopedia : CardBase
    {
        public override string Name { get { return "Encyclopedia"; } }
        public override int Age { get { return 6; } }
        public override Color Color { get { return Color.Blue; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Crown; } }
        public override Symbol Center { get { return Symbol.Crown; } }
        public override Symbol Right { get { return Symbol.Crown; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Crown,"You may meld all the highest cards in your score pile. If you meld one of the highest, you must meld all of the highest.", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}