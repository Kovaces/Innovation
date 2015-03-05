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
	public static class QueuedActionAskQuestion
	{
		public static void Execute(QueuedAction queuedAction)
		{
			queuedAction.Game.RequestQueue.AddRequest(new Request()
			{
				Type = RequestType.Boolean,

				ActivePlayer = queuedAction.ActivePlayer,
				TargetPlayer = queuedAction.TargetPlayer,
				QuestionedPlayer = queuedAction.TargetPlayer,
				PlayerSymbolCounts = queuedAction.PlayerSymbolCounts,
				ResponseHandler = queuedAction.Parameters.ResponseHandler
			});

			queuedAction.TargetPlayer.AskQuestion(queuedAction.Parameters.Text);
		}
	}
}
