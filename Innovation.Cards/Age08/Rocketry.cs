using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Rocketry : CardBase
    {
        public override string Name { get { return "Rocketry"; } }
        public override int Age { get { return 8; } }
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
                    new CardAction(ActionType.Required,Symbol.Clock,"Return a card in any other player's score pile for every two [CLOCK] on your board.", Action1)
                };
            }
        }
        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}