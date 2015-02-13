using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Databases : CardBase
    {
        public override string Name { get { return "Databases"; } }
        public override int Age { get { return 10; } }
        public override Color Color { get { return Color.Green; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Clock; } }
        public override Symbol Center { get { return Symbol.Clock; } }
        public override Symbol Right { get { return Symbol.Clock; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Clock,"I demand you return half (rounded up) of the cards in your score pile!", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}