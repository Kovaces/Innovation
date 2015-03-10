using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Fission : CardBase
    {
        public override string Name { get { return "Fission"; } }
        public override int Age { get { return 9; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Clock; } }
        public override Symbol Center { get { return Symbol.Clock; } }
        public override Symbol Right { get { return Symbol.Clock; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Clock,"I demand you draw a [10]! If it is red, remove all hands, boards, and score piles from the game! If this occurs, the dogma action is complete.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Clock,"Return a top card other than Fission from any player's board.", Action2)
                };
            }
        }
        CardActionResults Action1(CardActionParameters parameters) { throw new NotImplementedException(); }
        CardActionResults Action2(CardActionParameters parameters) { throw new NotImplementedException(); }
    }
}