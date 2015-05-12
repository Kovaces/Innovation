using Innovation.Actions;
using System.Collections.Generic;
using Innovation.Interfaces;


namespace Innovation.Cards
{
	public class Sailing : CardBase
	{
		public override string Name { get { return "Sailing"; } }
		public override int Age { get { return 1; } }
		public override Color Color { get { return Color.Green; } }
		public override Symbol Top { get { return Symbol.Crown; } }
		public override Symbol Left { get { return Symbol.Crown; } }
		public override Symbol Center { get { return Symbol.Blank; } }
		public override Symbol Right { get { return Symbol.Leaf; } }
		public override IEnumerable<ICardAction> Actions
		{
			get
			{
				return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Crown, "Draw and meld a [1].", Action1)
                };
			}
		}
		void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			Meld.Action(Draw.Action(1, parameters.AgeDecks), parameters.TargetPlayer);

			PlayerActed(parameters);
		}
	}
}