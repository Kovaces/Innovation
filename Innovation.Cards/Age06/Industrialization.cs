using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Industrialization : CardBase
    {
        public override string Name { get { return "Industrialization"; } }
        public override int Age { get { return 6; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Crown; } }
        public override Symbol Left { get { return Symbol.Factory; } }
        public override Symbol Center { get { return Symbol.Factory; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Factory,"Draw and tuck a [6] for every two [FACTORY] on your board.", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your red or purple cards right.", Action2)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
        bool Action2(object[] parameters) { throw new NotImplementedException(); }
    }
}