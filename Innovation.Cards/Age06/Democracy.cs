using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Democracy : CardBase
    {
        public override string Name => "Democracy";
        public override int Age => 6;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Crown;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Lightbulb,"You may return any number of cards from your hand. If you have returned more cards than any other player due to Democracy so far during this dogma action, draw and score an [8].", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}