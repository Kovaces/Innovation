using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Actions;
using Innovation.Interfaces;


using Innovation.Player;

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
		public override IEnumerable<ICardAction> Actions
		{
			get
			{
				return new List<CardAction>()
				{
                    new CardAction(ActionType.Required, Symbol.Tower, "Meld one or two cards from your hand. If you melded two, you may transfer your top red card to another player's board. If you do, transfer that player's top green card to your board.", Action1)
                };
			}
		}
		void Action1(ICardActionParameters parameters)
		{
			

			ValidateParameters(parameters);

			if (!parameters.TargetPlayer.Hand.Any())
				return;

			var cardsToMeld = parameters.TargetPlayer.Interaction.PickCards(parameters.TargetPlayer.Id, new PickCardParameters { CardsToPickFrom = parameters.TargetPlayer.Hand, MinimumCardsToPick = 1, MaximumCardsToPick = 2 }).ToList();
			
			foreach (var card in cardsToMeld)
			{
				parameters.TargetPlayer.RemoveCardFromHand(card);
				Meld.Action(card, parameters.TargetPlayer);
			}

			PlayerActed(parameters);

			if (cardsToMeld.Count != 2)
				return;

			var topRedCard = parameters.TargetPlayer.Tableau.GetTopCards().FirstOrDefault(x => x.Color == Color.Red);

			if (topRedCard == null)
				return;

			var answer = parameters.TargetPlayer.Interaction.AskQuestion(parameters.TargetPlayer.Id, "You may transfer your top red card to another player's board. If you do, transfer that player's top green card to your board.");
			if (!answer.HasValue || !answer.Value)
				return;

			var selectedPlayer = parameters.TargetPlayer.Interaction.PickPlayer(parameters.TargetPlayer.Id, (IEnumerable<Player.Player>)parameters.Players);
			
			parameters.TargetPlayer.Tableau.Stacks[Color.Red].Cards.Remove(topRedCard);
			selectedPlayer.Tableau.Stacks[Color.Red].AddCardToTop(topRedCard);

			var topGreenCard = selectedPlayer.Tableau.GetTopCards().FirstOrDefault(x => x.Color == Color.Green);
			
			if (topGreenCard == null)
				return;
			
			selectedPlayer.Tableau.Stacks[Color.Green].Cards.Remove(topGreenCard);
			parameters.TargetPlayer.Tableau.Stacks[Color.Green].AddCardToTop(topGreenCard);
		}
	}
}