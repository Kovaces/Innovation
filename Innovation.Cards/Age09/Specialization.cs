using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Specialization : CardBase
    {
        public override string Name { get { return "Specialization"; } }
        public override int Age { get { return 9; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Leaf; } }
        public override Symbol Right { get { return Symbol.Factory; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Factory,"Reveal a card from your hand. Take into your hand the top card of that color from all other players' boards.", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your yellow or blue cards up.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}