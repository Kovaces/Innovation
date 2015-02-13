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

			foreach (var action in card.Actions)
			{
				//foreach user in user order (i.e starting with the player to the left of the current player)
				foreach (var targetPlayer in GetPlayersInPlayerOrder(game.Players, game.Players.IndexOf(activePlayer)))
				{
					var activePlayerSymbolCount = activePlayer.Tableau.GetSymbolCount(action.Symbol);
					var targetPlayerSymbolCount = targetPlayer.Tableau.GetSymbolCount(action.Symbol);

					if (DeterminePlayerEligability(activePlayerSymbolCount, targetPlayerSymbolCount, action.ActionType == ActionType.Demand))
					{
						bool actionTaken = action.ActionHandler(new object[] { targetPlayer, game, activePlayer });
						
						if (!otherPlayerActed && actionTaken && !targetPlayer.Equals(activePlayer) && action.ActionType != ActionType.Demand)
							otherPlayerActed = true;
					}
				}
			}
			
			//for the current player if anyother player has executed this action perform a free draw action
			if (otherPlayerActed)
				activePlayer.Hand.Add(Draw.Action(activePlayer.Tableau.GetHighestAge(), game));

			game.ClearPropertyBag();
		}

		private static bool DeterminePlayerEligability(int activePlayerSymbolCount, int targetPlayerSymbolCount, bool isDemand)
		{
			return isDemand ? (activePlayerSymbolCount > targetPlayerSymbolCount) : (targetPlayerSymbolCount >= activePlayerSymbolCount);
		}

		private static List<IPlayer> GetPlayersInPlayerOrder(List<IPlayer> playerList, int startingIndex)
		{
			var players = playerList.GetRange(startingIndex + 1, playerList.Count - startingIndex - 1);
			if (players.Count < playerList.Count)
				players.AddRange(playerList.GetRange(0, startingIndex + 1));

			return players;
		}
	}
}
