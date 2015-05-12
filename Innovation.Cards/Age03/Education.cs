using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Education : CardBase
    {
        public override string Name { get { return "Education"; } }
        public override int Age { get { return 3; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Lightbulb; } }
        public override Symbol Left { get { return Symbol.Lightbulb; } }
        public override Symbol Center { get { return Symbol.Lightbulb; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return the highest card from your score pile. If you do, draw a card of value two higher than the highest card remaining in your score pile.", Action1)
                };
            }
        }
        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}