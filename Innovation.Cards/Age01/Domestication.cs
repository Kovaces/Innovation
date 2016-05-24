using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Cards
{
	public class Domestication : CardBase
	{
		public override string Name => "Domestication";
		public override int Age => 1;
		public override Color Color => Color.Yellow;
		public override Symbol Top => Symbol.Tower;
		public override Symbol Left => Symbol.Crown;
		public override Symbol Center => Symbol.Blank;
		public override Symbol Right => Symbol.Tower;

		public override IEnumerable<CardAction> Actions => new List<CardAction>()
		{
			new CardAction(ActionType.Required, Symbol.Tower, "Meld the lowest card in your hand. Draw a [1].", Action1)
		};

		private bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Hand.Any())
			{
				var lowestAgeInHand = parameters.TargetPlayer.Hand.Min(c => c.Age);
				var lowestCards = parameters.TargetPlayer.Hand.Where(c => c.Age.Equals(lowestAgeInHand)).ToList();

				ICard cardToMeld = parameters.TargetPlayer.PickCard(lowestCards);
				parameters.TargetPlayer.Hand.Remove(cardToMeld);
				Meld.Action(cardToMeld, parameters.TargetPlayer);
			}

			parameters.TargetPlayer.Hand.Add(Draw.Action(1, parameters.Game));

			return true;
		}
	}
}