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
        public override string Name => "Road Building";
        public override int Age => 2;
        public override Color Color => Color.Red;
        public override Symbol Top => Symbol.Tower;
        public override Symbol Left => Symbol.Tower;
        public override Symbol Center => Symbol.Blank;
        public override Symbol Right => Symbol.Tower;

        public override IEnumerable<CardAction> Actions => new List<CardAction>()
        {
            new CardAction(ActionType.Required, Symbol.Tower, "Meld one or two cards from your hand. If you melded two, you may transfer your top red card to another player's board. If you do, transfer that player's top green card to your board.", Action1)
        };

        bool Action1(CardActionParameters parameters) 
		{
			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Hand.Count > 0)
			{
				List<ICard> cardsToMeld = parameters.TargetPlayer.PickMultipleCards(parameters.TargetPlayer.Hand, 1, 2).ToList();
				foreach (ICard card in cardsToMeld)
				{
					parameters.TargetPlayer.Hand.Remove(card);
					Meld.Action(card, parameters.TargetPlayer);
				}

				if (cardsToMeld.Count == 2)
				{
					if (parameters.TargetPlayer.AskQuestion("Do you want to transfer your top red card to another player's board? If you do, transfer that player's top green card to your board."))
					{
						ICard topRedCard = parameters.TargetPlayer.Tableau.GetTopCards().Where(x => x.Color == Color.Red).FirstOrDefault();
						if (topRedCard != null)
						{
							IPlayer playerToTransferTo = parameters.TargetPlayer.PickPlayer(parameters.Game.Players.Where(x => x != parameters.TargetPlayer).ToList());

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