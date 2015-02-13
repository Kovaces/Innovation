using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Miniaturization : CardBase
    {
        public override string Name { get { return "Miniaturization"; } }
        public override int Age { get { return 10; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Lightbulb; } }
        public override Symbol Center { get { return Symbol.Clock; } }
        public override Symbol Right { get { return Symbol.Lightbulb; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return a card from your hand. If you returned a [10] draw a [10] for every different value card in your score pile.", Action1)
                };
            }
        }
        bool Action1(object[] parameters) { throw new NotImplementedException(); }
    }
}