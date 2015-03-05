using System.Collections.Generic;
using System.Linq;
using Innovation.Actions.ActionWorkers;
using Innovation.Actions.Handlers;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Actions
{
	public class Dogma
	{
		public static void Action(ICard card, IPlayer activePlayer, Game game)
		{
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

					if (action.ActionType == ActionType.Optional)
					{
						game.ActionQueue.AddAction(new QueuedAction()
						{
							Type = QueuedActionType.AskQuestion,
							Game = game,
							ActivePlayer = activePlayer,
							TargetPlayer = targetPlayer,
							PlayerSymbolCounts = playerSymbolCounts,
							Parameters = new ActionParameters()
							{
								Text = "Participate in " + card.Name + "'s action?\r" + action.ActionText,
								ResponseHandler = action.ActionHandler
							}
						});
					}
					else
						game.ActionQueue.AddAction(new QueuedAction()
						{
							Type = QueuedActionType.ImmediateDelegate,
							Game = game,
							ActivePlayer = activePlayer,
							TargetPlayer = targetPlayer,
							PlayerSymbolCounts = playerSymbolCounts,
							Parameters = new ActionParameters()
							{
								Text = "Participate in " + card.Name + "'s action?\r" + action.ActionText,
								ResponseHandler = action.ActionHandler
							}
						});
				}
			}

			game.ActionQueue.AddAction(new QueuedAction()
			{
				Type = QueuedActionType.EndDogma,
				Game = game,
				ActivePlayer = activePlayer,
			});

			ActionQueueManager.PopNextAction(game);
		}

		private static bool PlayerEligable(int activePlayerSymbolCount, int targetPlayerSymbolCount, bool isDemand)
		{
			return isDemand ? (activePlayerSymbolCount > targetPlayerSymbolCount) : (targetPlayerSymbolCount >= activePlayerSymbolCount);
		}
	}
}
