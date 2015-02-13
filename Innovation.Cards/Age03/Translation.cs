using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Translation : CardBase
    {
        public override string Name { get { return "Translation"; } }
        public override int Age { get { return 3; } }
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
                    new CardAction(ActionType.Optional,Symbol.Crown,"You may meld all the cards in your score pile. If you meld one, you must meld them all.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Crown,"If each top card on your board has a [CROWN], claim the World achievement.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}