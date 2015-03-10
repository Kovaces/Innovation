using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Banking : CardBase
    {
        public override string Name { get { return "Banking"; } }
        public override int Age { get { return 5; } }
        public override Color Color { get { return Color.Green; } }
        public override Symbol Top { get { return Symbol.Factory; } }
        public override Symbol Left { get { return Symbol.Crown; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Crown; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a top non-green card with a [FACTORY] from your board to my board. If you do, draw and score a [5]!", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Crown,"You may splay your green cards right.", Action2)
                };
            }
        }
        CardActionResults Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        CardActionResults Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}