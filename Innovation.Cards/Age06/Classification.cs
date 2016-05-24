using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Classification : CardBase
    {
        public override string Name => "Classification";
        public override int Age => 6;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Lightbulb;
        public override Symbol Left => Symbol.Lightbulb;
        public override Symbol Center => Symbol.Lightbulb;
        public override Symbol Right => Symbol.Blank;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Lightbulb,"Reveal the color of a card from your hand. Take into your hand all cards of that color from all other player's hands. Then, meld all cards of that color from your hand.", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}