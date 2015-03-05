using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
using Innovation.Actions.Handlers;
namespace Innovation.Cards
{
	public class Clothing : CardBase
	{
		public override string Name { get { return "Clothing"; } }
		public override int Age { get { return 1; } }
		public override Color Color { get { return Color.Green; } }
		public override Symbol Top { get { return Symbol.Blank; } }
		public override Symbol Left { get { return Symbol.Crown; } }
		public override Symbol Center { get { return Symbol.Leaf; } }
		public override Symbol Right { get { return Symbol.Leaf; } }

		public override IEnumerable<CardAction> Actions
		{
			get
			{
				return new List<CardAction>()
				{
					new CardAction(ActionType.Required, Symbol.Leaf, "Meld a card from your hand of different color from any card on your board.", Action1),
					new CardAction(ActionType.Required, Symbol.Leaf, "Draw and score a [1] for each color present on your board not present on any other player's board.", Action2)
				};
			}
		}

		bool Action1(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			List<Color> topCardColors = parameters.TargetPlayer.Tableau.GetStackColors();
			List<ICard> cardsToMeld = parameters.TargetPlayer.Hand.Where(x => !topCardColors.Contains(x.Color)).ToList();

			if (cardsToMeld.Count == 0)
				return false;

			RequestQueueManager.PickCards(
				parameters.Game,
				parameters.TargetPlayer,
				parameters.ActivePlayer,
				parameters.TargetPlayer,
				cardsToMeld,
				1, 1,
				parameters.PlayerSymbolCounts,
				Action1_Step2
			);
			return false;
		}
		bool Action1_Step2(CardActionParameters parameters)
		{
			ICard drawnCard = parameters.Answer.SingleCard;
			if (drawnCard == null)
				throw new ArgumentNullException("Must choose card.");

			parameters.TargetPlayer.Hand.Remove(drawnCard);
			Meld.Action(drawnCard, parameters.TargetPlayer);

			return true;
		}

		bool Action2(CardActionParameters parameters)
		{
			ValidateParameters(parameters);

			List<Color> targetPlayerTopCardColors = parameters.TargetPlayer.Tableau.GetStackColors();
			List<Color> otherPlayerTopCardColors = parameters.Game.Players.Where(p => p != parameters.TargetPlayer).SelectMany(r => r.Tableau.GetStackColors()).Distinct().ToList();

			var numberOfCardsToDraw = targetPlayerTopCardColors.Count(x => !otherPlayerTopCardColors.Contains(x));
			for (var i = 0; i < numberOfCardsToDraw; i++)
			{
				var card = Draw.Action(1, parameters.Game);
				if (card == null)
					return true;
				Score.Action(card, parameters.TargetPlayer);
			}

			return (numberOfCardsToDraw > 0);
		}
	}
}