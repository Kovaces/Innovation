using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
namespace Innovation.Cards
{
    public class RoadBuilding : ICard
    {
        public string Name { get { return "Road Building"; } }
        public int Age { get { return 2; } }
        public Color Color { get { return Color.Red; } }
        public Symbol Top { get { return Symbol.Tower; } }
        public Symbol Left { get { return Symbol.Tower; } }
        public Symbol Center { get { return Symbol.Blank; } }
        public Symbol Right { get { return Symbol.Tower; } }
        public IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Tower, "Meld one or two cards from your hand. If you melded two, you may transfer your top red card to another player's board. If you do, transfer that player's top green card to your board.", Action1)
                };
            }
        }
        bool Action1(object[] parameters) 
		{
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			if (targetPlayer.Hand.Count > 0)
			{
				List<ICard> cardsToMeld = targetPlayer.PickFromMultipleCards(targetPlayer.Hand, 1, 2);
				foreach (ICard card in cardsToMeld)
					Meld.Action(card, targetPlayer);

				if (cardsToMeld.Count == 2)
				{
					if (targetPlayer.AskQuestion("Do you want to transfer your top red card to another player's board? If you do, transfer that player's top green card to your board."))
					{
						ICard topRedCard = targetPlayer.Tableau.GetTopCards().Where(x => x.Color == Color.Red).FirstOrDefault();
						if (topRedCard != null)
						{
							Player playerToTransferTo = targetPlayer.PickPlayer(game);

							targetPlayer.Tableau.Stacks[Color.Red].Cards.Remove(topRedCard);
							playerToTransferTo.Tableau.Stacks[Color.Red].AddCardToTop(topRedCard);

							ICard topGreenCard = playerToTransferTo.Tableau.GetTopCards().Where(x => x.Color == Color.Green).FirstOrDefault();
							if (topGreenCard != null)
							{
								playerToTransferTo.Tableau.Stacks[Color.Green].Cards.Remove(topGreenCard);
								targetPlayer.Tableau.Stacks[Color.Green].AddCardToTop(topGreenCard);
							}
						}
					}
				}

				return true;
			}

			return false;
		}
    }
}