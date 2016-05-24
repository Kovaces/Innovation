using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class TheInternet : CardBase
    {
        public override string Name => "The Internet";
        public override int Age => 10;
        public override Color Color => Color.Purple;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Clock;
        public override Symbol Center => Symbol.Clock;
        public override Symbol Right => Symbol.Lightbulb;

        public override IEnumerable<CardAction> Actions => new List<CardAction>(){
            new CardAction(ActionType.Optional,Symbol.Clock,"You may splay your green cards up.", Action1)
            ,new CardAction(ActionType.Required,Symbol.Clock,"Draw and score a [10].", Action2)
            ,new CardAction(ActionType.Required,Symbol.Clock,"Draw and meld a [10] for every two [CLOCK] on your board.", Action3)
        };

        bool Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        bool Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
        bool Action3(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}