using System;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class CityStates : ICard
    {
        public string Name { get { return "City States"; } }
        public int Age { get { return 1; } }
        public Color Color { get { return Color.Purple; } }
        public Symbol Top { get { return Symbol.Blank; } }
        public Symbol Left { get { return Symbol.Crown; } }
        public Symbol Center { get { return Symbol.Crown; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>(){
                    new CardAction(ActionType.Demand,Symbol.Crown,"I demand you transfer a top card with a [TOWER] from your board to my board if you have at least four [TOWER] on your board! If you do, draw a [1]!", Action1)
                };
            }
        }
		bool Action1(object[] parameters)
		{
			Game game = null;
			Player targetPlayer = null;
			Player activePlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer, out activePlayer);

			if (targetPlayer.Tableau.GetSymbolCount(Symbol.Tower) >= 4)
			{
				List<ICard> topCardsWithTowers = new List<ICard>();
				foreach (Stack stack in targetPlayer.Tableau.Stacks.Values)
				{
					ICard card = stack.GetTopCard();
					if (card != null)
					{
						if (CardHelper.CardHasSymbol(card, Symbol.Tower))
							topCardsWithTowers.Add(card);
					}
				}
				if (topCardsWithTowers.Count > 0)
				{
					ICard cardToMove = targetPlayer.PickCardFromHand(topCardsWithTowers);
					
					// remove from targetPlayer's board
					targetPlayer.Tableau.Stacks[cardToMove.Color].Cards.Remove(cardToMove);
					
					// add to currentPlayer's board - doesn't say meld, so just add it
					activePlayer.Tableau.Stacks[cardToMove.Color].AddCardToTop(cardToMove);

					// if you do, draw a 1
					targetPlayer.Hand.Add(Draw.Action(1, game));

					return true;
				}
			}

			return false;
		}
    }
}