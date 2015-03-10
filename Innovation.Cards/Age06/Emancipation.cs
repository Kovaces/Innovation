using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Emancipation : CardBase
    {
        public override string Name { get { return "Emancipation"; } }
        public override int Age { get { return 6; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Factory; } }
        public override Symbol Left { get { return Symbol.Lightbulb; } }
        public override Symbol Center { get { return Symbol.Factory; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Factory,"I demand you transfer a card from your hand to my score pile! If you do, draw a [6]!", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your red or puple cards right.", Action2)
                };
            }
        }
        CardActionResults Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        CardActionResults Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}