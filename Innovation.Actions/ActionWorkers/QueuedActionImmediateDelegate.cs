using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Actions.Handlers;
using Innovation.Models;
using Innovation.Models.Interfaces;

namespace Innovation.Actions.ActionWorkers
{
	public static class QueuedActionImmediateDelegate
	{
		public static void Execute(QueuedAction queuedAction)
		{
			// just a queued action that had to wait for other players to go.  no questions or anything.. execute when popped

			ActionQueueManager.ExecuteCardAction(
				queuedAction.Game,
				queuedAction.Parameters.ResponseHandler,
				new CardActionParameters()
				{
					ActivePlayer = queuedAction.ActivePlayer,
					Game = queuedAction.Game,
					PlayerSymbolCounts = queuedAction.PlayerSymbolCounts,
					TargetPlayer = queuedAction.TargetPlayer,
				}
			);
		}
	}
}
