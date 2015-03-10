using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Satellites : CardBase
    {
        public override string Name { get { return "Satellites"; } }
        public override int Age { get { return 9; } }
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
                    new CardAction(ActionType.Required,Symbol.Clock,"Return all cards from your hand, and draw three [8].", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Clock,"You may splay your purple cards up.", Action2)
                    ,new CardAction(ActionType.Required,Symbol.Clock,"Meld a card from your hand and then execute each of its non-demand dogma effects. Do not share them", Action3)
                };
            }
        }
        CardActionResults Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        CardActionResults Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
        CardActionResults Action3(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}