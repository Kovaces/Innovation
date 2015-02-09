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

			foreach (var action in card.Actions)
			{
				//foreach user in user order (i.e starting with the player to the left of the current player)
				foreach (var targetPlayer in GetPlayersInPlayerOrder(game.Players, game.Players.IndexOf(activePlayer)))
				{
					var activePlayerSymbolCount = activePlayer.Tableau.GetSymbolCount(action.Symbol);
					if ((targetPlayer.Equals(activePlayer) && action.ActionType != ActionType.Demand)
							|| DeterminePlayerEligability(targetPlayer.Tableau.GetSymbolCount(action.Symbol), activePlayerSymbolCount, action.ActionType == ActionType.Demand))
					{
						bool actionTaken = false;
						if (action.ActionType == ActionType.Demand)
						{
							actionTaken = action.ActionHandler(new object[] { targetPlayer, game, activePlayer });
						}
						else if (action.ActionType == ActionType.Required)
						{
							actionTaken = action.ActionHandler(new object[] { targetPlayer, game });
						}
						else if (action.ActionType == ActionType.Optional)
						{
							if (targetPlayer.AskToParticipate(action))
								actionTaken = action.ActionHandler(new object[] { targetPlayer, game });
						}

						if (!otherPlayerActed && actionTaken && !targetPlayer.Equals(activePlayer) && action.ActionType != ActionType.Demand)
							otherPlayerActed = true;
					}
				}
			}
			
			//for the current player if anyother player has executed this action perform a free draw action
			if (otherPlayerActed)
				activePlayer.Hand.Add(Draw.Action(activePlayer.Tableau.GetHighestAge(), game));
		}

		private static bool DeterminePlayerEligability(int activePlayerSymbolCount, int playerSymbolCount, bool isDemand)
		{
			return isDemand ? (activePlayerSymbolCount < playerSymbolCount) : (playerSymbolCount >= activePlayerSymbolCount);
		}

		private static List<Player> GetPlayersInPlayerOrder(List<Player> playerList, int startingIndex)
		{
			var players = playerList.GetRange(startingIndex + 1, playerList.Count - startingIndex - 1);
			if (players.Count < playerList.Count)
				players.AddRange(playerList.GetRange(0, startingIndex + 1));

			return players;
		}
	}
}
