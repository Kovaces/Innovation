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
		public override string Name { get { return "Archery"; } }
		public override int Age { get { return 1; } }
		public override Color Color { get { return Color.Red; } }
		public override Symbol Top { get { return Symbol.Tower; } }
		public override Symbol Left { get { return Symbol.Lightbulb; } }
		public override Symbol Center { get { return Symbol.Blank; } }
		public override Symbol Right { get { return Symbol.Tower; } }
		public override IEnumerable<CardAction> Actions
		{
			get
			{
				return new List<CardAction>
				{
                    new CardAction(ActionType.Demand, Symbol.Tower, "I demand you draw a [1], then transfer the highest card in your hand to my hand!", Action1)
                };
			}
		}
		bool Action1(object[] parameters)
		{
			ParseParameters(parameters, 3);

			TargetPlayer.Hand.Add(Draw.Action(1, Game));

			var highestAgeInHand = TargetPlayer.Hand.Max(c => c.Age);
			var highestCards = TargetPlayer.Hand.Where(c => c.Age.Equals(highestAgeInHand)).ToList();

			ICard selectedCard = TargetPlayer.PickCard(highestCards);

			TargetPlayer.Hand.Remove(selectedCard);
			CurrentPlayer.Hand.Add(selectedCard);

			return true;
		}
	}
}