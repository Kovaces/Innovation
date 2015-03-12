using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;
using Innovation.Actions.Handlers;

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
		CardActionResults Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			if (parameters.TargetPlayer.Hand.Count == 0)
				return new CardActionResults(false, false);

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				parameters.TargetPlayer.Hand,
				1,
				2,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);

			return new CardActionResults(false, true);
		}
		CardActionResults Action1_Step2(CardActionParameters parameters)
		{
			List<ICard> cardsToMeld = parameters.Answer.MultipleCards;

			foreach (ICard card in cardsToMeld)
			{
				parameters.TargetPlayer.RemoveCardFromHand(card);
				Meld.Action(card, parameters.TargetPlayer);
			}

			if (cardsToMeld.Count == 2)
			{
				if (!parameters.TargetPlayer.Tableau.GetTopCards().Where(x => x.Color == Color.Red).Any())
					return new CardActionResults(true, false);

				RequestQueueManager.AskQuestion(
					parameters.Game,
					parameters.ActivePlayer,
					parameters.TargetPlayer,
					"Do you want to transfer your top red card to another player's board? If you do, transfer that player's top green card to your board.",
					parameters.PlayerSymbolCounts,
					Action1_Step3
				);
			}

			return new CardActionResults(true, true);
		}
		CardActionResults Action1_Step3(CardActionParameters parameters)
		{
			if (parameters.Answer.Boolean)
			{
				ICard topRedCard = parameters.TargetPlayer.Tableau.GetTopCards().Where(x => x.Color == Color.Red).FirstOrDefault();
				if (topRedCard != null)
				{
					RequestQueueManager.PickPlayer(
						parameters.Game,
						parameters.ActivePlayer,
						parameters.TargetPlayer,
						parameters.Game.Players.Where(x => x != parameters.TargetPlayer).ToList(),
						1, 1,
						parameters.PlayerSymbolCounts,
						Action1_Step4
					);
				}
			}
			return new CardActionResults(true, true);
		}
		CardActionResults Action1_Step4(CardActionParameters parameters)
		{
			if (parameters.Answer.Players.Count == 0)
				throw new ArgumentNullException("Must choose a player.");

			IPlayer playerToTransferTo = parameters.Answer.Players.First();

			ICard topRedCard = parameters.TargetPlayer.Tableau.GetTopCards().Where(x => x.Color == Color.Red).FirstOrDefault();
			parameters.TargetPlayer.Tableau.Stacks[Color.Red].Cards.Remove(topRedCard);
			playerToTransferTo.Tableau.Stacks[Color.Red].AddCardToTop(topRedCard);

			ICard topGreenCard = playerToTransferTo.Tableau.GetTopCards().Where(x => x.Color == Color.Green).FirstOrDefault();
			if (topGreenCard != null)
			{
				playerToTransferTo.Tableau.Stacks[Color.Green].Cards.Remove(topGreenCard);
				parameters.TargetPlayer.Tableau.Stacks[Color.Green].AddCardToTop(topGreenCard);
			}

			return new CardActionResults(true, true);
		}
	}
}