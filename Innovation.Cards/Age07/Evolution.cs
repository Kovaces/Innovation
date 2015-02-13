using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Publications : CardBase
    {
        public override string Name { get { return "Publications"; } }
        public override int Age { get { return 7; } }
        public override Color Color { get { return Color.Blue; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Lightbulb; } }
        public override Symbol Center { get { return Symbol.Clock; } }
        public override Symbol Right { get { return Symbol.Lightbulb; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may rearrange the order of one color of cards on your board.", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may splay your yellow or blue cards up.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}