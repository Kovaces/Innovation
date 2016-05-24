using System;
using System.Collections.Generic;
using Innovation.Interfaces;



namespace Innovation.Cards
{
    public class Electricity : CardBase
    {
        public override string Name => "Electricity";
        public override int Age => 7;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Lightbulb;
        public override Symbol Left => Symbol.Factory;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Factory;

        public override IEnumerable<ICardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Required,Symbol.Factory,"Return all your top cards without a [FACTORY], then draw an [8] for each card you returned.", Action1)
        };

        void Action1(ICardActionParameters parameters) { throw new NotImplementedException(); }
    }
}