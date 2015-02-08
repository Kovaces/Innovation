using System.Collections.Generic;
using System.Linq;
using Innovation.Models;
using Innovation.Models.Enums;

namespace Innovation.Actions
{
	public class Dogma
	{
		public static void Action(ICard card, Player activePlayer, Game game)
		{
			var otherPlayerActed = false;

			//foreach user in user order (i.e starting with the player to the left of the current player)
			foreach (var player in GetPlayersInPlayerOrder(game.Players, game.Players.IndexOf(activePlayer)))
			{
				foreach (var action in card.Actions)
				{
					var activePlayerSymbolCount = activePlayer.Tableau.GetSymbolCount(action.Symbol);
					if (DeterminePlayerEligability(player.Tableau.GetSymbolCount(action.Symbol), activePlayerSymbolCount, action.ActionType == ActionType.Demand))
					{
						var actionTaken = action.ActionHandler(new object[] { player, game} );

						if (!otherPlayerActed && actionTaken && !player.Equals(activePlayer))
							otherPlayerActed = true;
					}
				}
			}
			
			//for the current player if anyother player has executed this action perform a free draw action
			if (otherPlayerActed)
				Draw.Action(activePlayer.Tableau.GetHighestAge(), game);
		}

		private static bool DeterminePlayerEligability(int activePlayerSymbolCount, int playerSymbolCount, bool isDemand)
		{
			return isDemand ? (activePlayerSymbolCount < playerSymbolCount) : (playerSymbolCount >= activePlayerSymbolCount);
		}

		private static List<Player> GetPlayersInPlayerOrder(List<Player> playerList, int startingIndex)
		{
			var players = playerList.GetRange(startingIndex, playerList.Count - startingIndex);
			if (players.Count < playerList.Count)
				players.AddRange(playerList.GetRange(0, startingIndex));

			return players;
		}
	}
}
