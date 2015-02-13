using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Democracy : CardBase
    {
        public override string Name { get { return "Democracy"; } }
        public override int Age { get { return 6; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Crown; } }
        public override Symbol Left { get { return Symbol.Lightbulb; } }
        public override Symbol Center { get { return Symbol.Lightbulb; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return any number of cards from your hand. If you have returned more cards than any other player due to Democracy so far during this dogma action, draw and score an [8].", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}