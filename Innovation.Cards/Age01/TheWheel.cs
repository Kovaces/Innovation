using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Cards
{
	public class TheWheel : CardBase
	{
		public override string Name { get { return "The Wheel"; } }
		public override int Age { get { return 1; } }
		public override Color Color { get { return Color.Green; } }
		public override Symbol Top { get { return Symbol.Blank; } }
		public override Symbol Left { get { return Symbol.Tower; } }
		public override Symbol Center { get { return Symbol.Tower; } }
		public override Symbol Right { get { return Symbol.Tower; } }
		public override IEnumerable<CardAction> Actions
		{
			get
			{
				return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Tower, "Draw two [1].", Action1)
                };
			}
		}
		CardActionResults Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			var drawnCard = Draw.Action(1, parameters.Game);
			if (drawnCard == null)
				return new CardActionResults(true, false);
			parameters.TargetPlayer.Hand.Add(drawnCard);

			drawnCard = Draw.Action(1, parameters.Game);
			if (drawnCard == null)
				return new CardActionResults(true, false);
			parameters.TargetPlayer.Hand.Add(drawnCard);

			return new CardActionResults(true, false);
		}
	}
}