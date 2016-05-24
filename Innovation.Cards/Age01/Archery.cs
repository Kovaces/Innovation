using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
	public class Archery : CardBase
	{
		public override string Name => "Archery";
		public override int Age => 1;
		public override Color Color => Color.Red;
		public override Symbol Top => Symbol.Tower;
		public override Symbol Left => Symbol.Lightbulb;
		public override Symbol Center => Symbol.Blank;
		public override Symbol Right => Symbol.Tower;

		public override IEnumerable<CardAction> Actions => new List<CardAction>
		{
			new CardAction(ActionType.Demand, Symbol.Tower, "I demand you draw a [1], then transfer the highest card in your hand to my hand!", Action1)
		};

		private bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			parameters.TargetPlayer.Hand.Add(Draw.Action(1, parameters.Game));

			var highestAgeInHand = parameters.TargetPlayer.Hand.Max(c => c.Age);
			var highestCards = parameters.TargetPlayer.Hand.Where(c => c.Age.Equals(highestAgeInHand)).ToList();

			ICard selectedCard = parameters.TargetPlayer.PickCard(highestCards);

			parameters.TargetPlayer.Hand.Remove(selectedCard);
			parameters.ActivePlayer.Hand.Add(selectedCard);

			return true;
		}
	}
}