using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class Calendar : CardBase
    {
        public override string Name { get { return "Calendar"; } }
        public override int Age { get { return 2; } }
        public override Color Color { get { return Color.Blue; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Leaf; } }
        public override Symbol Center { get { return Symbol.Leaf; } }
        public override Symbol Right { get { return Symbol.Lightbulb; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Leaf, "If you have more cards in your score pile than in your hand, draw two [3].", Action1)
                };
            }
        }
		CardActionResults Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			int cardsInScorePile = parameters.TargetPlayer.Tableau.ScorePile.Count;
			int cardsInHand = parameters.TargetPlayer.Hand.Count;

			if (cardsInScorePile <= cardsInHand)
				return new CardActionResults(false, false);
			
			parameters.TargetPlayer.AddCardToHand(Draw.Action(3, parameters.Game));
			parameters.TargetPlayer.AddCardToHand(Draw.Action(3, parameters.Game));

			return new CardActionResults(true, false);
		}
    }
}