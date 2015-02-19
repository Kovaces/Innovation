using System.Collections.Generic;
using System.Linq;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Actions
{
	public class Dogma
	{
		public static void Action(ICard card, IPlayer activePlayer, Game game)
		{
			var otherPlayerActed = false;
			var playerSymbolCounts = new Dictionary<IPlayer, Dictionary<Symbol, int>>();

			game.Players.ForEach(p => playerSymbolCounts.Add(p, p.Tableau.GetSymbolCounts()));

			foreach (var action in card.Actions)
			{
				var activePlayerSymbolCount = playerSymbolCounts[activePlayer][action.Symbol];

				//foreach user in user order (i.e starting with the player to the left of the current player)
				foreach (var targetPlayer in game.GetPlayersInPlayerOrder(game.Players.IndexOf(activePlayer)))
				{
					if (game.GameEnded)
						break;

					var targetPlayerSymbolCount = playerSymbolCounts[targetPlayer][action.Symbol];

					if (!PlayerEligable(activePlayerSymbolCount, targetPlayerSymbolCount, action.ActionType == ActionType.Demand))
						continue;
					
					var actionTaken = action.ActionHandler(new CardActionParameters { TargetPlayer = targetPlayer, Game = game, ActivePlayer = activePlayer, PlayerSymbolCounts = playerSymbolCounts });

					if (action.ActionType == ActionType.Demand)
						continue;

					if (actionTaken && !targetPlayer.Equals(activePlayer))
						otherPlayerActed = true;
					
				}
			}

			//for the current player if anyother player has executed this action perform a free draw action
			if (otherPlayerActed)
				activePlayer.Hand.Add(Draw.Action(activePlayer.Tableau.GetHighestAge(), game));
			
			game.ClearPropertyBag();
		}

		private static bool PlayerEligable(int activePlayerSymbolCount, int targetPlayerSymbolCount, bool isDemand)
		{
			return isDemand ? (activePlayerSymbolCount > targetPlayerSymbolCount) : (targetPlayerSymbolCount >= activePlayerSymbolCount);
		}
	}
}
