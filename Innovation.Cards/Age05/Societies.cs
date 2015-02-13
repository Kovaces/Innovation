using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Societies : CardBase
    {
        public override string Name { get { return "Societies"; } }
        public override int Age { get { return 5; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Crown; } }
        public override Symbol Left { get { return Symbol.Blank; } }
        public override Symbol Center { get { return Symbol.Lightbulb; } }
        public override Symbol Right { get { return Symbol.Crown; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a top non-purple card with a [BULB] from your board to my board! If you do, draw a [5]!", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}