using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Innovation.Actions.ActionWorkers;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Actions.Handlers
{
	public class ActionQueueManager
	{


		public static void ExecuteCardAction(Game game, CardActionDelegate cardActionDelegate, CardActionParameters parameters)
		{
			var cardActionResults = cardActionDelegate(parameters);
			if (cardActionResults.OtherPlayerActed)
				game.StashPropertyBagValue("OtherPlayersActed", true);

			game.IsWaiting = cardActionResults.IsWaitingForResponse;
		}

		public static void AddAction(Game game, QueuedAction action)
		{
			game.ActionQueue.AddAction(action);
		}
		public static void AddActionToTop(Game game, QueuedAction action)
		{
			game.ActionQueue.AddActionToTop(action);
		}


		public static void PopNextAction(Game game)
		{
			QueuedAction queuedAction = game.ActionQueue.PopAction();

			while (!game.IsWaiting)
			{
				if (game.GameEnded)
				{
					game.ActionQueue.Clear();
					return;                   // end of game!
				}

				if (queuedAction != null)
				{
					switch (queuedAction.Type)
					{
						// actions with immediate responses
						case QueuedActionType.EndDogma:
							QueuedActionEndDogma.Execute(queuedAction);
							break;
						case QueuedActionType.Draw:
							QueuedActionDraw.Execute(queuedAction);
							break;

						// actions requiring input or will take care of themselves
						case QueuedActionType.ImmediateDelegate:
							QueuedActionImmediateDelegate.Execute(queuedAction);
							break;

						case QueuedActionType.AskQuestion:
							QueuedActionAskQuestion.Execute(queuedAction);
							break;
						case QueuedActionType.PickCard:
							QueuedActionPickCard.Execute(queuedAction);
							break;
						case QueuedActionType.PickPlayer:
							QueuedActionPickPlayer.Execute(queuedAction);
							break;
					}
				}

				if (!game.IsWaiting)
					queuedAction = game.ActionQueue.PopAction();
			}
		}
	}
}