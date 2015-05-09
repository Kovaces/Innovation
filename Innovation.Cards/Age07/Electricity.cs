using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Cards
{
    public class Electricity : CardBase
    {
        public override string Name { get { return "Electricity"; } }
        public override int Age { get { return 7; } }
        public override Color Color { get { return Color.Green; } }
        public override Symbol Top { get { return Symbol.Lightbulb; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Factory; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Factory,"Return all your top cards without a [FACTORY], then draw an [8] for each card you returned.", Action1)
                };
            }
        }
        void Action1(ICardActionParameters input) { throw new NotImplementedException(); }
    }
}