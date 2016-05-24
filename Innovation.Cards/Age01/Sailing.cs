using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Sailing : CardBase
    {
        public override string Name => "Sailing";
        public override int Age => 1;
        public override Color Color => Color.Green;
        public override Symbol Top => Symbol.Crown;
        public override Symbol Left => Symbol.Crown;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Leaf;

        public override IEnumerable<CardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Required, Symbol.Crown, "Draw and meld a [1].", Action1)
        };

        private bool Action1(CardActionParameters parameters)
        {
            ValidateParameters(parameters);

            Meld.Action(Draw.Action(1, parameters.Game), parameters.TargetPlayer);

            return true;
        }
    }
}