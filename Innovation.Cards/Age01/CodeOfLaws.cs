using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
using Innovation.Actions.Handlers;
namespace Innovation.Cards
{
    public class CodeOfLaws : CardBase
    {
        public override string Name { get { return "Code of Laws"; } }
        public override int Age { get { return 1; } }
        public override Color Color { get { return Color.Purple; } }
        public override Symbol Top { get { return Symbol.Blank; } }
        public override Symbol Left { get { return Symbol.Crown; } }
        public override Symbol Center { get { return Symbol.Crown; } }
        public override Symbol Right { get { return Symbol.Leaf; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Optional, Symbol.Crown, "You may tuck a card from your hand of the same color as any card on your board. If you do, you may splay that color of your cards left.", Action1)
                };
            }
        }
		CardActionResults Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			List<Color> topCardColors = parameters.TargetPlayer.Tableau.GetStackColors();
			List<ICard> cardsMatchingBoardColor = parameters.TargetPlayer.Hand.Where(x => topCardColors.Contains(x.Color)).ToList();

			if (cardsMatchingBoardColor.Count == 0)
				return new CardActionResults(false, false);

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				cardsMatchingBoardColor,
				1, 1,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return new CardActionResults(false, true);
		}

		CardActionResults Action1_Step2(CardActionParameters parameters)
		{
			ICard cardToTuck = parameters.Answer.SingleCard;
			if (cardToTuck == null)
				return new CardActionResults(false, false);
				
			parameters.TargetPlayer.Hand.Remove(cardToTuck);
			Tuck.Action(cardToTuck, parameters.TargetPlayer);

			RequestQueueManager.AskToSplay(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				new List<Color>() { cardToTuck.Color },
				SplayDirection.Left,
				parameters.PlayerSymbolCounts,
				Action1_Step3
			);

			return new CardActionResults(true, true);
		}

		CardActionResults Action1_Step3(CardActionParameters parameters)
		{
			parameters.TargetPlayer.Tableau.Stacks[parameters.Answer.Color].SplayedDirection = SplayDirection.Left;

			return new CardActionResults(true, false);
		}
    }
}