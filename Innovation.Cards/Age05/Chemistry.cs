using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Chemistry : CardBase
    {
        public override string Name { get { return "Chemistry"; } }
        public override int Age { get { return 5; } }
        public override Color Color { get { return Color.Blue; } }
        public override Symbol Top { get { return Symbol.Factory; } }
        public override Symbol Left { get { return Symbol.Lightbulb; } }
        public override Symbol Center { get { return Symbol.Factory; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Factory,"You may splay your blue cards right.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Factory,"Draw and score a card of value one higher than the highest top card on your board and then return a card from your score pile.", Action2)
                };
            }
        }
        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}