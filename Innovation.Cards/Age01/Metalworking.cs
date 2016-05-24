using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Metalworking : CardBase
    {
        public override string Name => "Metalworking";
        public override int Age => 1;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Tower;
        public override Symbol Left => Symbol.Tower;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<CardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Required, Symbol.Tower, "Draw and reveal a [1]. If it has a [TOWER], score it and repeat this dogma effect. Otherwise, keep it.", Action1)
        };

        private bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			var card = Draw.Action(1, parameters.Game);
			parameters.TargetPlayer.RevealCard(card);

			while (card.HasSymbol(Symbol.Tower))
			{
				Score.Action(card, parameters.TargetPlayer);

				card = Draw.Action(1, parameters.Game);
				parameters.TargetPlayer.RevealCard(card);
			}

			parameters.TargetPlayer.Hand.Add(card);

			return true;
		}
    }
}