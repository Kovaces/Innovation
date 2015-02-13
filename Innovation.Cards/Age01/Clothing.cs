using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
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

		bool Action1(object[] parameters)
		{
			ParseParameters(parameters, 2);

			List<Color> topCardColors = TargetPlayer.Tableau.GetStackColors();
			List<ICard> cardsToMeld = TargetPlayer.Hand.Where(x => !topCardColors.Contains(x.Color)).ToList();

			if (cardsToMeld.Count > 0)
			{
				ICard card = TargetPlayer.PickCard(cardsToMeld);

				TargetPlayer.Hand.Remove(card);
				Meld.Action(card, TargetPlayer);

				return true;
			}

			return false;
		}

		bool Action2(object[] parameters)
		{
			ParseParameters(parameters, 2);

			List<Color> currentPlayerTopCardColors = TargetPlayer.Tableau.GetStackColors();
			List<Color> otherPlayerTopCardColors = Game.Players.Where(p => p != TargetPlayer).SelectMany(r => r.Tableau.GetStackColors()).Distinct().ToList();

			var numberOfCardsToDraw = currentPlayerTopCardColors.Count(x => !otherPlayerTopCardColors.Contains(x));
			for (var i = 0; i < numberOfCardsToDraw; i++)
				Score.Action(Draw.Action(1, Game), TargetPlayer);

			return (numberOfCardsToDraw > 0);
		}
	}
}