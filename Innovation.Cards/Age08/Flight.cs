﻿using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Flight : CardBase
    {
        public override string Name { get { return "Flight"; } }
        public override int Age { get { return 8; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Crown; } }
        public override Symbol Left { get { return Symbol.Blank; } }
        public override Symbol Center { get { return Symbol.Clock; } }
        public override Symbol Right { get { return Symbol.Crown; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Crown,"If you red cards are splayed up, you may splay any one color of your cards up.", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Crown,"You may splay your red cards up.", Action2)
                };
            }
        }
        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}