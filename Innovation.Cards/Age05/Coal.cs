using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Cards
{
    public class Coal : CardBase
    {
        public override string Name { get { return "Coal"; } }
        public override int Age { get { return 5; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Factory; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Factory; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Factory,"Draw and tuck a [5].", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your red cards right.", Action2)
                    ,new CardAction(ActionType.Optional,Symbol.Factory,"You may score any one of your top cards. If you do, also score the card beneath it.", Action3)
                };
            }
        }
        void Action1(ICardActionParameters input) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters input) { throw new NotImplementedException(); }
        void Action3(ICardActionParameters input) { throw new NotImplementedException(); }
    }
}