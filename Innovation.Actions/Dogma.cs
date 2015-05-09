using System;
using System.Collections.Generic;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Players;

namespace Innovation.Actions
{
	public class Dogma
	{
		public static void Action(ICard card, List<Player> players, Player activePlayer, List<Deck> ageDecks, Action<string, object> addToGameStorage, Func<string, object> getFromGameStorage)
		{
			var playerSymbolCounts = new Dictionary<Player, Dictionary<Symbol, int>>();

			players.ForEach(p => playerSymbolCounts.Add(p, p.Tableau.GetSymbolCounts()));

			foreach (var action in card.Actions)
			{
				var activePlayerSymbolCount = playerSymbolCounts[activePlayer][action.Symbol];

				foreach (var targetPlayer in players)
				{
					var targetPlayerSymbolCount = playerSymbolCounts[targetPlayer][action.Symbol];

					if (!PlayerEligable(activePlayerSymbolCount, targetPlayerSymbolCount, action.ActionType == ActionType.Demand))
						continue;

					action.ActionHandler(new CardActionParameters { TargetPlayer = targetPlayer, ActivePlayer = activePlayer, AgeDecks = ageDecks, Players = players, AddToStorage = addToGameStorage, GetFromStorage = getFromGameStorage});
				}
			}

			if ((bool)getFromGameStorage(ContextStorage.AnotherPlayerTookDogmaActionKey))
				activePlayer.Hand.Add(Draw.Action(activePlayer.Tableau.GetHighestAge(), ageDecks));
		}

		private static bool PlayerEligable(int activePlayerSymbolCount, int targetPlayerSymbolCount, bool isDemand)
		{
			return isDemand ? (activePlayerSymbolCount > targetPlayerSymbolCount) : (targetPlayerSymbolCount >= activePlayerSymbolCount);
		}
	}
}
