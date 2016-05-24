using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
	public class Mysticism : CardBase
	{
		public override string Name => "Mysticism";
		public override int Age => 1;
		public override Color Color => Color.Purple;
		public override Symbol Top => Symbol.Blank;
		public override Symbol Left => Symbol.Tower;
		public override Symbol Center => Symbol.Tower;
		public override Symbol Right => Symbol.Tower;

		public override IEnumerable<CardAction> Actions => new List<CardAction>(){
			new CardAction(ActionType.Required,Symbol.Tower,"Draw and reveal a [1]. If it is the same color as any card on your board, meld it and draw a [1].", Action1)
		};

		private bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			ICard card = Draw.Action(1, parameters.Game);

			parameters.TargetPlayer.RevealCard(card);

			if (parameters.TargetPlayer.Tableau.GetStackColors().Contains(card.Color))
			{
				Meld.Action(card, parameters.TargetPlayer);
				parameters.TargetPlayer.Hand.Add(Draw.Action(1, parameters.Game));
			}
			else
				parameters.TargetPlayer.Hand.Add(card);

			return true;
		}
	}
}