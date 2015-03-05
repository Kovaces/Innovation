using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Innovation.Models;
using Innovation.Models.Interfaces;

namespace Innovation.Actions.ActionWorkers
{
	public static class QueuedActionEndDogma
	{
		public static void Execute(QueuedAction queuedAction)
		{
			// a single dogma action has completed.  give away free cards and clean up

			if ((bool)queuedAction.Game.GetPropertyBagValue("OtherPlayersActed"))
				queuedAction.ActivePlayer.Hand.Add(Draw.Action(queuedAction.ActivePlayer.Tableau.GetHighestAge(), queuedAction.Game));

			queuedAction.Game.ClearPropertyBag();

			//queuedAction.Game.AdvanceGameState();
		}
	}
}
