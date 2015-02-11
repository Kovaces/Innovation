using System;
using System.Linq;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Actions;
namespace Innovation.Cards
{
	public class Clothing : ICard
	{
		public string Name { get { return "Clothing"; } }
		public int Age { get { return 1; } }
		public Color Color { get { return Color.Green; } }
		public Symbol Top { get { return Symbol.Blank; } }
		public Symbol Left { get { return Symbol.Crown; } }
		public Symbol Center { get { return Symbol.Leaf; } }
		public Symbol Right { get { return Symbol.Leaf; } }
		public IEnumerable<CardAction> Actions
		{
			get
			{
				return new List<CardAction>(){
                    new CardAction(ActionType.Required,Symbol.Leaf,"Meld a card from your hand of different color from any card on your board.", Action1)
                    ,new CardAction(ActionType.Required,Symbol.Leaf,"Draw and score a [1] for each color present on your board not present on any other player's board.", Action2)
                };
			}
		}
		bool Action1(object[] parameters)
		{
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			List<Color> topCardColors = targetPlayer.Tableau.GetStackColors();
			List<ICard> cardsToMeld = targetPlayer.Hand.Where(x => !topCardColors.Contains(x.Color)).ToList();

			if (cardsToMeld.Count > 0)
			{
				ICard card = targetPlayer.PickCardFromHand(cardsToMeld);

				targetPlayer.Hand.Remove(card);
				Meld.Action(card, targetPlayer);

				return true;
			}

			return false;
		}
		bool Action2(object[] parameters)
		{
			Game game = null;
			Player targetPlayer = null;
			CardHelper.GetParameters(parameters, out game, out targetPlayer);

			List<Color> currentPlayerTopCardColors = null;
			List<Color> otherPlayerTopCardColors = new List<Color>();

			foreach (Player player in game.Players)
			{
				if (player == targetPlayer)
					currentPlayerTopCardColors = player.Tableau.GetStackColors();
				else
					otherPlayerTopCardColors.AddRange(player.Tableau.GetStackColors().Where(x => !otherPlayerTopCardColors.Contains(x)));
			}

			bool didSomething = false;
			List<Color> exclusiveColors = currentPlayerTopCardColors.Where(x => !otherPlayerTopCardColors.Contains(x)).ToList();
			foreach (Color color in exclusiveColors)
			{
				didSomething = true;
				Score.Action(Draw.Action(1, game), targetPlayer);
			}

			return didSomething;
		}
	}
}