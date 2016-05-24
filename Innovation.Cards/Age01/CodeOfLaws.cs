using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
namespace Innovation.Cards
{
	public class CodeOfLaws : CardBase
	{
		public override string Name => "Code of Laws";
		public override int Age => 1;
		public override Color Color => Color.Purple;
		public override Symbol Top => Symbol.Blank;
		public override Symbol Left => Symbol.Crown;
		public override Symbol Center => Symbol.Crown;
		public override Symbol Right => Symbol.Leaf;

		public override IEnumerable<CardAction> Actions => new List<CardAction>()
		{
			new CardAction(ActionType.Optional, Symbol.Crown, "You may tuck a card from your hand of the same color as any card on your board. If you do, you may splay that color of your cards left.", Action1)
		};

		private bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			List<Color> topCardColors = parameters.TargetPlayer.Tableau.GetStackColors();
			List<ICard> cardsMatchingBoardColor = parameters.TargetPlayer.Hand.Where(x => topCardColors.Contains(x.Color)).ToList();

			if (cardsMatchingBoardColor.Count == 0)
				return false;

			ICard cardToTuck = parameters.TargetPlayer.PickCard(cardsMatchingBoardColor);

			if (cardToTuck == null)
				return false;
				
			parameters.TargetPlayer.Hand.Remove(cardToTuck);
			Tuck.Action(cardToTuck, parameters.TargetPlayer);

			if (parameters.TargetPlayer.AskToSplay(cardToTuck.Color, SplayDirection.Left))
				parameters.TargetPlayer.Tableau.Stacks[cardToTuck.Color].SplayedDirection = SplayDirection.Left;

			return true;	
		}
	}
}