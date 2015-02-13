using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Cards
{
    public class RoadBuilding : CardBase
    {
        public override string Name { get { return "Road Building"; } }
        public override int Age { get { return 2; } }
        public override Color Color { get { return Color.Red; } }
        public override Symbol Top { get { return Symbol.Tower; } }
        public override Symbol Left { get { return Symbol.Tower; } }
        public override Symbol Center { get { return Symbol.Blank; } }
        public override Symbol Right { get { return Symbol.Tower; } }
        public override IEnumerable<CardAction> Actions
        {
            get
            {
                return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Tower, "Meld one or two cards from your hand. If you melded two, you may transfer your top red card to another player's board. If you do, transfer that player's top green card to your board.", Action1)
                };
            }
        }
        bool Action1(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Hand.Count > 0)
			{
				List<ICard> cardsToMeld = parameters.TargetPlayer.PickMultipleCards(parameters.TargetPlayer.Hand, 1, 2).ToList();
				foreach (ICard card in cardsToMeld)
					Meld.Action(card, parameters.TargetPlayer);

				if (cardsToMeld.Count == 2)
				{
					if (parameters.TargetPlayer.AskQuestion("Do you want to transfer your top red card to another player's board? If you do, transfer that player's top green card to your board."))
					{
						ICard topRedCard = parameters.TargetPlayer.Tableau.GetTopCards().Where(x => x.Color == Color.Red).FirstOrDefault();
						if (topRedCard != null)
						{
							IPlayer playerToTransferTo = parameters.TargetPlayer.PickPlayer(parameters.Game.Players);

							parameters.TargetPlayer.Tableau.Stacks[Color.Red].Cards.Remove(topRedCard);
							playerToTransferTo.Tableau.Stacks[Color.Red].AddCardToTop(topRedCard);

							ICard topGreenCard = playerToTransferTo.Tableau.GetTopCards().Where(x => x.Color == Color.Green).FirstOrDefault();
							if (topGreenCard != null)
							{
								playerToTransferTo.Tableau.Stacks[Color.Green].Cards.Remove(topGreenCard);
								parameters.TargetPlayer.Tableau.Stacks[Color.Green].AddCardToTop(topGreenCard);
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