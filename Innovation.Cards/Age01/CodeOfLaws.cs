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
		bool Action1(object[] parameters)
		{
			ParseParameters(parameters, 2);

			List<Color> topCardColors = TargetPlayer.Tableau.GetStackColors();
			List<ICard> cardsMatchingBoardColor = TargetPlayer.Hand.Where(x => topCardColors.Contains(x.Color)).ToList();

			if (cardsMatchingBoardColor.Count == 0)
				return false;

			ICard cardToTuck = TargetPlayer.PickCard(cardsMatchingBoardColor);

			if (cardToTuck == null)
				return false;
				
			TargetPlayer.Hand.Remove(cardToTuck);
			Tuck.Action(cardToTuck, TargetPlayer);

			if (TargetPlayer.AskToSplay(cardToTuck.Color, SplayDirection.Left))
				TargetPlayer.Tableau.Stacks[cardToTuck.Color].SplayedDirection = SplayDirection.Left;

			return true;	
		}
    }
}