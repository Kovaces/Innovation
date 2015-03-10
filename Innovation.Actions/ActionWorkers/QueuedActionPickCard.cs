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
	public static class QueuedActionPickCard
	{
		public static void Execute(QueuedAction queuedAction)
		{
			queuedAction.Game.RequestQueue.AddRequest(new Request()
			{
				Type = RequestType.Card,

				ActivePlayer = queuedAction.ActivePlayer,
				TargetPlayer = queuedAction.TargetPlayer,
				PlayerSymbolCounts = queuedAction.PlayerSymbolCounts,
				MinimumNumberToSelect = queuedAction.Parameters.MinSelections,
				MaximumNumberToSelect = queuedAction.Parameters.MaxSelections,
				Objects = queuedAction.Parameters.Cards,
				ResponseHandler = queuedAction.Parameters.ResponseHandler
			});

			queuedAction.TargetPlayer.PickMultipleCards(queuedAction.Parameters.Cards, queuedAction.Parameters.MinSelections, queuedAction.Parameters.MaxSelections);
		}
	}
}
