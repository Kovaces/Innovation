using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Mobility : CardBase
    {
        public override string Name { get { return "Mobility"; } }
        public override int Age { get { return 8; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Clock; } }
        public override Symbol Right { get { return Symbol.Factory; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Factory,"I demand you transfer your two highest non-red top cards without a [FACTORY] from your board to my score pile! If you transferred any cards, draw an [8]!", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}