using Innovation.Actions;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;
using System.Collections.Generic;

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
		public override IEnumerable<ICardAction> Actions
		{
			get
			{
				return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Tower, "Draw two [1].", Action1)
                };
			}
		}
		void Action1(ICardActionParameters input)
		{
			var parameters = input as CardActionParameters;

			ValidateParameters(parameters);

			parameters.TargetPlayer.AddCardToHand(Draw.Action(1, parameters.AgeDecks));
			parameters.TargetPlayer.AddCardToHand(Draw.Action(1, parameters.AgeDecks));

			PlayerActed(parameters);
		}
	}
}