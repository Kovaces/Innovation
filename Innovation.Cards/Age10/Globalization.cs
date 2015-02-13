using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Globalization : CardBase
    {
        public override string Name { get { return "Globalization"; } }
        public override int Age { get { return 10; } }
        public override Color Color { get { return Color.Yellow; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Factory; } }
        public override Symbol Right { get { return Symbol.Factory; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Factory,"I demand you return a top card with a [LEAF] on your board.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Factory,"Draw and score a [6]. If no player has more [LEAF] than [FACTORY] on their board, the single player with the most points wins.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}