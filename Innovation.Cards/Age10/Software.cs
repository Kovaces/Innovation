using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Software : CardBase
    {
        public override string Name { get { return "Software"; } }
        public override int Age { get { return 10; } }
        public override Color Color { get { return Color.Blue; } }
        public override Symbol Top { get { return Symbol.Clock; } }
        public override Symbol Left { get { return Symbol.Clock; } }
        public override Symbol Center { get { return Symbol.Clock; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Clock,"Draw and score a [10].", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Clock,"Draw and meld two [10], then execute each of the second card's non-demand dogma effects. Do not share them.", Action2)
                };
            }
        }
        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}