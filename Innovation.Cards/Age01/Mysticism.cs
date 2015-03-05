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
		public override string Name { get { return "Mysticism"; } }
		public override int Age { get { return 1; } }
		public override Color Color { get { return Color.Purple; } }
		public override Symbol Top { get { return Symbol.Blank; } }
		public override Symbol Left { get { return Symbol.Tower; } }
		public override Symbol Center { get { return Symbol.Tower; } }
		public override Symbol Right { get { return Symbol.Tower; } }
		public override IEnumerable<CardAction> Actions
		{
			get
			{
				return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Tower,"Draw and reveal a [1]. If it is the same color as any card on your board, meld it and draw a [1].", Action1)
                };
			}
		}
		bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			ICard card = Draw.Action(1, parameters.Game);
			if (card == null)
				return true;

			parameters.TargetPlayer.RevealCard(card);

			if (parameters.TargetPlayer.Tableau.GetStackColors().Contains(card.Color))
			{
				Meld.Action(card, parameters.TargetPlayer);
				var drawnCard = Draw.Action(1, parameters.Game);
				if (drawnCard == null)
					return true;

				parameters.TargetPlayer.Hand.Add(drawnCard);
			}
			else
				parameters.TargetPlayer.Hand.Add(card);

			return true;
		}
	}
}