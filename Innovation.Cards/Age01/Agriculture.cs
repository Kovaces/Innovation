using System;
using System.Collections.Generic;
using System.Linq;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Cards
{
	public class Agriculture : CardBase
	{
		public override string Name { get { return "Agriculture"; } }
		public override int Age { get { return 1; } }
		public override Color Color { get { return Color.Yellow; } }
		public override Symbol Top { get { return Symbol.Blank; } }
		public override Symbol Left { get { return Symbol.Leaf; } }
		public override Symbol Center { get { return Symbol.Leaf; } }
		public override Symbol Right { get { return Symbol.Leaf; } }
		public override IEnumerable<CardAction> Actions
		{
			get
			{
				return new List<CardAction>() 
				{
                    new CardAction(ActionType.Optional, Symbol.Leaf, "You may return a card from your hand. If you do, draw and score a card of value one higher than the card you returned.", Action1)
                };
			}
		}

		private bool Action1(object[] parameters)
		{
			ParseParameters(parameters, 2);

			ICard selectedCard = TargetPlayer.PickCardFromHand();
			
			if (selectedCard == null)
				return false;

			TargetPlayer.Hand.Remove(selectedCard);
			Return.Action(selectedCard, Game);

			int ageToDraw = selectedCard.Age + 1;
			var cardToScore = Draw.Action(ageToDraw, Game);
			Score.Action(cardToScore, TargetPlayer);

			return true;
		}
	}
}