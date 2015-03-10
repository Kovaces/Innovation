using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models;
using Innovation.Models.Enums;
using Innovation.Models.Interfaces;

namespace Innovation.Actions.ActionWorkers
{
	public static class QueuedActionPickPlayer
	{
		public static void Execute(QueuedAction queuedAction)
		{
			queuedAction.Game.RequestQueue.AddRequest(new Request()
			{
				Type = RequestType.Player,

				ActivePlayer = queuedAction.ActivePlayer,
				TargetPlayer = queuedAction.TargetPlayer,
				PlayerSymbolCounts = queuedAction.PlayerSymbolCounts,
				MinimumNumberToSelect = 1,
				MaximumNumberToSelect = 1,
				Objects = queuedAction.Parameters.Players,
				ResponseHandler = queuedAction.Parameters.ResponseHandler
			});

			queuedAction.TargetPlayer.PickPlayer(queuedAction.Parameters.Players, 1, 1);
		}
	}
}
