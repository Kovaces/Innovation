using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Cards
{
    public class Corporations : CardBase
    {
        public override string Name { get { return "Corporations"; } }
        public override int Age { get { return 8; } }
        public override Color Color { get { return Color.Green; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Factory; } }
        public override Symbol Right { get { return Symbol.Crown; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Factory,"I demand you transfer a top non-green card with a [FACTORY] from your board to my score pile! If you do, draw and meld an [8]!", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Factory,"Draw and meld an [8].", Action2)
                };
            }
        }
        void Action1(ICardActionParameters input) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters input) { throw new NotImplementedException(); }
    }
}