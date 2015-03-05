using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
			var otherPlayerActed = cardActionDelegate(parameters);
			if (otherPlayerActed)
				game.StashPropertyBagValue("OtherPlayersActed", true);
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

			bool waiting = false;

			while (!waiting)
			{
				if (game.GameEnded)
				{
					game.ActionQueue.Clear();
					return;                   // end of game!
				}

				if (queuedAction == null)
				{
					// panic and figure out what to do?  aaaaaaahhhhhhh!

					// examine game state to figure out who's next and make them do something
					//		need to store current player
					//		need to store how many actions that player has taken

					// need to put this somewhere better ?
				}
				else
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
							waiting = true;
							break;

						case QueuedActionType.AskQuestion:
							QueuedActionAskQuestion.Execute(queuedAction);
							waiting = true;
							break;
						case QueuedActionType.PickCard:
							QueuedActionPickCard.Execute(queuedAction);
							waiting = true;
							break;
						case QueuedActionType.PickPlayer:
							QueuedActionPickPlayer.Execute(queuedAction);
							waiting = true;
							break;
					}
				}

				if (!waiting)
					queuedAction = game.ActionQueue.PopAction();
			}
		}
	}
}