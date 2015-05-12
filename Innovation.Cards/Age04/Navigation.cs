using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Navigation : CardBase
    {
        public override string Name { get { return "Navigation"; } }
        public override int Age { get { return 4; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Crown; } }
        public override Symbol Center { get { return Symbol.Crown; } }
        public override Symbol Right { get { return Symbol.Crown; } }
        public override IEnumerable<ICardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a [2] or [3] from your score pile, if it has any, to my score pile!", Action1)
                };
            }
        }
        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}