using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Statistics : CardBase
    {
        public override string Name { get { return "Statistics"; } }
        public override int Age { get { return 5; } }
        public override Color Color { get { return Color.Yellow; } }
        public override Symbol Top { get { return Symbol.Leaf; } }
        public override Symbol Left { get { return Symbol.Lightbulb; } }
        public override Symbol Center { get { return Symbol.Leaf; } }
        public override Symbol Right { get { return Symbol.Blank; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Leaf,"I demand you transfer the highest card in your score pile to your hand! If you do, and have only one card in your hand afterwards, repeat this demand!", Action1)
                    ,new CardAction(ActionType.Optional,Symbol.Leaf,"You may splay your yellow cards right.", Action2)
                };
            }
        }
        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
        void Action2(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}