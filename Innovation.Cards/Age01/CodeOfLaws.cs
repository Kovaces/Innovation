using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
namespace Innovation.Cards
{
    public class CodeofLaws : ICard
    {
        public string Name { get { return "Code of Laws"; } }
        public int Age { get { return 1; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Crown; } }
        public Symbol Center { get { return Symbol.Crown; } }
        public Symbol Right { get { return Symbol.Leaf; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Optional,Symbol.Crown,"You may tuck a card from your hand of the same color as any card on your board. If you do, you may splay that color of your cards left.", Action1)
                };
            }
        }
		bool Action1(object[] parameters)
		{
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			List<Color> topCardColors = targetPlayer.Tableau.GetStackColors();
			List<ICard> cardsMatchingBoardColor = targetPlayer.Hand.Where(x => topCardColors.Contains(x.Color)).ToList();

			if (cardsMatchingBoardColor.Count > 0)
			{
				ICard cardToTuck = targetPlayer.PickCardFromHand(cardsMatchingBoardColor);

				Tuck.Action(cardToTuck, targetPlayer);

				if (targetPlayer.AskToSplay(cardToTuck.Color, SplayDirection.Left))
					targetPlayer.Tableau.Stacks[cardToTuck.Color].SplayedDirection = SplayDirection.Left;

				return true;
			}

			return false;
		}
    }
}