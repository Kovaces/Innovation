﻿using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Measurement : CardBase
    {
        public override string Name { get { return "Measurement"; } }
        public override int Age { get { return 5; } }
        public override Color Color { get { return Color.Green; } }
        public override Symbol Top { get { return Symbol.Lightbulb; } }
        public override Symbol Left { get { return Symbol.Leaf; } }
        public override Symbol Center { get { return Symbol.Lightbulb; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you do, splay any one color of your cards right, and draw a card of value equal to the number of cards of that color on your board.", Action1)
                };
            }
        }
        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}