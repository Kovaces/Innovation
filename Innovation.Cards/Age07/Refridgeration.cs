using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Cards
{
    public class Refridgeration : CardBase
    {
        public override string Name { get { return "Refridgeration"; } }
        public override int Age { get { return 7; } }
        public override Color Color { get { return Color.Yellow; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Leaf; } }
        public override Symbol Center { get { return Symbol.Leaf; } }
        public override Symbol Right { get { return Symbol.Crown; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you return half (rounded down) of the cards in your hand!", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Leaf,"You may score a card from your hand.", Action2)
                };
            }
        }
        void Action1(ICardActionParameters input) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters input) { throw new NotImplementedException(); }
    }
}