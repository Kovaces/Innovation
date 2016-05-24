using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Cards
{
    public class TheWheel : CardBase
    {
        public override string Name => "The Wheel";
        public override int Age => 1;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Blank;
        public override Symbol Left => Symbol.Tower;
        public override Symbol Center => Symbol.Tower;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<CardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Required, Symbol.Tower, "Draw two [1].", Action1)
        };

        private bool Action1(CardActionParameters parameters)
        {
            ValidateParameters(parameters);

            parameters.TargetPlayer.Hand.Add(Draw.Action(1, parameters.Game));
            parameters.TargetPlayer.Hand.Add(Draw.Action(1, parameters.Game));

            return true;
        }
    }
}