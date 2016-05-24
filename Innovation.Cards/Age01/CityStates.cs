using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
	public class CityStates : CardBase
	{
		public override string Name => "City States";
		public override int Age => 1;
		public override Color Color => Color.Purple;
		public override Symbol Top => Symbol.Blank;
		public override Symbol Left => Symbol.Crown;
		public override Symbol Center => Symbol.Crown;
		public override Symbol Right => Symbol.Tower;

		public override IEnumerable<CardAction> Actions => new List<CardAction>()
		{
			new CardAction(ActionType.Demand, Symbol.Crown, "I demand you transfer a top card with a [TOWER] from your board to my board if you have at least four [TOWER] on your board! If you do, draw a [1]!", Action1)
		};

		private bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Tableau.GetSymbolCount(Symbol.Tower) < 4)
				return false;
			
			List<ICard> topCardsWithTowers = new List<ICard>();
			
			foreach (Stack stack in parameters.TargetPlayer.Tableau.Stacks.Values)
			{
				ICard card = stack.GetTopCard();
				if (card != null)
				{
					if (card.HasSymbol(Symbol.Tower))
						topCardsWithTowers.Add(card);
				}
			}

			if (topCardsWithTowers.Count == 0)
				return false;
			
			
			ICard cardToMove = parameters.TargetPlayer.PickCard(topCardsWithTowers);
					
			// remove from TargetPlayer's board
			parameters.TargetPlayer.Tableau.Stacks[cardToMove.Color].RemoveCard(cardToMove);
					
			// add to ActivePlayer's board - doesn't say meld, so just add it
			parameters.ActivePlayer.Tableau.Stacks[cardToMove.Color].AddCardToTop(cardToMove);

			// if you do, draw a 1
			parameters.TargetPlayer.Hand.Add(Draw.Action(1, parameters.Game));
			
			return true;
		}
	}
}