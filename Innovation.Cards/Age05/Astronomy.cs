using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Astronomy : CardBase
    {
        public override string Name { get { return "Astronomy"; } }
        public override int Age { get { return 5; } }
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
                    new CardAction(ActionType.Required,Symbol.Lightbulb,"Draw and reveal a [6]. If the card is green or blue, meld it and repeat this dogma effect.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Lightbulb,"If all the non-purple top cards on your board are value [6] or higher, claim the Universe achievement.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}