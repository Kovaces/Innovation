using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class MetricSystem : CardBase
    {
        public override string Name { get { return "Metric System"; } }
        public override int Age { get { return 6; } }
        public override Color Color { get { return Color.Green; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Crown; } }
        public override Symbol Right { get { return Symbol.Crown; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Crown,"If your green cards are splayed right, you may splay any one color of your cards right.", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Crown,"You may splay your green cards right.", Action2)
                };
            }
        }
        CardActionResults Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        CardActionResults Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}